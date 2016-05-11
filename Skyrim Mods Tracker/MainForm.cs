using SMT.Managers;
using SMT.Models;
using SMT.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMT
{
    public partial class MainForm : Form
    {
        private const string DDChromeBookmarks = "chromium/x-bookmark-entries";
        private const string DDText = "Text";

        private enum SourcesColumns
        {
            Server = 0,
            Language = 1,
            Version = 2,
            State = 3,
            Path = 4
        }

        private enum ModColumns
        {
            Name = 0,
            Version = 1,
            State = 2,
            File = 3
        }

        private BindingList<Mod> mods;
        private Mod selectedMod;
        private BindingList<ModSource> sources;

        private BackgroundWorker bgWorker;
        private int pendingDialogs; // used to update status when dialogs are pending

        public MainForm()
        {
            InitializeComponent();
            DefineControlMessagesStyle();

            mods = new BindingList<Mod>(ModsManager.Mods.ToList());
            sources = new BindingList<ModSource>();
            bsMods.DataSource = mods;

            cbLanguage.DataSource = Enum.GetValues(typeof(Language));

            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;

            ApplySettings();

            if (SettingsManager.AutoScanMods)
                RunScanMods();

            SetDefaultStatus();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            dgvMods.AutoResizeColumns();
            tbName.BindLabel(lNameError);
            tbRoot.BindLabel(lRootError);
            tbVersion.BindLabel(lVersionError);
            tbURL.BindLabel(lURLError);
            tbSourceVersion.BindLabel(lSrcVersionError);

            tbName.TextChanged += OnModTextChanged;
            tbRoot.TextChanged += OnModTextChanged;
            tbVersion.TextChanged += OnModTextChanged;

            tbSourceVersion.TextChanged += OnSourceTextChanged;
            tbURL.TextChanged += OnSourceTextChanged;

            SetModEditable(false);
            SetSourceEditable(false);
            ModsManager.UpdateStates();
            MarkMods();
        }

        private void DefineControlMessagesStyle()
        {
            // define style.
            ControlMessagesExtension.ErrorBackColor = Color.LightCoral;
            ControlMessagesExtension.ErrorLabelColor = Color.OrangeRed;
            ControlMessagesExtension.WarningBackColor = Color.Khaki;
            ControlMessagesExtension.WarningLabelColor = Color.DarkOrange;
            ControlMessagesExtension.ValidBackColor = Color.LightGreen;
            ControlMessagesExtension.ValidLabelColor = Color.ForestGreen;
            ControlMessagesExtension.ClearBackColor = Color.White;
            ControlMessagesExtension.ClearLabelColor = Color.Black;
        }

        private bool SaveSources(Mod mod, bool supressWarning = false)
        {
            if (mod != null)
            {
                var invalidSourcesCount = sources.Count(s => !s.IsValid);

                if (supressWarning || invalidSourcesCount == 0 ||
                    (DialogResult.OK == MessageBox.Show("Some of the sources has invalid configuration.\n" +
                                                        "Saving these sources may break traking of the mod '" + mod.Name + "'. Save it anyways? Or cancel and fix the errors?" +
                                                        "\n\nMod '" + mod.Name + "' has " + invalidSourcesCount + " invalid sources.", "Ivalid mos sources", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)))
                {
                    mod.Sources.Clear();
                    foreach (var src in sources)
                        mod.Sources.Add(src);
                    return true;
                }
            }
            return false;
        }

        private void Save()
        {
            SetUIEnabled(false);
            var m = ModsManager.Mods;
            m.Clear();
            foreach (var mod in mods)
                m.Add(mod);
            ModsManager.NormalizeMods();
            StorageManager.Sync();
            SetUIEnabled(true);
            ModsManager.UpdateStates();
            MarkMods();
        }

        private void ApplySettings()
        {
            ofdRoot.InitialDirectory = (SettingsManager.HasValidModsLocation ? SettingsManager.ModsLocation : Environment.CurrentDirectory);
            if (SettingsManager.HasValidModsLocation)
            {
                AutoCompleteStringCollection completions = new AutoCompleteStringCollection();
                completions.AddRange(Directory.GetFiles(SettingsManager.ModsLocation).Select(str => Path.GetFileName(str)).ToArray());
                completions.AddRange(Directory.GetDirectories(SettingsManager.ModsLocation).Select(str => Path.GetFileName(str)).ToArray());
                tbRoot.AutoCompleteCustomSource = completions;
            }
        }

        private void SetModEditable(bool isEditable)
        {
            tbName.Enabled = isEditable;
            tbRoot.Enabled = isEditable;
            tbVersion.Enabled = isEditable;
            bBrowse.Enabled = isEditable;
            bRemoveMod.Enabled = isEditable;
            bAddSource.Enabled = isEditable;
            dgvSources.Enabled = isEditable;

            if (!isEditable)
            {
                dgvMods.ClearSelection();
                bsMods.SuspendBinding();
                tbName.Clear();
                tbRoot.Clear();
                tbVersion.Clear();
                tbName.ClearMessage();
                tbRoot.ClearMessage();
                tbVersion.ClearMessage();
                SetSourceEditable(false);
                dgvSources.DataSource = null;
            }
            else {
                bsMods.ResumeBinding();
                MarkSources();
            }
        }
        private void SetSourceEditable(bool isEditable)
        {
            cbLanguage.Enabled = isEditable;
            cbManual.Enabled = isEditable;
            tbSourceVersion.Enabled = isEditable && (cbManual.Enabled && cbManual.Checked);
            tbURL.Enabled = isEditable;
            bRemoveSource.Enabled = isEditable;

            if (!isEditable)
            {
                dgvSources.ClearSelection();
                bsSources.SuspendBinding();
                cbLanguage.SelectedIndex = -1;
                tbURL.Clear();
                tbSourceVersion.Clear();
                tbSourceVersion.ClearMessage();
                tbURL.ClearMessage();
                bUpdate.Enabled = false;
            }
            else
                bsSources.ResumeBinding();
        }

        private void SetUIEnabled(bool isEnabled)
        {
            msMenu.Enabled = isEnabled;
            gbMod.Enabled = isEnabled;
            gbSource.Enabled = isEnabled;
        }

        private void SetDefaultStatus()
        {
            spbStatusProgress.Visible = false;
            SetStatus(string.Format("Total mods: {0}.", mods.Count));
        }
        private void SetStatus(string status)
        {
            if (status == null) status = "";
            slStatusTitle.Text = status;
        }

        private void ValidateModName()
        {
            var cur = bsMods.Current as Mod;
            if (cur == null) return;
            if (!cur.HasValidName) tbName.SetError("Name must not be empty.");
            else if (!cur.HasUniqueName()) tbName.SetWarning("Name must be unique.");
            else tbName.SetValidMessage();
        }
        private void ValidateModVersion()
        {
            var cur = bsMods.Current as Mod;
            if (cur == null) return;
            if (!cur.HasValidVersion) tbVersion.SetError("Version must not be empty.");
            else tbVersion.SetValidMessage();
        }
        private void ValidateModFileName()
        {
            var cur = bsMods.Current as Mod;
            if (cur == null) return;
            if (!cur.HasValidFileName) tbRoot.SetWarning("Set valid root to provide version naming feature.");
            else tbRoot.SetValidMessage();
        }
        private void ValidateModFields()
        {
            if (bsMods.IsBindingSuspended) return;
            ValidateModName();
            ValidateModVersion();
            ValidateModFileName();
        }

        private void ValidateSourceURL()
        {
            var cur = bsSources.Current as ModSource;
            if (cur == null) return;
            if (!cur.HasValidURL) tbURL.SetError("Malformed URL.");
            else if (!cur.HasKnownServer) tbURL.SetError("Uknown server domain.");
            else tbURL.SetValidMessage();
        }
        private void ValidateSourceVersion()
        {
            var cur = bsSources.Current as ModSource;
            if (cur == null) return;
            if (!tbSourceVersion.Enabled && !cur.HasValidVersion) tbSourceVersion.SetError("Version must not be empty.");
            else tbSourceVersion.SetValidMessage();
        }
        private void ValidateSourceFields()
        {
            if (bsSources.IsBindingSuspended) return;
            ValidateSourceURL();
            ValidateSourceVersion();
        }

        private void MarkMods()
        {
            for (int i = 0; i < mods.Count; i++)
                MarkMod(i);
        }
        private void MarkMod(int index)
        {
            if (index < 0 || index >= mods.Count) return;
            Mod mod = mods[index];
            DataGridViewRow row = dgvMods.Rows[index];
            DataGridViewCell cell = row.Cells[(int)ModColumns.State];

            if (mod.State != ModState.NotTracking && mod.State != ModState.Undefined &&
                mod.Sources.Select(s => s.State).Where(state => state == SourceState.UnknownServer ||
                                                                state == SourceState.UnreachablePage ||
                                                                state == SourceState.UnavailableVersion).Count() > 0)
            {
                row.DefaultCellStyle.BackColor = Color.PaleVioletRed;
            }

            switch (mod.State)
            {
                case ModState.Undefined:
                case ModState.NotTracking: row.DefaultCellStyle.BackColor = Color.LightPink; break;
                case ModState.InvlaidFilePath: row.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow; break;
                case ModState.MissedFile: row.DefaultCellStyle.BackColor = Color.FromArgb(220, 220, 220); break;
                case ModState.UpToDate: row.DefaultCellStyle.BackColor = Color.LightGreen; break;
                case ModState.Outdated: row.DefaultCellStyle.BackColor = Color.Orange; break;
                default: break;
            }
            MarkSources();
        }

        private void MarkSources()
        {
            for(int i=0; i< sources.Count;i++)
            {
                ModSource src = sources[i];
                DataGridViewRow row = dgvSources.Rows[i];
                DataGridViewLinkCell serverCell = row.Cells[(int)SourcesColumns.Server] as DataGridViewLinkCell;
                DataGridViewLinkCell pathCell = row.Cells[(int)SourcesColumns.Path] as DataGridViewLinkCell;

                serverCell.LinkBehavior = (src.HasValidURL ? LinkBehavior.AlwaysUnderline : LinkBehavior.NeverUnderline);
                pathCell.LinkBehavior = serverCell.LinkBehavior;
                switch (src.State)
                {
                    default:
                    case SourceState.Undefined: row.DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.UnknownServer: row.DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.BrokenServer: row.DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.UnreachablePage: row.DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.UnavailableVersion: row.DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.Available: row.DefaultCellStyle.BackColor = Color.LightGreen; break;
                    case SourceState.Update: row.DefaultCellStyle.BackColor = Color.Orange; break;
                    case SourceState.Outdated: row.DefaultCellStyle.BackColor = Color.LightCyan; break;
                }
            }
        }

        private void RunCheckMods(IList<Mod> checkMods)
        {
            if (bgWorker.IsBusy) return;
            spbStatusProgress.Maximum = checkMods.Count();
            spbStatusProgress.Value = 0;
            spbStatusProgress.Visible = true;
            SetStatus("Checking mods...");
            SetUIEnabled(false);

            bgWorker.DoWork += CheckModsWork;
            bgWorker.ProgressChanged += CheckModsWorkProgressChanged;
            bgWorker.RunWorkerCompleted += CheckModsWorkCompleted;
            bgWorker.RunWorkerAsync(checkMods);
        }
        private void CheckModsWork(object sender, DoWorkEventArgs e)
        {
            var checkMods = e.Argument as IList<Mod>;
            int total = checkMods.Count;
            for (int i = 0; i < total; i++)
            {
                Mod cur = checkMods[i];
                int progress = (int)(((double)i / (double)checkMods.Count) * 100);
                (sender as BackgroundWorker).ReportProgress(progress, new object[] { cur, i, total, false }); // mod, index, total, isProcessed
                cur.CheckUpdates();
                (sender as BackgroundWorker).ReportProgress(progress, new object[] { cur, i, total, true }); // mod, index, total, isProcessed
            }
        }
        private void CheckModsWorkProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var userState = e.UserState as object[];
            var mod = userState[0] as Mod;
            var index = (int)userState[1];
            var total = (int)userState[2];
            var isCompleted = (bool)userState[3];
            string format = "{4}% ({2}/{3}). Checking '{0}'...{1}";
            SetStatus(string.Format(format, mod.Name, (isCompleted ? "Done" : ""), index + 1, total, e.ProgressPercentage));
            if (isCompleted)
            {
                spbStatusProgress.Value++;
                MarkMod(mods.IndexOf(mod));
            }
        }
        private void CheckModsWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetUIEnabled(true);
            dgvMods.AutoResizeColumns();
            spbStatusProgress.Value = 0;
            SetDefaultStatus();
            bgWorker.DoWork -= CheckModsWork;
            bgWorker.ProgressChanged -= CheckModsWorkProgressChanged;
            bgWorker.RunWorkerCompleted -= CheckModsWorkCompleted;
        }

        private void RunScanMods()
        {
            if (bgWorker.IsBusy) return;
            spbStatusProgress.Maximum = Directory.GetFiles(SettingsManager.ModsLocation).Length + Directory.GetDirectories(SettingsManager.ModsLocation).Length;
            spbStatusProgress.Value = 0;
            spbStatusProgress.Visible = true;
            SetStatus("Scanning mods...");
            SetUIEnabled(false);

            bgWorker.DoWork += ScanModsWork;
            bgWorker.ProgressChanged += ScanModsWorkProgressChanged;
            bgWorker.RunWorkerCompleted += ScanModsWorkCompleted;
            bgWorker.RunWorkerAsync();
        }
        private void ScanModsWork(object sender, DoWorkEventArgs e)
        {
            string[] files = Directory.GetFiles(SettingsManager.ModsLocation);
            string[] dirs = Directory.GetDirectories(SettingsManager.ModsLocation);
            int totalSize = files.Length + dirs.Length;
            for (int i = 0; i < files.Length; i++)
            {
                string filename = Path.GetFileNameWithoutExtension(files[i]);
                Mod scannedMod = ModsManager.ParseMod(filename);
                scannedMod.FileName = Path.GetFileName(files[i]);

                int progress = (int)(((double)i / (double)totalSize) * 100);
                (sender as BackgroundWorker).ReportProgress(progress, new object[] { scannedMod, i }); // mod, index
            }
        }
        private void ScanModsWorkProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var userState = e.UserState as object[];
            var mod = userState[0] as Mod;
            var index = (int)userState[1];


            List<Mod> duplicates = mods.Where(m => m.Name.Equals(mod.Name)).ToList();
            bool added = false;

            if (mod.IsValid)
            {
                if (duplicates != null && duplicates.Count > 0)
                {
                    var difVersion = duplicates.FirstOrDefault(m => !m.Version.Equals(mod.Version));
                    var sameVersion = duplicates.FirstOrDefault(m => m.Version.Equals(mod.Version));
                    if (sameVersion == null && difVersion != null)
                    {
                        pendingDialogs++;
                        if (DialogResult.Yes == MessageBox.Show(this, string.Format("Found mod '{0}', which already exists in database, but has different version.\n\nDatabase version: '{1}'\nFile version: '{2}'\nDo you want to use version file version?", mod.Name, difVersion.Version, mod.Version), "Duplicated mod", MessageBoxButtons.YesNo))
                        {
                            difVersion.Version = mod.Version;
                            pendingDialogs--;
                        }
                        else
                        { 
                            added = true;
                            mods.Add(mod);
                            pendingDialogs--;
                        }
                    }
                }
                else {
                    added = true;
                    mods.Add(mod);
                }
            }
            string format = "{4}% ({2}/{3}). {1} '{0}'";
            SetStatus(string.Format(format, mod.Name, (added ? "Added" : "Scanned"), index + 1, mods.Count, e.ProgressPercentage));
            if (pendingDialogs == 0) SetDefaultStatus();
            spbStatusProgress.Value++;
            MarkMod(mods.Count - 1);
        }
        private void ScanModsWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetUIEnabled(true);
            dgvMods.AutoResizeColumns();
            SetDefaultStatus();
            bgWorker.DoWork -= ScanModsWork;
            bgWorker.ProgressChanged -= ScanModsWorkProgressChanged;
            bgWorker.RunWorkerCompleted -= ScanModsWorkCompleted;
        }

        private void bsMods_CurrentChanged(object sender, EventArgs e)
        {
            if (bsMods.Current == null) { return; }
            (bsMods.Current as Mod).UpdateState();
            MarkMod(bsMods.Position);
        }

        private void bsMods_CurrentItemChanged(object sender, EventArgs e)
        {
            if (bsMods.Current == null) { SetModEditable(false); return; }
          
            selectedMod = bsMods.Current as Mod;
            sources = new BindingList<ModSource>(selectedMod.Sources.ToList());
            bsSources.DataSource = sources;
            dgvSources.DataSource = bsSources;
            dgvSources.ClearSelection();
            SetModEditable(mods.Count > 0);
        }
        private void bsSources_CurrentItemChanged(object sender, EventArgs e)
        {
            ValidateSourceFields();
            SetSourceEditable(sources.Count > 0);
            if (bsSources.Current != null)
                bUpdate.Enabled = (bsSources.Current as ModSource).State == SourceState.Update;
        }

        private void dgvMods_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)ModColumns.State) // "State cell except header"
                RunCheckMods(new Mod[] { mods[e.RowIndex] });
        }

        private void dgvSources_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)SourcesColumns.Server || e.ColumnIndex == (int)SourcesColumns.Path)
            {
                var src = sources[e.RowIndex];
                if (src.HasValidURL)
                    Process.Start(src.URL);
            }
        }

        private void dgvMods_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = dgvMods.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                    SetModEditable(false);
                else if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
                    SetModEditable(true);
            }
        }
        private void dgvSources_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = dgvSources.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                    SetSourceEditable(false);
                else if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
                    SetSourceEditable(true);
            }
        }

        private void EditRow(DataGridView dgv, int index)
        {
            dgv.ClearSelection();
            dgv.Rows[index].Selected = true;
            tbName.Focus();
            tbName.DeselectAll();
        }

        private void OnModTextChanged(object sender, EventArgs e)
        {
            if (bsMods.IsBindingSuspended) return;
            bsMods.EndEdit();
            (sender as TextBox).TextChanged -= OnModTextChanged;
            bsMods.ResetCurrentItem();
            (sender as TextBox).TextChanged += OnModTextChanged;
            ValidateModFields();
            var cur = bsMods.Current as Mod;
            if (cur != null)
            {
                cur.UpdateState();
                MarkMod(bsMods.Position);
            }
        }
        private void OnSourceTextChanged(object sender, EventArgs e)
        {
            if (bsSources.IsBindingSuspended) return;
            bsSources.EndEdit();
            (sender as TextBox).TextChanged -= OnSourceTextChanged;
            bsSources.ResetCurrentItem();
            (sender as TextBox).TextChanged += OnSourceTextChanged;
            ValidateSourceFields();
            MarkSources();
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == ofdRoot.ShowDialog())
            {
                tbRoot.Text = Path.GetFileName(ofdRoot.FileName);
                bsMods.EndEdit();
            }
        }
        private void bAddMod_Click(object sender, EventArgs e)
        {
            bsMods.AddNew();
            SetModEditable(true);
            EditRow(dgvMods, mods.Count - 1);
        }
        private void bRemoveMod_Click(object sender, EventArgs e)
        {
            bsMods.RemoveCurrent();
            SetModEditable(mods.Count > 0);
        }
        private void bAddSource_Click(object sender, EventArgs e)
        {
            bsSources.AddNew();
            SetSourceEditable(true);
            EditRow(dgvSources, sources.Count - 1);
        }
        private void bRemoveSource_Click(object sender, EventArgs e)
        {
            bsSources.RemoveCurrent();
            SetSourceEditable(sources.Count > 0);
        }

        private void serversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ServersForm().ShowDialog();
        }
        private void checkModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunCheckMods(mods);
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
            bsMods.ResetBindings(false);
            MarkMods();
        }
        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == new PreferencesForm().ShowDialog())
                RunScanMods();
            ApplySettings();
        }

        private void cbManual_CheckedChanged(object sender, EventArgs e)
        {
            tbSourceVersion.Enabled = cbManual.Checked;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            var invalidMods = mods.Where(m => !m.IsValid || m.Sources.Count(s => !s.IsValid) != 0).Select(m => m.Name).ToList();

            if (invalidMods.Count == 0)
            {
                if (DialogResult.Yes == MessageBox.Show("Do you want to save any changes?", "Saving data", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    Save();
            }
            else if (DialogResult.OK == MessageBox.Show("Some of the mods has invalid configuration or broken sources.\n" +
                                                    "Closing this window will save these configurations and may break some of the tracking features." +
                                                    "\n\nFix mods: [" + string.Join(", ", invalidMods) + "]" +
                                                    "\n\n Continue?", "Invalid mods", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
            {
                Save();
            }
            else e.Cancel = true;
        }

        private void dgvSources_DragDrop(object sender, DragEventArgs e)
        {
            var frmts = e.Data.GetFormats();
            if (e.Data.GetDataPresent(DDText))
                AddModSource(e.Data.GetData(DDText) as string);
            else if (e.Data.GetDataPresent(DDChromeBookmarks))
            {
                using (MemoryStream ms = e.Data.GetData(DDChromeBookmarks) as MemoryStream)
                {
                    var urls = ChromeUtils.ReadBookmarksURL(ms.ToArray());
                    foreach (var url in urls)
                        AddModSource(url);
                }
            }
                
        }

        private void dgvSources_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DDText) || e.Data.GetDataPresent(DDChromeBookmarks))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void AddModSource(string sourceURL)
        {
            
            ModSource source;
            if (ModsManager.TryBuildModSource(sourceURL, out source))
            {
                bsSources.Add(source);
                SetSourceEditable(true);
            }
            bsSources.ResetBindings(false);
            if (bsMods.Current != null)
            {
                (bsMods.Current as Mod).UpdateState();
                bsMods.ResetCurrentItem();
                MarkMod(bsMods.Position);
            }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            var src = bsSources.Current as ModSource;
            var mod = bsMods.Current as Mod;
            if (src != null && mod != null)
            {
                mod.Version = src.Version;
                bsMods.EndEdit();
                bsMods.ResetCurrentItem();
                mod.UpdateState();
                MarkMod(bsMods.Position);
                MarkSources();
            }

        }

      
    }
}
