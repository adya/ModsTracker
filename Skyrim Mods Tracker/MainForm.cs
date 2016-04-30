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
        private BindingList<Mod> mods;
        private BindingList<ModSource> sources;

        public MainForm()
        {
            InitializeComponent();
            DefineControlMessagesStyle();

            mods = new BindingList<Mod>(ModsManager.Mods.ToList());
            sources = new BindingList<ModSource>();
            bsMods.DataSource = mods;

            cbLanguage.DataSource = Enum.GetValues(typeof(Language));
            ofdRoot.InitialDirectory = Environment.CurrentDirectory;

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
            else
                bsMods.ResumeBinding();
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
        private void ValidateModRoot()
        {
            var cur = bsMods.Current as Mod;
            if (cur == null) return;
            if (!cur.HasValidRoot) tbRoot.SetWarning("Set valid root to provide version naming feature.");
            else tbRoot.SetValidMessage();
        }
        private void ValidateModFields()
        {
            if (bsMods.IsBindingSuspended) return;
            ValidateModName();
            ValidateModVersion();
            ValidateModRoot();
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
                MarkMod(mods[i], i);
        }
        private void MarkMod(Mod mod, int index)
        {

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

        private void bsMods_CurrentItemChanged(object sender, EventArgs e)
        {
            if (bsMods.Current == null) { SetModEditable(false); return; }
            var selectedMod = bsMods.Current as Mod;
            if (selectedMod != null) SaveSources(selectedMod);
            sources = new BindingList<ModSource>(selectedMod.Sources.ToList());
            bsSources.DataSource = sources;
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
   
        private void bBrowse_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == ofdRoot.ShowDialog())
            {
                tbRoot.Text = ofdRoot.FileName;
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
            if (mods.Count > 0) EditRow(dgvMods, mods.Count - 1);
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
            if (sources.Count > 0) EditRow(dgvSources, sources.Count - 1);
        }

        private void serversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ServersForm().ShowDialog();
        }
        private void checkModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModsManager.CheckUpdates();
            MarkMods();
            dgvMods.AutoResizeColumns();
        }
  
        private void cbManual_CheckedChanged(object sender, EventArgs e)
        {
            tbSourceVersion.Enabled = cbManual.Checked;
        }

        private void OnModTextChanged(object sender, EventArgs e)
        {
            if (bsMods.IsBindingSuspended) return;
            bsMods.EndEdit();
            bsMods.ResetCurrentItem();
            ValidateModFields();
        }
        private void OnSourceTextChanged(object sender, EventArgs e)
        {
            if (bsSources.IsBindingSuspended) return;
            bsSources.EndEdit();
            bsSources.ResetCurrentItem();
            ValidateSourceFields();
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
                var m = ModsManager.Mods;
                m.Clear();
                foreach (var mod in mods)
                {
                    SaveSources(mod, true);
                    m.Add(mod);
                }
            }
            else e.Cancel = true;
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ModsManager.NormalizeMods();
            StorageManager.Sync();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PreferencesForm().ShowDialog();
        }
    }
}
