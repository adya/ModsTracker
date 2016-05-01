using SMT.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMT.Utils;

namespace SMT
{
    public partial class PreferencesForm : Form
    {
        public PreferencesForm()
        {
            InitializeComponent();
            tbLocation.BindLabel(lLocationError);
            tbPattern.BindLabel(lPatternError);
            LoadPreferences();
            ValidateLocation();
            ValidatePattern();
        }

        private void LoadPreferences()
        {
            cbEnableScanning.Checked = SettingsManager.AutoScanMods;
            cbEnablePatterns.Checked = SettingsManager.PatternNaming;
            gbPattern.Enabled = cbEnablePatterns.Checked;
            cbRenameFiles.Checked = SettingsManager.AutoRename;

            tbLocation.Text = SettingsManager.ModsLocation;
            tbPattern.Text = SettingsManager.NamePattern;
            lPatternNames.Text = string.Format(lPatternNames.Text, SettingsManager.PATTERN_NAME_GROUP, SettingsManager.PATTERN_VERSION_GROUP, SettingsManager.PATTERN_LANGUAGE_GROUP);

            cbRecovery.Checked = SettingsManager.UseBackups;
            nudBackupLevel.Value = SettingsManager.BackupsLevel;
            
            cbRecovery.Checked = false;
            cbRecovery.Enabled = false;
            cbRecovery.Text = cbRecovery.Text + " (Not yet implementend)";
            gbBackups.Enabled = cbRecovery.Checked;
        }

        private void cbEnableScanning_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.AutoScanMods = cbEnableScanning.Checked;
        }

        private void cbEnablePatterns_CheckedChanged(object sender, EventArgs e)
        {
            gbPattern.Enabled = cbEnablePatterns.Checked;
            SettingsManager.PatternNaming = cbEnablePatterns.Checked;
        }

        private void cbUnlockPattern_CheckedChanged(object sender, EventArgs e)
        {
            tbPattern.ReadOnly = !cbUnlockPattern.Checked;
        }

        private void cbRecovery_CheckedChanged(object sender, EventArgs e)
        {
            gbBackups.Enabled = cbRecovery.Checked;
            SettingsManager.UseBackups = cbRecovery.Checked;
        }

        private void PreferencesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SettingsManager.IsValid)
                SettingsManager.Sync();
            else {
                MessageBox.Show("Please, fix invalid settings before continuing.");
                e.Cancel = true;
            }
        }

        private void cbRenameFiles_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.AutoRename = cbRenameFiles.Checked;
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            fbdModLocation.RootFolder = Environment.SpecialFolder.MyComputer;
            if (SettingsManager.HasValidModsLocation) fbdModLocation.SelectedPath = SettingsManager.ModsLocation;
            if (DialogResult.OK == fbdModLocation.ShowDialog())
                tbLocation.Text = fbdModLocation.SelectedPath;
        }

        private void tbLocation_TextChanged(object sender, EventArgs e)
        {
            SettingsManager.ModsLocation = tbLocation.Text;
            ValidateLocation();
        }

       

        private void tbPattern_TextChanged(object sender, EventArgs e)
        {
            SettingsManager.NamePattern = tbPattern.Text;
            ValidatePattern();
        }

        private void ValidatePattern()
        {
            if (SettingsManager.PatternNaming)
            {
                if (!SettingsManager.HasValidNamePattern)
                    tbPattern.SetError("Invalid pattern");
                else if (!SettingsManager.HasFullNamePattern)
                tbPattern.SetWarning(string.Format("Some of regex groups are missing (use groups <{0}>, <{1}>, <{2}>)", SettingsManager.PATTERN_NAME_GROUP, SettingsManager.PATTERN_VERSION_GROUP, SettingsManager.PATTERN_LANGUAGE_GROUP));
                else
                    tbPattern.SetValidMessage();
            }
            else tbPattern.SetValidMessage();
        }

        private void ValidateLocation()
        {
            if (!SettingsManager.HasValidModsLocation)
                tbLocation.SetError("Invalid path");
            else
                tbLocation.SetValidMessage();
        }

        private void bScanNow_Click(object sender, EventArgs e)
        {
            if (SettingsManager.HasValidModsLocation)
            { 
                DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
                MessageBox.Show("Please, set valid path to the mods folder.");
        }
    }
}
