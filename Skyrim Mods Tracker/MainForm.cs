﻿using SMT.Managers;
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

        private List<Mod> mods;
        private Mod selectedMod;
        private int selectedModIndex;

        private List<Source> sources;
        private Source selectedSource;
        private int selectedSorceIndex;

        private BackgroundWorker bgWorker;
        private int pendingDialogs; // used to update status when dialogs are pending

        public MainForm()
        {
            InitializeComponent();

            mods = ModsManager.Mods.ToList();
            cbSourceLanguage.DataSource = Enum.GetValues(typeof(Language));

            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;

            SetDefaultStatus();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            dgvMods.AutoResizeColumns();
            tbModName.BindLabel(lNameError);
            tbModVersion.BindLabel(lVersionError);
            tbSourceURL.BindLabel(lURLError);
            tbSourceVersion.BindLabel(lSrcVersionError);

            tbModName.TextChanged += OnModTextChanged;
            tbModVersion.TextChanged += OnModTextChanged;

            tbSourceVersion.TextChanged += OnSourceTextChanged;
            tbSourceURL.TextChanged += OnSourceTextChanged;

            SetModEditable(false);
            SetSourceEditable(false);
            ModsManager.UpdateStates();
            MarkMods();
        }

        #region Manual "Data Binding"

        private void SelectMod(int index) { if (index < 0 || index >= mods.Count) return; selectedModIndex = index; selectedMod = mods[selectedModIndex]; }
        private void LoadSelectedMod() { tbModName.Text = selectedMod.Name; }
        private void AddMod() { dgvMods.Rows.Add(); mods.Add(new Mod()); }
        private void RemoveMod(int index) { if (index < 0 || index >= mods.Count) return; mods.RemoveAt(index); dgvMods.Rows.RemoveAt(index); }


        #endregion

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

        private void SetModEditable(bool isEditable)
        {
            tbModName.Enabled = isEditable;
            tbModVersion.Enabled = isEditable;
            bRemoveMod.Enabled = isEditable;
            bAddSource.Enabled = isEditable;
            dgvSources.Enabled = isEditable;

            if (!isEditable)
            {
                dgvMods.ClearSelection();
                tbModName.Clear();
                tbModVersion.Clear();
                tbModName.ClearMessage();
                tbModVersion.ClearMessage();
                SetSourceEditable(false);
                dgvSources.DataSource = null;
            }
            else {
                MarkSources();
            }
        }
        private void SetSourceEditable(bool isEditable)
        {
            cbSourceLanguage.Enabled = isEditable;
            cbManual.Enabled = isEditable;
            tbSourceVersion.Enabled = isEditable && (cbManual.Enabled && cbManual.Checked);
            tbSourceURL.Enabled = isEditable;
            bRemoveSource.Enabled = isEditable;

            if (!isEditable)
            {
                dgvSources.ClearSelection();
                cbSourceLanguage.SelectedIndex = -1;
                tbSourceURL.Clear();
                tbSourceVersion.Clear();
                tbSourceVersion.ClearMessage();
                tbSourceURL.ClearMessage();
                bUpdate.Enabled = false;
            }
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
            if (selectedMod == null) return;
            if (!selectedMod.HasValidName) tbModName.SetError("Name must not be empty.");
            else if (!selectedMod.HasUniqueName()) tbModName.SetWarning("Name must be unique.");
            else tbModName.SetValidMessage();
        }
        private void ValidateModVersion()
        {
            if (selectedMod == null) return;
            if (!selectedMod.HasValidVersion) tbModVersion.SetError("Version must not be empty.");
            else tbModVersion.SetValidMessage();
        }
       
        private void ValidateModFields()
        {
            ValidateModName();
            ValidateModVersion();
        }

        private void ValidateSourceURL()
        {
            if (selectedSource == null) return;
            if (!selectedSource.HasValidURL) tbSourceURL.SetError("Malformed URL.");
            else if (!selectedSource.HasKnownServer) tbSourceURL.SetError("Uknown server domain.");
            else tbSourceURL.SetValidMessage();
        }
        private void ValidateSourceVersion()
        {
            if (selectedSource == null) return;
            if (!tbSourceVersion.Enabled && !selectedSource.HasValidVersion) tbSourceVersion.SetError("Version must not be empty.");
            else tbSourceVersion.SetValidMessage();
        }
        private void ValidateSourceFields()
        {
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
            if (index < 0 || index >= dgvMods.Rows.Count) return;
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
                Source src = sources[i];
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
             //       case SourceState.Update: row.DefaultCellStyle.BackColor = Color.Orange; break;
             //       case SourceState.Outdated: row.DefaultCellStyle.BackColor = Color.LightCyan; break;
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
                cur.CheckUpdate();
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
            tbModName.Focus();
            tbModName.DeselectAll();
        }

        private void OnModTextChanged(object sender, EventArgs e)
        {
            ValidateModFields();
            if (selectedMod != null)
            {
                selectedMod.UpdateState();
                MarkMod(selectedModIndex);
            }
        }
        private void OnSourceTextChanged(object sender, EventArgs e)
        {
            ValidateSourceFields();
            MarkSources();
        }

        private void bAddMod_Click(object sender, EventArgs e)
        {
            SetModEditable(true);
            EditRow(dgvMods, mods.Count - 1);
        }
        private void bRemoveMod_Click(object sender, EventArgs e)
        {
            SetModEditable(mods.Count > 0);
        }
        private void bAddSource_Click(object sender, EventArgs e)
        {
            SetSourceEditable(true);
            EditRow(dgvSources, sources.Count - 1);
        }
        private void bRemoveSource_Click(object sender, EventArgs e)
        {
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
            MarkMods();
        }
        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
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
            Source source;
            if (SourcesManager.TryBuildModSource(sourceURL, out source))
            {
                SetSourceEditable(true);
            }
            if (selectedMod != null)
            {
                selectedMod.UpdateState();
                MarkMod(selectedModIndex);
            }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            if (selectedSource != null && selectedMod != null)
            {
                selectedMod.Version = selectedSource.Version;
                selectedMod.Language = selectedSource.Language;
                selectedMod.UpdateState();
                MarkMod(selectedModIndex);
                MarkSources();
            }

        }

      
    }
}
