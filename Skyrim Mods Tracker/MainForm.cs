using SMT.Managers;
using SMT.Models;
using SMT.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            var m = ModsManager.Mods;
            m.Clear();
            foreach (var mod in mods)
                m.Add(mod);
            ModsManager.NormalizeMods();
            StorageManager.Sync();
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
            bAddSource.Enabled = isEditable;
            bRemoveMod.Enabled = isEditable;
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
            }
            else
                bsSources.ResumeBinding();
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
            Mod mod = mods[index];
            if (mod.State != ModState.NotTracking && mod.State != ModState.Undefined &&
                mod.Sources.Select(s => s.State).Where(state => state == SourceState.UnknownServer ||
                                                                state == SourceState.UnreachablePage ||
                                                                state == SourceState.UnavailableVersion).Count() > 0)
            {
                dgvMods.Rows[index].DefaultCellStyle.BackColor = Color.Aqua;
            }

            switch (mod.State)
            {
                case ModState.Undefined:
                case ModState.NotTracking: dgvMods.Rows[index].DefaultCellStyle.BackColor = Color.LightPink; break;
                case ModState.MissedFile: dgvMods.Rows[index].DefaultCellStyle.BackColor = Color.FromArgb(220, 220, 220); break;
                case ModState.UpToDate: dgvMods.Rows[index].DefaultCellStyle.BackColor = Color.LightGreen; break;
                case ModState.Outdated: dgvMods.Rows[index].DefaultCellStyle.BackColor = Color.Orange; break;
                default: break;
            }
        }

        private void MarkSources()
        {
            for(int i=0; i< sources.Count;i++)
            {
                ModSource src = sources[i];
                switch (src.State)
                {
                    default:
                    case SourceState.Undefined: dgvSources.Rows[i].DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.UnknownServer: dgvSources.Rows[i].DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.BrokenServer: dgvSources.Rows[i].DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.UnreachablePage: dgvSources.Rows[i].DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.UnavailableVersion: dgvSources.Rows[i].DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.Available: dgvSources.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen; break;
                    case SourceState.Update: dgvSources.Rows[i].DefaultCellStyle.BackColor = Color.Orange; break;
                }
            }
        }

        private void RunCheckMods()
        {
            spbStatusProgress.Maximum = mods.Count;
            spbStatusProgress.Value = 0;
            spbStatusProgress.Visible = true;
            SetStatus("Checking mods...");

            bgWorker.DoWork += CheckModsWork;
            bgWorker.ProgressChanged += CheckModsWorkProgressChanged;
            bgWorker.RunWorkerCompleted += CheckModsWorkCompleted;
            bgWorker.RunWorkerAsync();
        }
        private void CheckModsWork(object sender, DoWorkEventArgs e)
        {

            for (int i = 0; i < mods.Count; i++)
            {
                int progress = (int)(((double)i / (double)mods.Count) * 100);
                (sender as BackgroundWorker).ReportProgress(progress, new object[] { mods[i], i, false }); // mod, index, isProcessed
                mods[i].CheckUpdates();
                (sender as BackgroundWorker).ReportProgress(progress, new object[] { mods[i], i, true }); // mod, index, isProcessed
            }
        }
        private void CheckModsWorkProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var userState = e.UserState as object[];
            var mod = userState[0] as Mod;
            var index = (int)userState[1];
            var isCompleted = (bool)userState[2];
            string format = "{4}% ({2}/{3}). Checking '{0}'...{1}";
            SetStatus(string.Format(format, mod.Name, (isCompleted ? "Done" : ""), index + 1, mods.Count, e.ProgressPercentage));
            if (isCompleted)
            {
                spbStatusProgress.Value++;
                MarkMod(index);
            }
        }
        private void CheckModsWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvMods.AutoResizeColumns();
            spbStatusProgress.Value = 0;
            SetDefaultStatus();
            bgWorker.DoWork -= CheckModsWork;
            bgWorker.ProgressChanged -= CheckModsWorkProgressChanged;
            bgWorker.RunWorkerCompleted -= CheckModsWorkCompleted;
        }

        private void RunScanMods()
        {
            spbStatusProgress.Maximum = Directory.GetFiles(SettingsManager.ModsLocation).Length + Directory.GetDirectories(SettingsManager.ModsLocation).Length;
            spbStatusProgress.Value = 0;
            spbStatusProgress.Visible = true;
            SetStatus("Scanning mods...");

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
                    if (difVersion != null)
                    {
                        pendingDialogs++;
                        if (DialogResult.Yes == MessageBox.Show(this, string.Format("Found mod '{0}', which already exists in database, but has different version.\n\nDatabase version: '{1}'\nFile version: '{2}'\nDo you want to use version file version?", mod.Name, difVersion.Version, mod.Version), "Duplicated mod", MessageBoxButtons.YesNo))
                        {
                            difVersion.Version = mod.Version;
                            pendingDialogs--;
                        }
                        else if (sameVersion == null)
                        {
                            added = true;
                            mods.Add(mod);

                        }
                        else pendingDialogs--;
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
            dgvMods.AutoResizeColumns();
            SetDefaultStatus();
            bgWorker.DoWork -= ScanModsWork;
            bgWorker.ProgressChanged -= ScanModsWorkProgressChanged;
            bgWorker.RunWorkerCompleted -= ScanModsWorkCompleted;
        }

        private void bsMods_CurrentItemChanged(object sender, EventArgs e)
        {
            if (bsMods.Current == null) { SetModEditable(false); return; }
           
            if (selectedMod != null) SaveSources(selectedMod);
            selectedMod = bsMods.Current as Mod;
            sources = new BindingList<ModSource>(selectedMod.Sources.ToList());
            bsSources.DataSource = sources;
            dgvSources.DataSource = bsSources;
            MarkSources();
            dgvSources.ClearSelection();
        }
        private void bsSources_CurrentItemChanged(object sender, EventArgs e)
        {
            ValidateSourceFields();
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
        }
        private void OnSourceTextChanged(object sender, EventArgs e)
        {
            if (bsSources.IsBindingSuspended) return;
            bsSources.EndEdit();
            (sender as TextBox).TextChanged -= OnSourceTextChanged;
            bsSources.ResetCurrentItem();
            (sender as TextBox).TextChanged += OnSourceTextChanged;
            ValidateSourceFields();
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
            //  if (mods.Count > 0) EditRow(dgvMods, mods.Count - 1);
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
            SetSourceEditable(false);
            //       if (sources.Count > 0) EditRow(dgvSources, sources.Count - 1);
        }

        private void serversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ServersForm().ShowDialog();
        }
        private void checkModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunCheckMods();
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

            if (invalidMods.Count == 0 ||
                (DialogResult.OK == MessageBox.Show("Some of the mods has invalid configuration or broken sources.\n" +
                                                    "Closing this window will save these configurations and may break some of the tracking features." +
                                                    "\n\nFix mods: [" + string.Join(", ", invalidMods) + "]" +
                                                    "\n\n Continue?", "Invalid mods", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)))
            {
                Save();
            }
            else e.Cancel = true;
        }

        private void dgvSources_DragDrop(object sender, DragEventArgs e)
        {
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
        }

       
    }
}
