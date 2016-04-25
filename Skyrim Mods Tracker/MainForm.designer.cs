namespace SMT
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.dgvMods = new System.Windows.Forms.DataGridView();
            this.gbMod = new System.Windows.Forms.GroupBox();
            this.bModAdd = new System.Windows.Forms.Button();
            this.bModDelete = new System.Windows.Forms.Button();
            this.bBrowse = new System.Windows.Forms.Button();
            this.tbRoot = new System.Windows.Forms.TextBox();
            this.tbVersion = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lRoot = new System.Windows.Forms.Label();
            this.lVersion = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.serversToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdRoot = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvSources = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbSource = new System.Windows.Forms.GroupBox();
            this.bSourceAdd = new System.Windows.Forms.Button();
            this.bSourceDelete = new System.Windows.Forms.Button();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.cbManual = new System.Windows.Forms.CheckBox();
            this.tbSourceVersion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.lLanguage = new System.Windows.Forms.Label();
            this.lServer = new System.Windows.Forms.Label();
            this.bsSources = new System.Windows.Forms.BindingSource(this.components);
            this.bsMods = new System.Windows.Forms.BindingSource(this.components);
            this.serverDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.languageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rootDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMods)).BeginInit();
            this.gbMod.SuspendLayout();
            this.msMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSources)).BeginInit();
            this.panel1.SuspendLayout();
            this.gbSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsSources)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMods)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMods
            // 
            this.dgvMods.AllowUserToAddRows = false;
            this.dgvMods.AllowUserToDeleteRows = false;
            this.dgvMods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMods.AutoGenerateColumns = false;
            this.dgvMods.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMods.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMods.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMods.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.iDDataGridViewTextBoxColumn,
            this.versionDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn,
            this.rootDataGridViewTextBoxColumn});
            this.dgvMods.DataSource = this.bsMods;
            this.dgvMods.Location = new System.Drawing.Point(12, 27);
            this.dgvMods.MultiSelect = false;
            this.dgvMods.Name = "dgvMods";
            this.dgvMods.ReadOnly = true;
            this.dgvMods.RowHeadersVisible = false;
            this.dgvMods.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMods.Size = new System.Drawing.Size(626, 424);
            this.dgvMods.TabIndex = 0;
            this.dgvMods.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvMods_MouseDown);
            // 
            // gbMod
            // 
            this.gbMod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMod.Controls.Add(this.bModAdd);
            this.gbMod.Controls.Add(this.bModDelete);
            this.gbMod.Controls.Add(this.bBrowse);
            this.gbMod.Controls.Add(this.tbRoot);
            this.gbMod.Controls.Add(this.tbVersion);
            this.gbMod.Controls.Add(this.tbName);
            this.gbMod.Controls.Add(this.lRoot);
            this.gbMod.Controls.Add(this.lVersion);
            this.gbMod.Controls.Add(this.lName);
            this.gbMod.Location = new System.Drawing.Point(3, 3);
            this.gbMod.Name = "gbMod";
            this.gbMod.Size = new System.Drawing.Size(354, 134);
            this.gbMod.TabIndex = 1;
            this.gbMod.TabStop = false;
            this.gbMod.Text = "Mod";
            // 
            // bModAdd
            // 
            this.bModAdd.BackColor = System.Drawing.Color.YellowGreen;
            this.bModAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bModAdd.Location = new System.Drawing.Point(190, 105);
            this.bModAdd.Name = "bModAdd";
            this.bModAdd.Size = new System.Drawing.Size(75, 23);
            this.bModAdd.TabIndex = 4;
            this.bModAdd.Text = "Add";
            this.bModAdd.UseVisualStyleBackColor = false;
            this.bModAdd.Click += new System.EventHandler(this.bModAdd_Click);
            // 
            // bModDelete
            // 
            this.bModDelete.BackColor = System.Drawing.Color.IndianRed;
            this.bModDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bModDelete.Location = new System.Drawing.Point(271, 105);
            this.bModDelete.Name = "bModDelete";
            this.bModDelete.Size = new System.Drawing.Size(75, 23);
            this.bModDelete.TabIndex = 5;
            this.bModDelete.Text = "Delete";
            this.bModDelete.UseVisualStyleBackColor = false;
            this.bModDelete.Click += new System.EventHandler(this.bModDelete_Click);
            // 
            // bBrowse
            // 
            this.bBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.bBrowse.Location = new System.Drawing.Point(326, 69);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(20, 20);
            this.bBrowse.TabIndex = 3;
            this.bBrowse.Text = "...";
            this.bBrowse.UseVisualStyleBackColor = true;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // tbRoot
            // 
            this.tbRoot.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMods, "Root", true));
            this.tbRoot.Location = new System.Drawing.Point(51, 69);
            this.tbRoot.Name = "tbRoot";
            this.tbRoot.Size = new System.Drawing.Size(269, 20);
            this.tbRoot.TabIndex = 3;
            // 
            // tbVersion
            // 
            this.tbVersion.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMods, "Version", true));
            this.tbVersion.Location = new System.Drawing.Point(51, 43);
            this.tbVersion.Name = "tbVersion";
            this.tbVersion.Size = new System.Drawing.Size(295, 20);
            this.tbVersion.TabIndex = 2;
            // 
            // tbName
            // 
            this.tbName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMods, "Name", true));
            this.tbName.Location = new System.Drawing.Point(51, 17);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(295, 20);
            this.tbName.TabIndex = 1;
            // 
            // lRoot
            // 
            this.lRoot.AutoSize = true;
            this.lRoot.Location = new System.Drawing.Point(7, 72);
            this.lRoot.Name = "lRoot";
            this.lRoot.Size = new System.Drawing.Size(33, 13);
            this.lRoot.TabIndex = 0;
            this.lRoot.Text = "Root:";
            // 
            // lVersion
            // 
            this.lVersion.AutoSize = true;
            this.lVersion.Location = new System.Drawing.Point(7, 46);
            this.lVersion.Name = "lVersion";
            this.lVersion.Size = new System.Drawing.Size(45, 13);
            this.lVersion.TabIndex = 0;
            this.lVersion.Text = "Version:";
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(7, 20);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(38, 13);
            this.lName.TabIndex = 0;
            this.lName.Text = "Name:";
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serversToolStripMenuItem});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(1001, 24);
            this.msMenu.TabIndex = 2;
            this.msMenu.Text = "menuStrip1";
            // 
            // serversToolStripMenuItem
            // 
            this.serversToolStripMenuItem.Name = "serversToolStripMenuItem";
            this.serversToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.serversToolStripMenuItem.Text = "Servers";
            this.serversToolStripMenuItem.Click += new System.EventHandler(this.serversToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvSources);
            this.groupBox1.Location = new System.Drawing.Point(3, 143);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 146);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sources";
            // 
            // dgvSources
            // 
            this.dgvSources.AllowUserToAddRows = false;
            this.dgvSources.AllowUserToDeleteRows = false;
            this.dgvSources.AllowUserToResizeRows = false;
            this.dgvSources.AutoGenerateColumns = false;
            this.dgvSources.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSources.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSources.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serverDataGridViewTextBoxColumn,
            this.languageDataGridViewTextBoxColumn,
            this.versionDataGridViewTextBoxColumn1,
            this.pathDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn1});
            this.dgvSources.DataSource = this.bsSources;
            this.dgvSources.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvSources.Location = new System.Drawing.Point(10, 19);
            this.dgvSources.MultiSelect = false;
            this.dgvSources.Name = "dgvSources";
            this.dgvSources.ReadOnly = true;
            this.dgvSources.RowHeadersVisible = false;
            this.dgvSources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSources.Size = new System.Drawing.Size(336, 119);
            this.dgvSources.TabIndex = 0;
            this.dgvSources.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvSources_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbSource);
            this.panel1.Controls.Add(this.gbMod);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(641, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 427);
            this.panel1.TabIndex = 4;
            // 
            // gbSource
            // 
            this.gbSource.Controls.Add(this.bSourceAdd);
            this.gbSource.Controls.Add(this.bSourceDelete);
            this.gbSource.Controls.Add(this.tbURL);
            this.gbSource.Controls.Add(this.cbManual);
            this.gbSource.Controls.Add(this.tbSourceVersion);
            this.gbSource.Controls.Add(this.label1);
            this.gbSource.Controls.Add(this.cbLanguage);
            this.gbSource.Controls.Add(this.lLanguage);
            this.gbSource.Controls.Add(this.lServer);
            this.gbSource.Location = new System.Drawing.Point(3, 295);
            this.gbSource.Name = "gbSource";
            this.gbSource.Size = new System.Drawing.Size(354, 127);
            this.gbSource.TabIndex = 4;
            this.gbSource.TabStop = false;
            this.gbSource.Text = "Source";
            // 
            // bSourceAdd
            // 
            this.bSourceAdd.BackColor = System.Drawing.Color.YellowGreen;
            this.bSourceAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSourceAdd.Location = new System.Drawing.Point(190, 98);
            this.bSourceAdd.Name = "bSourceAdd";
            this.bSourceAdd.Size = new System.Drawing.Size(75, 23);
            this.bSourceAdd.TabIndex = 12;
            this.bSourceAdd.Text = "Add";
            this.bSourceAdd.UseVisualStyleBackColor = false;
            this.bSourceAdd.Click += new System.EventHandler(this.bSourceAdd_Click);
            // 
            // bSourceDelete
            // 
            this.bSourceDelete.BackColor = System.Drawing.Color.IndianRed;
            this.bSourceDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSourceDelete.Location = new System.Drawing.Point(271, 98);
            this.bSourceDelete.Name = "bSourceDelete";
            this.bSourceDelete.Size = new System.Drawing.Size(75, 23);
            this.bSourceDelete.TabIndex = 13;
            this.bSourceDelete.Text = "Delete";
            this.bSourceDelete.UseVisualStyleBackColor = false;
            this.bSourceDelete.Click += new System.EventHandler(this.bSourceDelete_Click);
            // 
            // tbURL
            // 
            this.tbURL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSources, "URL", true));
            this.tbURL.Location = new System.Drawing.Point(73, 19);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(273, 20);
            this.tbURL.TabIndex = 11;
            // 
            // cbManual
            // 
            this.cbManual.AutoSize = true;
            this.cbManual.Location = new System.Drawing.Point(331, 75);
            this.cbManual.Name = "cbManual";
            this.cbManual.Size = new System.Drawing.Size(15, 14);
            this.cbManual.TabIndex = 9;
            this.cbManual.UseVisualStyleBackColor = true;
            this.cbManual.CheckedChanged += new System.EventHandler(this.cbManual_CheckedChanged);
            // 
            // tbSourceVersion
            // 
            this.tbSourceVersion.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSources, "Version", true));
            this.tbSourceVersion.Location = new System.Drawing.Point(73, 72);
            this.tbSourceVersion.Name = "tbSourceVersion";
            this.tbSourceVersion.ReadOnly = true;
            this.tbSourceVersion.Size = new System.Drawing.Size(252, 20);
            this.tbSourceVersion.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Version:";
            // 
            // cbLanguage
            // 
            this.cbLanguage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSources, "Language", true));
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(73, 45);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(273, 21);
            this.cbLanguage.TabIndex = 5;
            // 
            // lLanguage
            // 
            this.lLanguage.AutoSize = true;
            this.lLanguage.Location = new System.Drawing.Point(7, 48);
            this.lLanguage.Name = "lLanguage";
            this.lLanguage.Size = new System.Drawing.Size(58, 13);
            this.lLanguage.TabIndex = 4;
            this.lLanguage.Text = "Language:";
            // 
            // lServer
            // 
            this.lServer.AutoSize = true;
            this.lServer.Location = new System.Drawing.Point(7, 22);
            this.lServer.Name = "lServer";
            this.lServer.Size = new System.Drawing.Size(32, 13);
            this.lServer.TabIndex = 2;
            this.lServer.Text = "URL:";
            // 
            // bsSources
            // 
            this.bsSources.DataSource = typeof(SMT.Models.ModSource);
            this.bsSources.CurrentChanged += new System.EventHandler(this.bsSourcesCurrentChanged);
            // 
            // bsMods
            // 
            this.bsMods.DataSource = typeof(SMT.Models.Mod);
            this.bsMods.CurrentChanged += new System.EventHandler(this.bsModsCurrentChanged);
            // 
            // serverDataGridViewTextBoxColumn
            // 
            this.serverDataGridViewTextBoxColumn.DataPropertyName = "Server";
            this.serverDataGridViewTextBoxColumn.HeaderText = "Server";
            this.serverDataGridViewTextBoxColumn.Name = "serverDataGridViewTextBoxColumn";
            this.serverDataGridViewTextBoxColumn.ReadOnly = true;
            this.serverDataGridViewTextBoxColumn.Width = 61;
            // 
            // languageDataGridViewTextBoxColumn
            // 
            this.languageDataGridViewTextBoxColumn.DataPropertyName = "Language";
            this.languageDataGridViewTextBoxColumn.HeaderText = "Language";
            this.languageDataGridViewTextBoxColumn.Name = "languageDataGridViewTextBoxColumn";
            this.languageDataGridViewTextBoxColumn.ReadOnly = true;
            this.languageDataGridViewTextBoxColumn.Width = 78;
            // 
            // versionDataGridViewTextBoxColumn1
            // 
            this.versionDataGridViewTextBoxColumn1.DataPropertyName = "Version";
            this.versionDataGridViewTextBoxColumn1.HeaderText = "Version";
            this.versionDataGridViewTextBoxColumn1.Name = "versionDataGridViewTextBoxColumn1";
            this.versionDataGridViewTextBoxColumn1.ReadOnly = true;
            this.versionDataGridViewTextBoxColumn1.Width = 65;
            // 
            // pathDataGridViewTextBoxColumn
            // 
            this.pathDataGridViewTextBoxColumn.DataPropertyName = "Path";
            this.pathDataGridViewTextBoxColumn.HeaderText = "Path";
            this.pathDataGridViewTextBoxColumn.Name = "pathDataGridViewTextBoxColumn";
            this.pathDataGridViewTextBoxColumn.ReadOnly = true;
            this.pathDataGridViewTextBoxColumn.Width = 52;
            // 
            // stateDataGridViewTextBoxColumn1
            // 
            this.stateDataGridViewTextBoxColumn1.DataPropertyName = "State";
            this.stateDataGridViewTextBoxColumn1.HeaderText = "State";
            this.stateDataGridViewTextBoxColumn1.Name = "stateDataGridViewTextBoxColumn1";
            this.stateDataGridViewTextBoxColumn1.ReadOnly = true;
            this.stateDataGridViewTextBoxColumn1.Width = 55;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 58;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Visible = false;
            this.iDDataGridViewTextBoxColumn.Width = 41;
            // 
            // versionDataGridViewTextBoxColumn
            // 
            this.versionDataGridViewTextBoxColumn.DataPropertyName = "Version";
            this.versionDataGridViewTextBoxColumn.HeaderText = "Version";
            this.versionDataGridViewTextBoxColumn.Name = "versionDataGridViewTextBoxColumn";
            this.versionDataGridViewTextBoxColumn.ReadOnly = true;
            this.versionDataGridViewTextBoxColumn.Width = 65;
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "State";
            this.stateDataGridViewTextBoxColumn.HeaderText = "State";
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            this.stateDataGridViewTextBoxColumn.ReadOnly = true;
            this.stateDataGridViewTextBoxColumn.Width = 55;
            // 
            // rootDataGridViewTextBoxColumn
            // 
            this.rootDataGridViewTextBoxColumn.DataPropertyName = "Root";
            this.rootDataGridViewTextBoxColumn.HeaderText = "Root";
            this.rootDataGridViewTextBoxColumn.Name = "rootDataGridViewTextBoxColumn";
            this.rootDataGridViewTextBoxColumn.ReadOnly = true;
            this.rootDataGridViewTextBoxColumn.Width = 53;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 451);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvMods);
            this.Controls.Add(this.msMenu);
            this.MainMenuStrip = this.msMenu;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(2000, 1000);
            this.MinimumSize = new System.Drawing.Size(630, 490);
            this.Name = "MainForm";
            this.Text = "Skyrim Mods Tracker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMods)).EndInit();
            this.gbMod.ResumeLayout(false);
            this.gbMod.PerformLayout();
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSources)).EndInit();
            this.panel1.ResumeLayout(false);
            this.gbSource.ResumeLayout(false);
            this.gbSource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsSources)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMods)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gbMod;
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem serversToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvMods;
        private System.Windows.Forms.TextBox tbVersion;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lRoot;
        private System.Windows.Forms.Label lVersion;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.OpenFileDialog ofdRoot;
        private System.Windows.Forms.Button bBrowse;
        private System.Windows.Forms.TextBox tbRoot;
        private System.Windows.Forms.Button bModAdd;
        private System.Windows.Forms.Button bModDelete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvSources;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbSource;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.CheckBox cbManual;
        private System.Windows.Forms.TextBox tbSourceVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Label lLanguage;
        private System.Windows.Forms.Label lServer;
        private System.Windows.Forms.Button bSourceAdd;
        private System.Windows.Forms.Button bSourceDelete;
        private System.Windows.Forms.BindingSource bsMods;
        private System.Windows.Forms.BindingSource bsSources;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rootDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn languageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn pathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn1;
    }
}

