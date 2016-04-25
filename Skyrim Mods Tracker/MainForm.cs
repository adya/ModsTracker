using SMT.Managers;
using SMT.Models;
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
        private List<Mod> mods;
        private Mod selectedMod;
        private List<ModSource> sources;
        private ModSource selectedSource;

        public MainForm()
        {
            InitializeComponent();
            mods = ModsManager.Mods.ToList();
            bsMods.DataSource = mods;
            ofdRoot.InitialDirectory = Environment.CurrentDirectory;

            cbLanguage.DataSource = Enum.GetValues(typeof(Language));

            SetModEditable(false);
        }

        private void bsModsCurrentChanged(object sender, EventArgs e)
        {
            SaveSources();
            selectedMod = bsMods.Current as Mod;
            sources = selectedMod.Sources.ToList();
            bsSources.DataSource = sources;
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
            StorageManager.Sync();
        }

        private void serversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ServersForm().ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dgvMods.ClearSelection();
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
            bSourceAdd.Enabled = isEditable;
            bSourceDelete.Enabled = isEditable;
            if (isEditable) bsMods.ResumeBinding();
            else
            {
                bsMods.SuspendBinding();
                tbName.Clear();
                tbRoot.Clear();
                tbVersion.Clear();
                SetSourceEditable(false);
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

        private void bModAdd_Click(object sender, EventArgs e)
        {
            bsMods.AddNew();
            SetModEditable(true);
            tbName.Focus();
            
        }

        private void bModDelete_Click(object sender, EventArgs e)
        {
            bsMods.RemoveCurrent();
            SetModEditable(false);
        }

        private void bSourceAdd_Click(object sender, EventArgs e)
        {
            bsSources.AddNew();
            SetSourceEditable(true);
        }

        private void bSourceDelete_Click(object sender, EventArgs e)
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

        private void cbServer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
