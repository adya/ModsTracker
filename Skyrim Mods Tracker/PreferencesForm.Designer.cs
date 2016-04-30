namespace SMT
{
    partial class PreferencesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbPattern = new System.Windows.Forms.GroupBox();
            this.cbRenameFiles = new System.Windows.Forms.CheckBox();
            this.cbUnlockPattern = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPattern = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbEnableScanning = new System.Windows.Forms.CheckBox();
            this.gbScanning = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bBrowse = new SMT.Utils.StateButton();
            this.tbLocation = new System.Windows.Forms.TextBox();
            this.cbEnablePatterns = new System.Windows.Forms.CheckBox();
            this.cbRecovery = new System.Windows.Forms.CheckBox();
            this.gbBackups = new System.Windows.Forms.GroupBox();
            this.nudBackupLevel = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lbBackups = new System.Windows.Forms.ListBox();
            this.bRecover = new SMT.Utils.StateButton();
            this.label5 = new System.Windows.Forms.Label();
            this.bDelete = new SMT.Utils.StateButton();
            this.panel1.SuspendLayout();
            this.gbPattern.SuspendLayout();
            this.gbScanning.SuspendLayout();
            this.gbBackups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBackupLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbBackups);
            this.panel1.Controls.Add(this.cbRecovery);
            this.panel1.Controls.Add(this.gbPattern);
            this.panel1.Controls.Add(this.cbEnableScanning);
            this.panel1.Controls.Add(this.gbScanning);
            this.panel1.Controls.Add(this.cbEnablePatterns);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(454, 363);
            this.panel1.TabIndex = 1;
            // 
            // gbPattern
            // 
            this.gbPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPattern.Controls.Add(this.cbRenameFiles);
            this.gbPattern.Controls.Add(this.cbUnlockPattern);
            this.gbPattern.Controls.Add(this.label4);
            this.gbPattern.Controls.Add(this.tbPattern);
            this.gbPattern.Controls.Add(this.label3);
            this.gbPattern.Location = new System.Drawing.Point(12, 115);
            this.gbPattern.Name = "gbPattern";
            this.gbPattern.Size = new System.Drawing.Size(432, 90);
            this.gbPattern.TabIndex = 11;
            this.gbPattern.TabStop = false;
            this.gbPattern.Text = "Pattern";
            // 
            // cbRenameFiles
            // 
            this.cbRenameFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbRenameFiles.AutoSize = true;
            this.cbRenameFiles.Location = new System.Drawing.Point(9, 67);
            this.cbRenameFiles.Name = "cbRenameFiles";
            this.cbRenameFiles.Size = new System.Drawing.Size(249, 17);
            this.cbRenameFiles.TabIndex = 10;
            this.cbRenameFiles.Text = "Update file names when updating mods version";
            this.cbRenameFiles.UseVisualStyleBackColor = true;
            // 
            // cbUnlockPattern
            // 
            this.cbUnlockPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbUnlockPattern.AutoSize = true;
            this.cbUnlockPattern.Location = new System.Drawing.Point(407, 22);
            this.cbUnlockPattern.Name = "cbUnlockPattern";
            this.cbUnlockPattern.Size = new System.Drawing.Size(15, 14);
            this.cbUnlockPattern.TabIndex = 9;
            this.cbUnlockPattern.UseVisualStyleBackColor = true;
            this.cbUnlockPattern.CheckedChanged += new System.EventHandler(this.cbUnlockPattern_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(354, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Regular expressions pattern (use groups <name>, <version>, <language>)";
            // 
            // tbPattern
            // 
            this.tbPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPattern.Location = new System.Drawing.Point(92, 19);
            this.tbPattern.Name = "tbPattern";
            this.tbPattern.ReadOnly = true;
            this.tbPattern.Size = new System.Drawing.Size(304, 20);
            this.tbPattern.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Name pattern:";
            // 
            // cbEnableScanning
            // 
            this.cbEnableScanning.AutoSize = true;
            this.cbEnableScanning.Location = new System.Drawing.Point(21, 12);
            this.cbEnableScanning.Name = "cbEnableScanning";
            this.cbEnableScanning.Size = new System.Drawing.Size(275, 17);
            this.cbEnableScanning.TabIndex = 10;
            this.cbEnableScanning.Text = "Scan mods in specified directory on Tracker\'s launch";
            this.cbEnableScanning.UseVisualStyleBackColor = true;
            this.cbEnableScanning.CheckedChanged += new System.EventHandler(this.cbEnableScanning_CheckedChanged);
            // 
            // gbScanning
            // 
            this.gbScanning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbScanning.Controls.Add(this.label1);
            this.gbScanning.Controls.Add(this.bBrowse);
            this.gbScanning.Controls.Add(this.tbLocation);
            this.gbScanning.Location = new System.Drawing.Point(12, 35);
            this.gbScanning.Name = "gbScanning";
            this.gbScanning.Size = new System.Drawing.Size(432, 51);
            this.gbScanning.TabIndex = 9;
            this.gbScanning.TabStop = false;
            this.gbScanning.Text = "Mod Scanning";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mods Location:";
            // 
            // bBrowse
            // 
            this.bBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bBrowse.BackColor = System.Drawing.Color.SkyBlue;
            this.bBrowse.DisabledBackColor = System.Drawing.Color.LightGray;
            this.bBrowse.DisabledForeColor = System.Drawing.Color.DimGray;
            this.bBrowse.EnabledBackColor = System.Drawing.Color.SkyBlue;
            this.bBrowse.EnabledForeColor = System.Drawing.Color.Black;
            this.bBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 4.25F);
            this.bBrowse.ForeColor = System.Drawing.Color.Black;
            this.bBrowse.Location = new System.Drawing.Point(402, 19);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(20, 20);
            this.bBrowse.TabIndex = 6;
            this.bBrowse.Text = "..";
            this.bBrowse.UseVisualStyleBackColor = false;
            // 
            // tbLocation
            // 
            this.tbLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLocation.Location = new System.Drawing.Point(92, 19);
            this.tbLocation.Name = "tbLocation";
            this.tbLocation.Size = new System.Drawing.Size(304, 20);
            this.tbLocation.TabIndex = 5;
            // 
            // cbEnablePatterns
            // 
            this.cbEnablePatterns.AutoSize = true;
            this.cbEnablePatterns.Location = new System.Drawing.Point(21, 92);
            this.cbEnablePatterns.Name = "cbEnablePatterns";
            this.cbEnablePatterns.Size = new System.Drawing.Size(150, 17);
            this.cbEnablePatterns.TabIndex = 0;
            this.cbEnablePatterns.Text = "Enable pattern files names";
            this.cbEnablePatterns.UseVisualStyleBackColor = true;
            this.cbEnablePatterns.CheckedChanged += new System.EventHandler(this.cbEnablePatterns_CheckedChanged);
            // 
            // cbRecovery
            // 
            this.cbRecovery.AutoSize = true;
            this.cbRecovery.Location = new System.Drawing.Point(21, 211);
            this.cbRecovery.Name = "cbRecovery";
            this.cbRecovery.Size = new System.Drawing.Size(89, 17);
            this.cbRecovery.TabIndex = 12;
            this.cbRecovery.Text = "Use backups";
            this.cbRecovery.UseVisualStyleBackColor = true;
            this.cbRecovery.CheckedChanged += new System.EventHandler(this.cbRecovery_CheckedChanged);
            // 
            // gbBackups
            // 
            this.gbBackups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBackups.Controls.Add(this.bDelete);
            this.gbBackups.Controls.Add(this.label5);
            this.gbBackups.Controls.Add(this.bRecover);
            this.gbBackups.Controls.Add(this.lbBackups);
            this.gbBackups.Controls.Add(this.label2);
            this.gbBackups.Controls.Add(this.nudBackupLevel);
            this.gbBackups.Location = new System.Drawing.Point(12, 234);
            this.gbBackups.Name = "gbBackups";
            this.gbBackups.Size = new System.Drawing.Size(432, 120);
            this.gbBackups.TabIndex = 13;
            this.gbBackups.TabStop = false;
            this.gbBackups.Text = "Backup";
            // 
            // nudBackupLevel
            // 
            this.nudBackupLevel.Location = new System.Drawing.Point(106, 19);
            this.nudBackupLevel.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudBackupLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBackupLevel.Name = "nudBackupLevel";
            this.nudBackupLevel.Size = new System.Drawing.Size(53, 20);
            this.nudBackupLevel.TabIndex = 0;
            this.nudBackupLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Max backup level:";
            // 
            // lbBackups
            // 
            this.lbBackups.FormattingEnabled = true;
            this.lbBackups.Location = new System.Drawing.Point(220, 19);
            this.lbBackups.Name = "lbBackups";
            this.lbBackups.Size = new System.Drawing.Size(202, 69);
            this.lbBackups.TabIndex = 9;
            // 
            // bRecover
            // 
            this.bRecover.BackColor = System.Drawing.Color.ForestGreen;
            this.bRecover.DisabledBackColor = System.Drawing.Color.LightGray;
            this.bRecover.DisabledForeColor = System.Drawing.Color.DimGray;
            this.bRecover.EnabledBackColor = System.Drawing.Color.ForestGreen;
            this.bRecover.EnabledForeColor = System.Drawing.SystemColors.ControlText;
            this.bRecover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRecover.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bRecover.Location = new System.Drawing.Point(220, 91);
            this.bRecover.Name = "bRecover";
            this.bRecover.Size = new System.Drawing.Size(132, 23);
            this.bRecover.TabIndex = 10;
            this.bRecover.Text = "Recover";
            this.bRecover.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 66);
            this.label5.TabIndex = 11;
            this.label5.Text = "This option allows to creating backups. Max level will limit total number of back" +
    "ups which can be written without override.";
            // 
            // bDelete
            // 
            this.bDelete.BackColor = System.Drawing.Color.IndianRed;
            this.bDelete.DisabledBackColor = System.Drawing.Color.LightGray;
            this.bDelete.DisabledForeColor = System.Drawing.Color.DimGray;
            this.bDelete.EnabledBackColor = System.Drawing.Color.IndianRed;
            this.bDelete.EnabledForeColor = System.Drawing.SystemColors.ControlText;
            this.bDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDelete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bDelete.Location = new System.Drawing.Point(358, 91);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(64, 23);
            this.bDelete.TabIndex = 12;
            this.bDelete.Text = "Delete";
            this.bDelete.UseVisualStyleBackColor = false;
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 363);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(470, 250);
            this.Name = "PreferencesForm";
            this.Text = "Preferences";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbPattern.ResumeLayout(false);
            this.gbPattern.PerformLayout();
            this.gbScanning.ResumeLayout(false);
            this.gbScanning.PerformLayout();
            this.gbBackups.ResumeLayout(false);
            this.gbBackups.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBackupLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbPattern;
        private System.Windows.Forms.CheckBox cbRenameFiles;
        private System.Windows.Forms.CheckBox cbUnlockPattern;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPattern;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbEnableScanning;
        private System.Windows.Forms.GroupBox gbScanning;
        private System.Windows.Forms.Label label1;
        private Utils.StateButton bBrowse;
        private System.Windows.Forms.TextBox tbLocation;
        private System.Windows.Forms.CheckBox cbEnablePatterns;
        private System.Windows.Forms.CheckBox cbRecovery;
        private System.Windows.Forms.GroupBox gbBackups;
        private Utils.StateButton bRecover;
        private System.Windows.Forms.ListBox lbBackups;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudBackupLevel;
        private Utils.StateButton bDelete;
        private System.Windows.Forms.Label label5;
    }
}