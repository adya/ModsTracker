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
        private Mod selectedMod;
        private List<ModSource> sources;
        private ModSource selectedSource;

        public MainForm()
        {
            InitializeComponent();
            DefineControlMessagesStyle();

            mods = new BindingList<Mod>(ModsManager.Mods.ToList());
            bsMods.DataSource = mods;
            cbLanguage.DataSource = Enum.GetValues(typeof(Language));
            ofdRoot.InitialDirectory = Environment.CurrentDirectory;
            SetModEditable(false);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dgvMods.AutoResizeColumns();

            tbName.BindLabel(lNameError);
            tbRoot.BindLabel(lRootError);
            tbVersion.BindLabel(lVersionError);
            tbURL.BindLabel(lURLError);
            tbSourceVersion.BindLabel(lSrcVersionError);

            dgvMods.ClearSelection();
            dgvSources.ClearSelection();

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

        private void bsModsCurrentChanged(object sender, EventArgs e)
        {
            SaveSources();
            if (bsMods.Current == null) { SetModEditable(false); return; }
            selectedMod = bsMods.Current as Mod;
            sources = selectedMod.Sources.ToList();
            bsSources.DataSource = sources;
            dgvSources.DataSource = bsSources;
        }

        private void SaveSources()
        {
            if (selectedMod != null)
            {
                selectedMod.Sources.Clear();
                foreach (var source in sources)
                {
                    if (ValidateSource(source))
                        selectedMod.Sources.Add(source);
                }
            }
        }

        private void bsSourcesCurrentChanged(object sender, EventArgs e)
        {
            selectedSource = bsSources.Current as ModSource;
            SetSourceEditable(selectedSource != null);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSources();
            var m = ModsManager.Mods;
            m.Clear();
            foreach (var mod in mods)
                m.Add(mod);
            StorageManager.Sync();
        }

        private void serversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ServersForm().ShowDialog();
        }

       

        private void dgvMods_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = dgvMods.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    dgvMods.ClearSelection();
                    SetModEditable(false);
                }
                else if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
                {
                    SetModEditable(true);
                }
            }
        }

        private bool ValidateSource(ModSource source)
        {
            return (source.Server != null && source.Path != "");
        }

        private void SetModEditable(bool isEditable)
        {
            tbName.Enabled = isEditable;
            tbRoot.Enabled = isEditable;
            tbVersion.Enabled = isEditable;
            bBrowse.Enabled = isEditable;
            bAddSource.Enabled = isEditable;
            bRemoveSource.Enabled = isEditable;
            bRemoveMod.Enabled = isEditable;
            dgvSources.Enabled = isEditable;
            if (isEditable) bsMods.ResumeBinding();
            else
            {
                bsMods.SuspendBinding();
                tbName.Clear();
                tbRoot.Clear();
                tbVersion.Clear();
                SetSourceEditable(false);
                dgvSources.DataSource = null;
                dgvSources.ClearSelection();
            }
        }

        private void SetSourceEditable(bool isEditable)
        {
            cbLanguage.Enabled = isEditable;
            cbManual.Enabled = isEditable;
            tbSourceVersion.Enabled = isEditable && (cbManual.Enabled && cbManual.Checked);
            tbURL.Enabled = isEditable;
            if (isEditable)
            {
                bsSources.ResumeBinding();
            }
            else
            {
                bsSources.SuspendBinding();
                cbLanguage.SelectedIndex = -1;
                tbSourceVersion.Clear();
            }
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
            tbName.Focus();
        }

        private void bRemoveMod_Click(object sender, EventArgs e)
        {
            bsMods.RemoveCurrent();
            SetModEditable(false);
        }

        private void bAddSource_Click(object sender, EventArgs e)
        {
            bsSources.AddNew();
            SetSourceEditable(true);
        }

        private void bRemoveSource_Click(object sender, EventArgs e)
        {
            bsSources.RemoveCurrent();
            SetSourceEditable(false);
        }

        private void dgvSources_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = dgvSources.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    dgvSources.ClearSelection();
                    SetSourceEditable(false);
                }
                else if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
                {
                    SetSourceEditable(true);
                }
            }
        }

        private void cbManual_CheckedChanged(object sender, EventArgs e)
        {
            tbSourceVersion.Enabled = cbManual.Checked;
        }

        private void MarkMods()
        {
            for (int i = 0; i < mods.Count; i++)
                MarkMod(mods[i], i);
        }

        private void MarkMod(Mod mod, int index)
        {

            if (mod.State != ModState.NotTracking && mod.State != ModState.Undefined && 
                mod.Sources.Select( s => s.State).Where(state => state == SourceState.UnknownServer || 
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

        private void checkModsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModsManager.CheckUpdates();
            MarkMods();
            dgvMods.AutoResizeColumns();
        }
    }
}
