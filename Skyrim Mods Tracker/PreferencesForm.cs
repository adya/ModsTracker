using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMT
{
    public partial class PreferencesForm : Form
    {
        public PreferencesForm()
        {
            InitializeComponent();
            LoadPreferences();
        }

        private void LoadPreferences()
        {
            gbScanning.Enabled = cbEnableScanning.Checked;
            gbPattern.Enabled = cbEnablePatterns.Checked;
            gbBackups.Enabled = cbRecovery.Checked;
        }

        private void cbEnableScanning_CheckedChanged(object sender, EventArgs e)
        {
            gbScanning.Enabled = cbEnableScanning.Checked;
        }

        private void cbEnablePatterns_CheckedChanged(object sender, EventArgs e)
        {
            gbPattern.Enabled = cbEnablePatterns.Checked;
        }

        private void cbUnlockPattern_CheckedChanged(object sender, EventArgs e)
        {
            tbPattern.ReadOnly = !cbUnlockPattern.Checked;
        }

        private void cbRecovery_CheckedChanged(object sender, EventArgs e)
        {
            gbBackups.Enabled = cbRecovery.Checked;
        }
    }
}
