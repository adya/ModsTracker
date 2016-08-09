using SMT.Actions;
using SMT.DGVBinding;
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
using System.Windows.Forms;

namespace SMT
{
    public partial class MainForm : Form
    {


        private enum SourcesColumns
        {
            Server,
            Version,
            State,
            Language,
            Path,
            ID
        }

        private enum ModColumns
        {
            Index,
            Name,
            Version,
            State,
            Language,
            ID
        }

        private class SMTModelIndexMapper<T> : IDataGridViewIndexMapper<T> where T : SMTModel<T>, new()
        {
            private int idColumnIndex;

            public SMTModelIndexMapper(int idColumnIndex)
            {
                this.idColumnIndex = idColumnIndex;
            }

            public int ItemIndexFromRowIndex(DataGridViewBinder<T> binder, int rowIndex)
            {
                int id = (int)binder.GridView.Rows[rowIndex].Cells[idColumnIndex].Value;
                return binder.Data.FindIndex(m => m.ID == id);
            }

            public int RowIndexFromItemIndex(DataGridViewBinder<T> binder, int itemIndex)
            {
                T model = binder.Data[itemIndex];
                for (int index = 0; index < binder.GridView.Rows.Count; index++)
                {
                    DataGridViewRow row = binder.GridView.Rows[index];
                    if ((int)row.Cells[idColumnIndex].Value == model.ID) return index;
                }
                return -1;
            }

        }

        private Mod editableMod;
        private Source editableSource;

        private Mod EditableMod { get {return editableMod;}  set { editableMod = (value != null ? value.Clone() : null); } }
        private Source EditableSource { get { return editableSource; } set { editableSource = (value != null ? value.Clone() : null); } }

        private DataGridViewBinder<Mod> modsBinder;
        private DataGridViewBinder<Source> sourcesBinder;

        private BackgroundWorker bgWorker;
        private ActionsManager actionsManager;

        public MainForm()
        {
            InitializeComponent();
            BindMods();
            actionsManager = new ActionsManager();

            cbSourceLanguage.DataSource = Enum.GetValues(typeof(Language));
            cbModLanguage.DataSource = Enum.GetValues(typeof(Language));

            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;

            SetDefaultStatus();
        }

        private void BindMods()
        {
            Unbind(modsBinder);
            modsBinder = new DataGridViewBinder<Mod>(dgvMods, new SelectionList<Mod>(ModsManager.Mods));
            modsBinder.IndexMapper = new SMTModelIndexMapper<Mod>((int)ModColumns.ID);
            modsBinder.PopulateRow += ModsBinder_OnPopulateRow;
            modsBinder.ItemSelected += ModsBinder_OnItemSelected;
            modsBinder.SelectionCleared += ModsBinder_SelectionCleared;
            modsBinder.AddCellClickHandler((int)ModColumns.State, ModsBinder_State_CellContentClick);
            modsBinder.Refresh(true);
        }

        private void BindSources(Mod mod)
        {
            Unbind(sourcesBinder);
            sourcesBinder = new DataGridViewBinder<Source>(dgvSources, new SelectionList<Source>(mod.Sources));
            sourcesBinder.IndexMapper = new SMTModelIndexMapper<Source>((int)SourcesColumns.ID);
            sourcesBinder.ItemSelected += SourcesBinder_OnItemSelected;
            sourcesBinder.PopulateRow += SourcesBinder_OnPopulateRow;
            sourcesBinder.SelectionCleared += SourcesBinder_SelectionCleared;
            sourcesBinder.AddCellClickHandler((int)SourcesColumns.Server, SourcesBinder_Server_Path_CellContentClick);
            sourcesBinder.AddCellClickHandler((int)SourcesColumns.Path, SourcesBinder_Server_Path_CellContentClick);
            sourcesBinder.Refresh(true);
        }

        private void Unbind<T>(DataGridViewBinder<T> binder) where T : new()
        {
            if (binder != null) binder.Dispose();
        }

        #region Mods Binder
        private void ModsBinder_OnItemSelected(DataGridViewBinder<Mod> sender, Mod item)
        {
            EditMod(item);
        }

        private void ModsBinder_SelectionCleared(DataGridViewBinder<Mod> sender)
        {
            CancelEditMod();
        }

        private void ModsBinder_OnPopulateRow(DataGridViewBinder<Mod> sender, DataGridViewRow row, Mod item)
        {
            row.Cells[(int)ModColumns.Index].Value = row.Index + 1;
            row.Cells[(int)ModColumns.Name].Value = item.Name;
            row.Cells[(int)ModColumns.Version].Value = item.Version.Value;
            row.Cells[(int)ModColumns.State].Value = item.StateString;
            row.Cells[(int)ModColumns.Language].Value = item.Language.ToShortString();
            row.Cells[(int)ModColumns.ID].Value = item.ID;
        }
        private void ModsBinder_State_CellContentClick(DataGridViewBinder<Mod> sender, DataGridViewRow row, Mod item)
        {
            RunCheckMods(new Mod[] { item });
        }
        #endregion
        #region Source Binder
        private void SourcesBinder_OnPopulateRow(DataGridViewBinder<Source> sender, DataGridViewRow row, Source item)
        {
            row.Cells[(int)SourcesColumns.Server].Value = item.Server.Name;
            row.Cells[(int)SourcesColumns.Version].Value = item.Version.Value;
            row.Cells[(int)SourcesColumns.State].Value = item.StateString;
            row.Cells[(int)SourcesColumns.Language].Value = item.Language.ToShortString();
            row.Cells[(int)SourcesColumns.Path].Value = item.Path;
            row.Cells[(int)SourcesColumns.ID].Value = item.ID;
        }

        private void SourcesBinder_OnItemSelected(DataGridViewBinder<Source> sender, Source item)
        {
            EditSource(item);
        }

        private void SourcesBinder_SelectionCleared(DataGridViewBinder<Source> sender)
        {
            CancelEditSource();
        }

        private void SourcesBinder_Server_Path_CellContentClick(DataGridViewBinder<Source> sender, DataGridViewRow row, Source item)
        {
            if (item.HasValidURL)
                Process.Start(item.URL);
        }
        #endregion

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

            CancelEditMod();
            ModsManager.UpdateStates();
            MarkMods();
        }

        private bool SaveSources(Mod mod, bool supressWarning = false)
        {
            if (mod != null)
            {
                var invalidSourcesCount = sourcesBinder.Data.Count(s => !s.IsValid);

                if (supressWarning || invalidSourcesCount == 0 ||
                    (DialogResult.OK == MessageBox.Show("Some of the sources has invalid configuration.\n" +
                                                        "Saving these sources may break traking of the mod '" + mod.Name + "'. Save it anyways? Or cancel and fix the errors?" +
                                                        "\n\nMod '" + mod.Name + "' has " + invalidSourcesCount + " invalid sources.", "Ivalid mos sources", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)))
                {
                    mod.Sources.Clear();
                    foreach (var src in sourcesBinder.Data)
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
            modsBinder.Data.Sort(new Comparison<Mod>((m1, m2) => m1.CompareTo(m2)));
            foreach (var mod in modsBinder.Data)
                m.Add(mod);
            ModsManager.NormalizeMods();
            StorageManager.Sync();
            SetUIEnabled(true);
            ModsManager.UpdateStates();
            MarkMods();
        }

        private void EditMod(Mod mod)
        {
            EditableMod = mod;
            Unbind(sourcesBinder);
            if (EditableMod != null)
            {
                SubscribeModsFieldsEvents(false);
                tbModName.Text = EditableMod.Name;
                tbModVersion.Text = EditableMod.Version.Value;
                cbModLanguage.SelectedItem = EditableMod.Language;
                SubscribeModsFieldsEvents(true);
                BindSources(EditableMod);
                MarkSources();
            }
            SetModEditable(EditableMod != null);
        }
        private void CancelEditMod() { EditMod(null); }

        private void EditSource(Source src)
        {
            EditableSource = src;
            if (EditableSource != null)
            {
                SubscribeSourcesFieldsEvents(false);
                tbSourceURL.Text = EditableSource.URL;
                tbSourceVersion.Text = EditableSource.Version.Value;
                cbSourceLanguage.SelectedItem = EditableSource.Language;               
                SubscribeSourcesFieldsEvents(true);
            }
            SetSourceEditable(EditableSource != null);
        }
        private void CancelEditSource() { EditSource(null); }

        private void SubscribeModsFieldsEvents(bool subscribe)
        {
            if (subscribe)
            {
                tbModName.TextChanged += OnModTextChanged;
                tbModVersion.TextChanged += OnModTextChanged;
                cbModLanguage.SelectedValueChanged += OnModTextChanged;
            }
            else
            {
                tbModName.TextChanged -= OnModTextChanged;
                tbModVersion.TextChanged -= OnModTextChanged;
                cbModLanguage.SelectedValueChanged -= OnModTextChanged;
            }
        }

        private void SubscribeSourcesFieldsEvents(bool subscribe)
        {
            if (subscribe)
            {
                tbSourceURL.TextChanged += OnSourceTextChanged;
                tbSourceVersion.TextChanged += OnSourceTextChanged;
                cbSourceLanguage.SelectedValueChanged += OnSourceTextChanged;
            }
            else
            {
                tbSourceURL.TextChanged -= OnSourceTextChanged;
                tbSourceVersion.TextChanged -= OnSourceTextChanged;
                cbSourceLanguage.SelectedValueChanged -= OnSourceTextChanged;
            }
        }

        private void SetModEditable(bool isEditable)
        {
            foreach (Control c in gbMod.Controls)
                c.Enabled = isEditable;
            bAddMod.Enabled = true;
            bAddSource.Enabled = isEditable;
            dgvSources.Enabled = isEditable;
            SetSourceEditable(false);
            if (!isEditable)
            {
                SubscribeModsFieldsEvents(false);
                modsBinder.ClearSelection();
                tbModName.Clear();
                tbModVersion.Clear();
                tbModName.ClearMessage();
                tbModVersion.ClearMessage();
                Unbind(sourcesBinder);
            }
        }
        private void SetSourceEditable(bool isEditable)
        {
            foreach (Control c in gbSource.Controls)
                c.Enabled = isEditable;
            tbSourceVersion.Enabled = isEditable && cbManual.Checked;
            bAddSource.Enabled = EditableMod != null;
            if (EditableSource != null && EditableMod != null)
                bUpdate.Enabled = (EditableSource.Version > EditableMod.Version);
            if (!isEditable)
            {
                if (sourcesBinder != null)
                    sourcesBinder.ClearSelection();
                SubscribeSourcesFieldsEvents(false);
                tbSourceURL.Clear();
                tbSourceVersion.Clear();
                tbSourceVersion.ClearMessage();
                tbSourceURL.ClearMessage();
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
            SetStatus(string.Format("Total mods: {0}.", modsBinder.Data.Count));
        }
        private void SetStatus(string status)
        {
            if (status == null) status = "";
            slStatusTitle.Text = status;
        }

        #region Fields validation
        private bool ValidateModName(Mod mod)
        {
            if (mod == null) tbModName.ClearMessage();
            else if (!mod.HasValidName) { tbModName.SetError("Name must not be empty."); return false; }
            else if (!mod.HasUniqueName()) tbModName.SetWarning("Name must be unique.");
            else tbModName.SetValidMessage();
            return true;
        }
        private bool ValidateModVersion(Mod mod)
        {
            if (mod == null) tbModVersion.ClearMessage();
            else if (!mod.HasValidVersion) {  tbModVersion.SetError("Version must not be empty."); return false; }
            else tbModVersion.SetValidMessage();
            return true;
        }
        private bool ValidateModFields(Mod mod)
        {
            return ValidateModName(mod) &&
                   ValidateModVersion(mod);
        }

        private bool ValidateSourceURL(Source source)
        {
            if (source == null) tbSourceURL.ClearMessage();
            else if (!source.HasValidURL) { tbSourceURL.SetError("Malformed URL."); return false; }
            else if (!source.HasKnownServer) { tbSourceURL.SetError("Uknown server domain."); return false; }
            else tbSourceURL.SetValidMessage();
            return true;
        }
        private bool ValidateSourceVersion(Source source)
        {
            if (source == null) tbSourceVersion.ClearMessage();
            else if (!tbSourceVersion.Enabled && !source.HasValidVersion) { tbSourceVersion.SetError("Version must not be empty."); return false; }
            else tbSourceVersion.SetValidMessage();
            return true;
        }
        private bool ValidateSourceFields(Source source)
        {
            return ValidateSourceURL(source) &&
                   ValidateSourceVersion(source);
        }
        #endregion

        private void MarkMods()
        {
            for (int i = 0; i < modsBinder.Data.Count; i++)
                MarkMod(i);
        }
        private void MarkMod(int index)
        {
            int rowIndex = modsBinder.IndexMapper.RowIndexFromItemIndex(modsBinder, index);
            if (index < 0 || index >= dgvMods.Rows.Count) return;
            Mod mod = modsBinder.Data[index];
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
                case ModState.UpToDate: row.DefaultCellStyle.BackColor = Color.LightGreen; break;
                case ModState.Outdated: row.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow; break;
                default: break;
            }
            MarkSources();
        }
        private void MarkSources()
        {
            if (sourcesBinder == null) return;
            for (int i = 0; i < sourcesBinder.Data.Count; i++)
            {
                MarkSource(i);
            }
        }
        private void MarkSource(int index)
        {
            Source src = sourcesBinder.Data[index];
            DataGridViewRow row = dgvSources.Rows[index];
            DataGridViewLinkCell serverCell = row.Cells[(int)SourcesColumns.Server] as DataGridViewLinkCell;
            DataGridViewLinkCell pathCell = row.Cells[(int)SourcesColumns.Path] as DataGridViewLinkCell;

            serverCell.LinkBehavior = (src.HasValidURL ? LinkBehavior.AlwaysUnderline : LinkBehavior.NeverUnderline);
            pathCell.LinkBehavior = serverCell.LinkBehavior;
            Mod mod = modsBinder.Data.SelectedItem;
            if (src.Version > mod.Version)
                row.DefaultCellStyle.BackColor = Color.Orange;
            else if (src.Version < mod.Version)
                row.DefaultCellStyle.BackColor = Color.LightCyan;
            else {
                switch (src.State)
                {
                    default:
                    case SourceState.Undefined: row.DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.UnknownServer: row.DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.BrokenServer: row.DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.UnreachablePage: row.DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.UnavailableVersion: row.DefaultCellStyle.BackColor = Color.LightPink; break;
                    case SourceState.Available: row.DefaultCellStyle.BackColor = Color.LightGreen; break;
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
                MarkMod(modsBinder.Data.IndexOf(mod));
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

        #region Drag & Drop
        private void dgvSources_DragDrop(object sender, DragEventArgs e)
        {
            var frmts = e.Data.GetFormats();
            if (e.Data.GetDataPresent(ChromeUtils.DDText))
                AddModSource(e.Data.GetData(ChromeUtils.DDText) as string);
            else if (e.Data.GetDataPresent(ChromeUtils.DDChromeBookmarks))
            {
                using (MemoryStream ms = e.Data.GetData(ChromeUtils.DDChromeBookmarks) as MemoryStream)
                {
                    var urls = ChromeUtils.ReadBookmarksURL(ms.ToArray());
                    foreach (var url in urls)
                        AddModSource(url);
                }
            }

        }
        private void dgvSources_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(ChromeUtils.DDText) || e.Data.GetDataPresent(ChromeUtils.DDChromeBookmarks))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }
        #endregion

        private void OnModTextChanged(object sender, EventArgs e)
        {
            if (EditableMod != null)
            {
                EditableMod.Name = tbModName.Text;
                EditableMod.Language = (Language)cbModLanguage.SelectedItem;
                EditableMod.Version.Value = tbModVersion.Text;
                if (ValidateModFields(EditableMod))
                {
                    if (modsBinder.IsSelected)
                    {
                        EditableMod.CopyTo(modsBinder.Data.SelectedItem);
                        modsBinder.Refresh();
                    }
                }
                MarkMod(modsBinder.SelectedItemIndex);
            }
        }
        private void OnSourceTextChanged(object sender, EventArgs e)
        {
            if (EditableSource != null)
            {
                EditableSource.URL = tbSourceURL.Text;
                EditableSource.Language = (Language)cbSourceLanguage.SelectedItem;
                if (cbManual.Checked) EditableSource.Version.Value = tbSourceVersion.Text;
                if (ValidateSourceFields(EditableSource))
                {
                    if (sourcesBinder.IsSelected)
                    {
                        EditableSource.CopyTo(sourcesBinder.Data.SelectedItem);
                        sourcesBinder.Refresh();
                    }
                }
                MarkMod(modsBinder.SelectedItemIndex);
                MarkSource(sourcesBinder.SelectedItemIndex);
            }
        }

        private void bAddMod_Click(object sender, EventArgs e)
        {

        }
        private void bRemoveMod_Click(object sender, EventArgs e)
        {
            if (modsBinder.IsSelected)
                actionsManager.PerformAction(new RemoveModelAction<Mod>(modsBinder.SelectedItem, modsBinder.Data));
            CancelEditMod();
        }
        private void bAddSource_Click(object sender, EventArgs e)
        {
            EditSource(sourcesBinder.AddItem());
            sourcesBinder.SelectRow(sourcesBinder.Data.Count - 1);
        }
        private void bRemoveSource_Click(object sender, EventArgs e)
        {
            if (sourcesBinder.IsSelected)
                actionsManager.PerformAction(new RemoveModelAction<Source>(sourcesBinder.SelectedItem, sourcesBinder.Data));
            CancelEditSource();
        }
        private void bUpdate_Click(object sender, EventArgs e)
        {
            if (modsBinder.Data.IsSelected && sourcesBinder.Data.IsSelected)
            {
                modsBinder.Data.SelectedItem.Version = sourcesBinder.Data.SelectedItem.Version;
                modsBinder.Data.SelectedItem.Language = sourcesBinder.Data.SelectedItem.Language;
                sourcesBinder.Data.SelectedItem.UpdateState();
                modsBinder.Data.SelectedItem.UpdateState();
                modsBinder.Refresh();
                MarkMod(modsBinder.Data.SelectedIndex);
                MarkSources();
            }

        }
        private void cbManual_CheckedChanged(object sender, EventArgs e)
        {
            tbSourceVersion.Enabled = cbManual.Checked;
        }

        private void serversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ServersForm().ShowDialog();
        }
        private void checkModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunCheckMods(modsBinder.Data);
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
            MarkMods();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            var invalidMods = modsBinder.Data.Where(m => !m.IsValid || m.Sources.Count(s => !s.IsValid) != 0).Select(m => m.Name).ToList();

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

        private void AddModSource(string sourceURL)
        {
            Source source;
            if (SourcesManager.TryBuildModSource(sourceURL, out source))
            {
                SetSourceEditable(true);
            }
            if (modsBinder.Data.IsSelected)
            {
                modsBinder.Data.SelectedItem.UpdateState();
                MarkMod(modsBinder.Data.SelectedIndex);
            }
        }
    }
}
