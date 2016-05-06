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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dgvMods = new System.Windows.Forms.DataGridView();
            this.gbMod = new System.Windows.Forms.GroupBox();
            this.lRootError = new System.Windows.Forms.Label();
            this.lVersionError = new System.Windows.Forms.Label();
            this.lNameError = new System.Windows.Forms.Label();
            this.tbRoot = new System.Windows.Forms.TextBox();
            this.tbVersion = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lRoot = new System.Windows.Forms.Label();
            this.lVersion = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.ofdRoot = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvSources = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbSource = new System.Windows.Forms.GroupBox();
            this.lDummyFocus = new System.Windows.Forms.Label();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.cbManual = new System.Windows.Forms.CheckBox();
            this.lSrcVersionError = new System.Windows.Forms.Label();
            this.lURLError = new System.Windows.Forms.Label();
            this.tbSourceVersion = new System.Windows.Forms.TextBox();
            this.lSrcVersion = new System.Windows.Forms.Label();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.lLanguage = new System.Windows.Forms.Label();
            this.lServer = new System.Windows.Forms.Label();
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.spbStatusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.slStatusTitle = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serversToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn5 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn6 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn7 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn8 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn9 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn10 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn11 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn12 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn13 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn14 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn15 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn16 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.State = new System.Windows.Forms.DataGridViewButtonColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewLinkColumn1 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.bUpdate = new SMT.Utils.StateButton();
            this.bAddSource = new SMT.Utils.StateButton();
            this.bRemoveSource = new SMT.Utils.StateButton();
            this.bsSources = new System.Windows.Forms.BindingSource(this.components);
            this.bAddMod = new SMT.Utils.StateButton();
            this.bRemoveMod = new SMT.Utils.StateButton();
            this.bBrowse = new SMT.Utils.StateButton();
            this.bsMods = new System.Windows.Forms.BindingSource(this.components);
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverDataGridViewLinkColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.languageDataGridViewTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionModDataGridViewTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateModDataGridViewTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pathDataGridViewLinkColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMods)).BeginInit();
            this.gbMod.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSources)).BeginInit();
            this.panel1.SuspendLayout();
            this.gbSource.SuspendLayout();
            this.ssStatus.SuspendLayout();
            this.msMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsSources)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMods)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMods
            // 
            this.dgvMods.AllowUserToAddRows = false;
            this.dgvMods.AllowUserToDeleteRows = false;
            this.dgvMods.AllowUserToOrderColumns = true;
            this.dgvMods.AllowUserToResizeRows = false;
            this.dgvMods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMods.AutoGenerateColumns = false;
            this.dgvMods.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMods.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMods.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMods.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.versionDataGridViewTextBoxColumn,
            this.State,
            this.FileName,
            this.iDDataGridViewTextBoxColumn});
            this.dgvMods.DataSource = this.bsMods;
            this.dgvMods.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMods.Location = new System.Drawing.Point(12, 27);
            this.dgvMods.Name = "dgvMods";
            this.dgvMods.ReadOnly = true;
            this.dgvMods.RowHeadersVisible = false;
            this.dgvMods.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMods.Size = new System.Drawing.Size(698, 537);
            this.dgvMods.TabIndex = 10;
            this.dgvMods.TabStop = false;
            this.dgvMods.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMods_CellContentClick);
            this.dgvMods.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvMods_MouseDown);
            // 
            // gbMod
            // 
            this.gbMod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMod.Controls.Add(this.lRootError);
            this.gbMod.Controls.Add(this.lVersionError);
            this.gbMod.Controls.Add(this.lNameError);
            this.gbMod.Controls.Add(this.bAddMod);
            this.gbMod.Controls.Add(this.bRemoveMod);
            this.gbMod.Controls.Add(this.bBrowse);
            this.gbMod.Controls.Add(this.tbRoot);
            this.gbMod.Controls.Add(this.tbVersion);
            this.gbMod.Controls.Add(this.tbName);
            this.gbMod.Controls.Add(this.lRoot);
            this.gbMod.Controls.Add(this.lVersion);
            this.gbMod.Controls.Add(this.lName);
            this.gbMod.Location = new System.Drawing.Point(3, 3);
            this.gbMod.Name = "gbMod";
            this.gbMod.Size = new System.Drawing.Size(406, 174);
            this.gbMod.TabIndex = 1;
            this.gbMod.TabStop = false;
            this.gbMod.Text = "Mod";
            // 
            // lRootError
            // 
            this.lRootError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lRootError.AutoSize = true;
            this.lRootError.Location = new System.Drawing.Point(61, 123);
            this.lRootError.Name = "lRootError";
            this.lRootError.Size = new System.Drawing.Size(29, 13);
            this.lRootError.TabIndex = 7;
            this.lRootError.Text = "Error";
            this.lRootError.Visible = false;
            // 
            // lVersionError
            // 
            this.lVersionError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lVersionError.AutoSize = true;
            this.lVersionError.Location = new System.Drawing.Point(61, 83);
            this.lVersionError.Name = "lVersionError";
            this.lVersionError.Size = new System.Drawing.Size(29, 13);
            this.lVersionError.TabIndex = 7;
            this.lVersionError.Text = "Error";
            this.lVersionError.Visible = false;
            // 
            // lNameError
            // 
            this.lNameError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lNameError.AutoSize = true;
            this.lNameError.Location = new System.Drawing.Point(61, 44);
            this.lNameError.Name = "lNameError";
            this.lNameError.Size = new System.Drawing.Size(29, 13);
            this.lNameError.TabIndex = 7;
            this.lNameError.Text = "Error";
            this.lNameError.Visible = false;
            // 
            // tbRoot
            // 
            this.tbRoot.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbRoot.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbRoot.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMods, "FileName", true));
            this.tbRoot.Location = new System.Drawing.Point(55, 100);
            this.tbRoot.Name = "tbRoot";
            this.tbRoot.Size = new System.Drawing.Size(265, 20);
            this.tbRoot.TabIndex = 3;
            // 
            // tbVersion
            // 
            this.tbVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVersion.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMods, "Version", true));
            this.tbVersion.Location = new System.Drawing.Point(55, 60);
            this.tbVersion.Name = "tbVersion";
            this.tbVersion.Size = new System.Drawing.Size(342, 20);
            this.tbVersion.TabIndex = 2;
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMods, "Name", true));
            this.tbName.Location = new System.Drawing.Point(55, 20);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(342, 20);
            this.tbName.TabIndex = 1;
            // 
            // lRoot
            // 
            this.lRoot.AutoSize = true;
            this.lRoot.Location = new System.Drawing.Point(7, 103);
            this.lRoot.Name = "lRoot";
            this.lRoot.Size = new System.Drawing.Size(26, 13);
            this.lRoot.TabIndex = 0;
            this.lRoot.Text = "File:";
            // 
            // lVersion
            // 
            this.lVersion.AutoSize = true;
            this.lVersion.Location = new System.Drawing.Point(7, 63);
            this.lVersion.Name = "lVersion";
            this.lVersion.Size = new System.Drawing.Size(45, 13);
            this.lVersion.TabIndex = 0;
            this.lVersion.Text = "Version:";
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(7, 23);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(38, 13);
            this.lName.TabIndex = 0;
            this.lName.Text = "Name:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvSources);
            this.groupBox1.Location = new System.Drawing.Point(3, 183);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(406, 175);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sources";
            // 
            // dgvSources
            // 
            this.dgvSources.AllowDrop = true;
            this.dgvSources.AllowUserToAddRows = false;
            this.dgvSources.AllowUserToOrderColumns = true;
            this.dgvSources.AllowUserToResizeRows = false;
            this.dgvSources.AutoGenerateColumns = false;
            this.dgvSources.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSources.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSources.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serverDataGridViewLinkColumn,
            this.languageDataGridViewTextColumn,
            this.versionModDataGridViewTextColumn,
            this.stateModDataGridViewTextColumn,
            this.pathDataGridViewLinkColumn});
            this.dgvSources.DataSource = this.bsSources;
            this.dgvSources.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvSources.Location = new System.Drawing.Point(10, 19);
            this.dgvSources.Name = "dgvSources";
            this.dgvSources.ReadOnly = true;
            this.dgvSources.RowHeadersVisible = false;
            this.dgvSources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSources.Size = new System.Drawing.Size(387, 150);
            this.dgvSources.TabIndex = 11;
            this.dgvSources.TabStop = false;
            this.dgvSources.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSources_CellContentClick);
            this.dgvSources.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvSources_DragDrop);
            this.dgvSources.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgvSources_DragEnter);
            this.dgvSources.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvSources_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbSource);
            this.panel1.Controls.Add(this.gbMod);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(716, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(412, 545);
            this.panel1.TabIndex = 4;
            // 
            // gbSource
            // 
            this.gbSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSource.Controls.Add(this.lDummyFocus);
            this.gbSource.Controls.Add(this.bUpdate);
            this.gbSource.Controls.Add(this.bAddSource);
            this.gbSource.Controls.Add(this.bRemoveSource);
            this.gbSource.Controls.Add(this.tbURL);
            this.gbSource.Controls.Add(this.cbManual);
            this.gbSource.Controls.Add(this.lSrcVersionError);
            this.gbSource.Controls.Add(this.lURLError);
            this.gbSource.Controls.Add(this.tbSourceVersion);
            this.gbSource.Controls.Add(this.lSrcVersion);
            this.gbSource.Controls.Add(this.cbLanguage);
            this.gbSource.Controls.Add(this.lLanguage);
            this.gbSource.Controls.Add(this.lServer);
            this.gbSource.Location = new System.Drawing.Point(3, 364);
            this.gbSource.Name = "gbSource";
            this.gbSource.Size = new System.Drawing.Size(406, 169);
            this.gbSource.TabIndex = 4;
            this.gbSource.TabStop = false;
            this.gbSource.Text = "Source";
            // 
            // lDummyFocus
            // 
            this.lDummyFocus.AutoSize = true;
            this.lDummyFocus.Location = new System.Drawing.Point(145, 145);
            this.lDummyFocus.Name = "lDummyFocus";
            this.lDummyFocus.Size = new System.Drawing.Size(74, 13);
            this.lDummyFocus.TabIndex = 0;
            this.lDummyFocus.Text = "Dummy Focus";
            this.lDummyFocus.Visible = false;
            // 
            // tbURL
            // 
            this.tbURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbURL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSources, "URL", true));
            this.tbURL.Location = new System.Drawing.Point(70, 20);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(327, 20);
            this.tbURL.TabIndex = 11;
            // 
            // cbManual
            // 
            this.cbManual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbManual.AutoSize = true;
            this.cbManual.Location = new System.Drawing.Point(382, 103);
            this.cbManual.Name = "cbManual";
            this.cbManual.Size = new System.Drawing.Size(15, 14);
            this.cbManual.TabIndex = 9;
            this.cbManual.UseVisualStyleBackColor = true;
            this.cbManual.CheckedChanged += new System.EventHandler(this.cbManual_CheckedChanged);
            // 
            // lSrcVersionError
            // 
            this.lSrcVersionError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lSrcVersionError.AutoSize = true;
            this.lSrcVersionError.Location = new System.Drawing.Point(76, 123);
            this.lSrcVersionError.Name = "lSrcVersionError";
            this.lSrcVersionError.Size = new System.Drawing.Size(29, 13);
            this.lSrcVersionError.TabIndex = 7;
            this.lSrcVersionError.Text = "Error";
            this.lSrcVersionError.Visible = false;
            // 
            // lURLError
            // 
            this.lURLError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lURLError.AutoSize = true;
            this.lURLError.Location = new System.Drawing.Point(76, 44);
            this.lURLError.Name = "lURLError";
            this.lURLError.Size = new System.Drawing.Size(29, 13);
            this.lURLError.TabIndex = 7;
            this.lURLError.Text = "Error";
            this.lURLError.Visible = false;
            // 
            // tbSourceVersion
            // 
            this.tbSourceVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSourceVersion.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSources, "Version", true));
            this.tbSourceVersion.Location = new System.Drawing.Point(70, 100);
            this.tbSourceVersion.Name = "tbSourceVersion";
            this.tbSourceVersion.Size = new System.Drawing.Size(306, 20);
            this.tbSourceVersion.TabIndex = 8;
            // 
            // lSrcVersion
            // 
            this.lSrcVersion.AutoSize = true;
            this.lSrcVersion.Location = new System.Drawing.Point(7, 103);
            this.lSrcVersion.Name = "lSrcVersion";
            this.lSrcVersion.Size = new System.Drawing.Size(45, 13);
            this.lSrcVersion.TabIndex = 7;
            this.lSrcVersion.Text = "Version:";
            // 
            // cbLanguage
            // 
            this.cbLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLanguage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSources, "Language", true));
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(70, 60);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(327, 21);
            this.cbLanguage.TabIndex = 5;
            // 
            // lLanguage
            // 
            this.lLanguage.AutoSize = true;
            this.lLanguage.Location = new System.Drawing.Point(7, 63);
            this.lLanguage.Name = "lLanguage";
            this.lLanguage.Size = new System.Drawing.Size(58, 13);
            this.lLanguage.TabIndex = 4;
            this.lLanguage.Text = "Language:";
            // 
            // lServer
            // 
            this.lServer.AutoSize = true;
            this.lServer.Location = new System.Drawing.Point(7, 23);
            this.lServer.Name = "lServer";
            this.lServer.Size = new System.Drawing.Size(32, 13);
            this.lServer.TabIndex = 2;
            this.lServer.Text = "URL:";
            // 
            // ssStatus
            // 
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spbStatusProgress,
            this.slStatusTitle});
            this.ssStatus.Location = new System.Drawing.Point(0, 547);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(716, 22);
            this.ssStatus.TabIndex = 11;
            this.ssStatus.Text = "statusStrip1";
            // 
            // spbStatusProgress
            // 
            this.spbStatusProgress.Name = "spbStatusProgress";
            this.spbStatusProgress.Size = new System.Drawing.Size(100, 16);
            this.spbStatusProgress.Visible = false;
            // 
            // slStatusTitle
            // 
            this.slStatusTitle.Name = "slStatusTitle";
            this.slStatusTitle.Size = new System.Drawing.Size(39, 17);
            this.slStatusTitle.Text = "Status";
            // 
            // checkModsToolStripMenuItem
            // 
            this.checkModsToolStripMenuItem.Name = "checkModsToolStripMenuItem";
            this.checkModsToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.checkModsToolStripMenuItem.Text = "Check mods";
            this.checkModsToolStripMenuItem.Click += new System.EventHandler(this.checkModsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serversToolStripMenuItem,
            this.preferencesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // serversToolStripMenuItem
            // 
            this.serversToolStripMenuItem.Name = "serversToolStripMenuItem";
            this.serversToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.serversToolStripMenuItem.Text = "Servers";
            this.serversToolStripMenuItem.Click += new System.EventHandler(this.serversToolStripMenuItem_Click);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkModsToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(1128, 24);
            this.msMenu.TabIndex = 2;
            this.msMenu.Text = "menuStrip1";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.DataPropertyName = "State";
            this.dataGridViewButtonColumn1.HeaderText = "State";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Width = 36;
            // 
            // dataGridViewButtonColumn2
            // 
            this.dataGridViewButtonColumn2.DataPropertyName = "State";
            this.dataGridViewButtonColumn2.HeaderText = "State";
            this.dataGridViewButtonColumn2.Name = "dataGridViewButtonColumn2";
            this.dataGridViewButtonColumn2.ReadOnly = true;
            this.dataGridViewButtonColumn2.Width = 36;
            // 
            // dataGridViewButtonColumn3
            // 
            this.dataGridViewButtonColumn3.DataPropertyName = "State";
            this.dataGridViewButtonColumn3.HeaderText = "State";
            this.dataGridViewButtonColumn3.Name = "dataGridViewButtonColumn3";
            this.dataGridViewButtonColumn3.ReadOnly = true;
            this.dataGridViewButtonColumn3.Width = 36;
            // 
            // dataGridViewButtonColumn4
            // 
            this.dataGridViewButtonColumn4.DataPropertyName = "State";
            this.dataGridViewButtonColumn4.HeaderText = "State";
            this.dataGridViewButtonColumn4.Name = "dataGridViewButtonColumn4";
            this.dataGridViewButtonColumn4.ReadOnly = true;
            this.dataGridViewButtonColumn4.Width = 36;
            // 
            // dataGridViewButtonColumn5
            // 
            this.dataGridViewButtonColumn5.DataPropertyName = "State";
            this.dataGridViewButtonColumn5.HeaderText = "State";
            this.dataGridViewButtonColumn5.Name = "dataGridViewButtonColumn5";
            this.dataGridViewButtonColumn5.ReadOnly = true;
            this.dataGridViewButtonColumn5.Width = 36;
            // 
            // dataGridViewButtonColumn6
            // 
            this.dataGridViewButtonColumn6.DataPropertyName = "State";
            this.dataGridViewButtonColumn6.HeaderText = "State";
            this.dataGridViewButtonColumn6.Name = "dataGridViewButtonColumn6";
            this.dataGridViewButtonColumn6.ReadOnly = true;
            this.dataGridViewButtonColumn6.Width = 36;
            // 
            // dataGridViewButtonColumn7
            // 
            this.dataGridViewButtonColumn7.DataPropertyName = "State";
            this.dataGridViewButtonColumn7.HeaderText = "State";
            this.dataGridViewButtonColumn7.Name = "dataGridViewButtonColumn7";
            this.dataGridViewButtonColumn7.ReadOnly = true;
            this.dataGridViewButtonColumn7.Width = 36;
            // 
            // dataGridViewButtonColumn8
            // 
            this.dataGridViewButtonColumn8.DataPropertyName = "State";
            this.dataGridViewButtonColumn8.HeaderText = "State";
            this.dataGridViewButtonColumn8.Name = "dataGridViewButtonColumn8";
            this.dataGridViewButtonColumn8.ReadOnly = true;
            this.dataGridViewButtonColumn8.Width = 36;
            // 
            // dataGridViewButtonColumn9
            // 
            this.dataGridViewButtonColumn9.DataPropertyName = "State";
            this.dataGridViewButtonColumn9.HeaderText = "State";
            this.dataGridViewButtonColumn9.Name = "dataGridViewButtonColumn9";
            this.dataGridViewButtonColumn9.ReadOnly = true;
            this.dataGridViewButtonColumn9.Width = 36;
            // 
            // dataGridViewButtonColumn10
            // 
            this.dataGridViewButtonColumn10.DataPropertyName = "State";
            this.dataGridViewButtonColumn10.HeaderText = "State";
            this.dataGridViewButtonColumn10.Name = "dataGridViewButtonColumn10";
            this.dataGridViewButtonColumn10.ReadOnly = true;
            this.dataGridViewButtonColumn10.Width = 36;
            // 
            // dataGridViewButtonColumn11
            // 
            this.dataGridViewButtonColumn11.DataPropertyName = "State";
            this.dataGridViewButtonColumn11.HeaderText = "State";
            this.dataGridViewButtonColumn11.Name = "dataGridViewButtonColumn11";
            this.dataGridViewButtonColumn11.ReadOnly = true;
            this.dataGridViewButtonColumn11.Width = 36;
            // 
            // dataGridViewButtonColumn12
            // 
            this.dataGridViewButtonColumn12.DataPropertyName = "State";
            this.dataGridViewButtonColumn12.HeaderText = "State";
            this.dataGridViewButtonColumn12.Name = "dataGridViewButtonColumn12";
            this.dataGridViewButtonColumn12.ReadOnly = true;
            this.dataGridViewButtonColumn12.Width = 36;
            // 
            // dataGridViewButtonColumn13
            // 
            this.dataGridViewButtonColumn13.DataPropertyName = "State";
            this.dataGridViewButtonColumn13.HeaderText = "State";
            this.dataGridViewButtonColumn13.Name = "dataGridViewButtonColumn13";
            this.dataGridViewButtonColumn13.ReadOnly = true;
            this.dataGridViewButtonColumn13.Width = 36;
            // 
            // dataGridViewButtonColumn14
            // 
            this.dataGridViewButtonColumn14.DataPropertyName = "State";
            this.dataGridViewButtonColumn14.HeaderText = "State";
            this.dataGridViewButtonColumn14.Name = "dataGridViewButtonColumn14";
            this.dataGridViewButtonColumn14.ReadOnly = true;
            this.dataGridViewButtonColumn14.Width = 36;
            // 
            // dataGridViewButtonColumn15
            // 
            this.dataGridViewButtonColumn15.DataPropertyName = "State";
            this.dataGridViewButtonColumn15.HeaderText = "State";
            this.dataGridViewButtonColumn15.Name = "dataGridViewButtonColumn15";
            this.dataGridViewButtonColumn15.ReadOnly = true;
            this.dataGridViewButtonColumn15.Width = 36;
            // 
            // dataGridViewButtonColumn16
            // 
            this.dataGridViewButtonColumn16.DataPropertyName = "State";
            this.dataGridViewButtonColumn16.HeaderText = "State";
            this.dataGridViewButtonColumn16.Name = "dataGridViewButtonColumn16";
            this.dataGridViewButtonColumn16.ReadOnly = true;
            this.dataGridViewButtonColumn16.Width = 36;
            // 
            // State
            // 
            this.State.DataPropertyName = "StateString";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Transparent;
            this.State.DefaultCellStyle = dataGridViewCellStyle1;
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            this.State.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.State.Width = 55;
            // 
            // FileName
            // 
            this.FileName.DataPropertyName = "FileName";
            this.FileName.HeaderText = "File name";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Width = 75;
            // 
            // dataGridViewLinkColumn1
            // 
            this.dataGridViewLinkColumn1.DataPropertyName = "Server";
            this.dataGridViewLinkColumn1.HeaderText = "Server";
            this.dataGridViewLinkColumn1.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.dataGridViewLinkColumn1.Name = "dataGridViewLinkColumn1";
            this.dataGridViewLinkColumn1.TrackVisitedState = false;
            this.dataGridViewLinkColumn1.Width = 42;
            // 
            // bUpdate
            // 
            this.bUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bUpdate.BackColor = System.Drawing.Color.GreenYellow;
            this.bUpdate.DisabledBackColor = System.Drawing.Color.LightGray;
            this.bUpdate.DisabledForeColor = System.Drawing.Color.DimGray;
            this.bUpdate.EnabledBackColor = System.Drawing.Color.GreenYellow;
            this.bUpdate.EnabledForeColor = System.Drawing.SystemColors.ControlText;
            this.bUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bUpdate.Location = new System.Drawing.Point(9, 140);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(130, 23);
            this.bUpdate.TabIndex = 13;
            this.bUpdate.Text = "Update from this source";
            this.bUpdate.UseVisualStyleBackColor = false;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // bAddSource
            // 
            this.bAddSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bAddSource.BackColor = System.Drawing.Color.ForestGreen;
            this.bAddSource.DisabledBackColor = System.Drawing.Color.LightGray;
            this.bAddSource.DisabledForeColor = System.Drawing.Color.DimGray;
            this.bAddSource.EnabledBackColor = System.Drawing.Color.ForestGreen;
            this.bAddSource.EnabledForeColor = System.Drawing.SystemColors.ControlText;
            this.bAddSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAddSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bAddSource.Location = new System.Drawing.Point(242, 140);
            this.bAddSource.Name = "bAddSource";
            this.bAddSource.Size = new System.Drawing.Size(75, 23);
            this.bAddSource.TabIndex = 13;
            this.bAddSource.Text = "Add";
            this.bAddSource.UseVisualStyleBackColor = false;
            this.bAddSource.Click += new System.EventHandler(this.bAddSource_Click);
            // 
            // bRemoveSource
            // 
            this.bRemoveSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bRemoveSource.BackColor = System.Drawing.Color.LightCoral;
            this.bRemoveSource.DisabledBackColor = System.Drawing.Color.LightGray;
            this.bRemoveSource.DisabledForeColor = System.Drawing.Color.DimGray;
            this.bRemoveSource.EnabledBackColor = System.Drawing.Color.LightCoral;
            this.bRemoveSource.EnabledForeColor = System.Drawing.SystemColors.ControlText;
            this.bRemoveSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRemoveSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bRemoveSource.Location = new System.Drawing.Point(323, 140);
            this.bRemoveSource.Name = "bRemoveSource";
            this.bRemoveSource.Size = new System.Drawing.Size(75, 23);
            this.bRemoveSource.TabIndex = 12;
            this.bRemoveSource.Text = "Remove";
            this.bRemoveSource.UseVisualStyleBackColor = false;
            this.bRemoveSource.Click += new System.EventHandler(this.bRemoveSource_Click);
            // 
            // bsSources
            // 
            this.bsSources.DataSource = typeof(SMT.Models.ModSource);
            this.bsSources.CurrentItemChanged += new System.EventHandler(this.bsSources_CurrentItemChanged);
            // 
            // bAddMod
            // 
            this.bAddMod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bAddMod.BackColor = System.Drawing.Color.ForestGreen;
            this.bAddMod.DisabledBackColor = System.Drawing.Color.LightGray;
            this.bAddMod.DisabledForeColor = System.Drawing.Color.DimGray;
            this.bAddMod.EnabledBackColor = System.Drawing.Color.ForestGreen;
            this.bAddMod.EnabledForeColor = System.Drawing.SystemColors.ControlText;
            this.bAddMod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAddMod.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bAddMod.Location = new System.Drawing.Point(242, 145);
            this.bAddMod.Name = "bAddMod";
            this.bAddMod.Size = new System.Drawing.Size(75, 23);
            this.bAddMod.TabIndex = 6;
            this.bAddMod.Text = "Add";
            this.bAddMod.UseVisualStyleBackColor = false;
            this.bAddMod.Click += new System.EventHandler(this.bAddMod_Click);
            // 
            // bRemoveMod
            // 
            this.bRemoveMod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bRemoveMod.BackColor = System.Drawing.Color.LightCoral;
            this.bRemoveMod.DisabledBackColor = System.Drawing.Color.LightGray;
            this.bRemoveMod.DisabledForeColor = System.Drawing.Color.DimGray;
            this.bRemoveMod.EnabledBackColor = System.Drawing.Color.LightCoral;
            this.bRemoveMod.EnabledForeColor = System.Drawing.SystemColors.ControlText;
            this.bRemoveMod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRemoveMod.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bRemoveMod.Location = new System.Drawing.Point(323, 145);
            this.bRemoveMod.Name = "bRemoveMod";
            this.bRemoveMod.Size = new System.Drawing.Size(75, 23);
            this.bRemoveMod.TabIndex = 5;
            this.bRemoveMod.Text = "Remove";
            this.bRemoveMod.UseVisualStyleBackColor = false;
            this.bRemoveMod.Click += new System.EventHandler(this.bRemoveMod_Click);
            // 
            // bBrowse
            // 
            this.bBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bBrowse.BackColor = System.Drawing.Color.SkyBlue;
            this.bBrowse.DisabledBackColor = System.Drawing.Color.LightGray;
            this.bBrowse.DisabledForeColor = System.Drawing.Color.DimGray;
            this.bBrowse.EnabledBackColor = System.Drawing.Color.SkyBlue;
            this.bBrowse.EnabledForeColor = System.Drawing.Color.Black;
            this.bBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 4.25F);
            this.bBrowse.ForeColor = System.Drawing.Color.Black;
            this.bBrowse.Location = new System.Drawing.Point(325, 100);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(72, 20);
            this.bBrowse.TabIndex = 4;
            this.bBrowse.Text = "..";
            this.bBrowse.UseVisualStyleBackColor = false;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // bsMods
            // 
            this.bsMods.DataSource = typeof(SMT.Models.Mod);
            this.bsMods.CurrentChanged += new System.EventHandler(this.bsMods_CurrentChanged);
            this.bsMods.CurrentItemChanged += new System.EventHandler(this.bsMods_CurrentItemChanged);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 58;
            // 
            // versionDataGridViewTextBoxColumn
            // 
            this.versionDataGridViewTextBoxColumn.DataPropertyName = "Version";
            this.versionDataGridViewTextBoxColumn.HeaderText = "Version";
            this.versionDataGridViewTextBoxColumn.Name = "versionDataGridViewTextBoxColumn";
            this.versionDataGridViewTextBoxColumn.ReadOnly = true;
            this.versionDataGridViewTextBoxColumn.Width = 65;
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
            // serverDataGridViewLinkColumn
            // 
            this.serverDataGridViewLinkColumn.DataPropertyName = "Server";
            this.serverDataGridViewLinkColumn.HeaderText = "Server";
            this.serverDataGridViewLinkColumn.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.serverDataGridViewLinkColumn.Name = "serverDataGridViewLinkColumn";
            this.serverDataGridViewLinkColumn.ReadOnly = true;
            this.serverDataGridViewLinkColumn.TrackVisitedState = false;
            this.serverDataGridViewLinkColumn.Width = 42;
            // 
            // languageDataGridViewTextColumn
            // 
            this.languageDataGridViewTextColumn.DataPropertyName = "Language";
            this.languageDataGridViewTextColumn.HeaderText = "Language";
            this.languageDataGridViewTextColumn.Name = "languageDataGridViewTextColumn";
            this.languageDataGridViewTextColumn.ReadOnly = true;
            this.languageDataGridViewTextColumn.Width = 78;
            // 
            // versionModDataGridViewTextColumn
            // 
            this.versionModDataGridViewTextColumn.DataPropertyName = "Version";
            this.versionModDataGridViewTextColumn.HeaderText = "Version";
            this.versionModDataGridViewTextColumn.Name = "versionModDataGridViewTextColumn";
            this.versionModDataGridViewTextColumn.ReadOnly = true;
            this.versionModDataGridViewTextColumn.Width = 65;
            // 
            // stateModDataGridViewTextColumn
            // 
            this.stateModDataGridViewTextColumn.DataPropertyName = "StateString";
            this.stateModDataGridViewTextColumn.HeaderText = "State";
            this.stateModDataGridViewTextColumn.Name = "stateModDataGridViewTextColumn";
            this.stateModDataGridViewTextColumn.ReadOnly = true;
            this.stateModDataGridViewTextColumn.Width = 55;
            // 
            // pathDataGridViewLinkColumn
            // 
            this.pathDataGridViewLinkColumn.DataPropertyName = "Path";
            this.pathDataGridViewLinkColumn.HeaderText = "Path";
            this.pathDataGridViewLinkColumn.Name = "pathDataGridViewLinkColumn";
            this.pathDataGridViewLinkColumn.ReadOnly = true;
            this.pathDataGridViewLinkColumn.Width = 33;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 569);
            this.Controls.Add(this.ssStatus);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvMods);
            this.Controls.Add(this.msMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMenu;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(2000, 1000);
            this.MinimumSize = new System.Drawing.Size(630, 575);
            this.Name = "MainForm";
            this.Text = "Skyrim Mods Tracker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMods)).EndInit();
            this.gbMod.ResumeLayout(false);
            this.gbMod.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSources)).EndInit();
            this.panel1.ResumeLayout(false);
            this.gbSource.ResumeLayout(false);
            this.gbSource.PerformLayout();
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsSources)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMods)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gbMod;
        private System.Windows.Forms.DataGridView dgvMods;
        private System.Windows.Forms.TextBox tbVersion;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lRoot;
        private System.Windows.Forms.Label lVersion;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.OpenFileDialog ofdRoot;
        private System.Windows.Forms.TextBox tbRoot;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvSources;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbSource;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.CheckBox cbManual;
        private System.Windows.Forms.TextBox tbSourceVersion;
        private System.Windows.Forms.Label lSrcVersion;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Label lLanguage;
        private System.Windows.Forms.Label lServer;
        private System.Windows.Forms.BindingSource bsSources;
        private Utils.StateButton bBrowse;
        private Utils.StateButton bAddMod;
        private Utils.StateButton bRemoveMod;
        private System.Windows.Forms.Label lRootError;
        private System.Windows.Forms.Label lVersionError;
        private System.Windows.Forms.Label lNameError;
        private System.Windows.Forms.Label lSrcVersionError;
        private System.Windows.Forms.Label lURLError;
        private Utils.StateButton bAddSource;
        private Utils.StateButton bRemoveSource;
        private System.Windows.Forms.BindingSource bsMods;
        private System.Windows.Forms.DataGridViewTextBoxColumn rootDataGridViewTextBoxColumn;
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.ToolStripProgressBar spbStatusProgress;
        private System.Windows.Forms.ToolStripStatusLabel slStatusTitle;
        private System.Windows.Forms.ToolStripMenuItem checkModsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serversToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.Label lDummyFocus;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private Utils.StateButton bUpdate;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn2;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn3;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn4;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn5;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn6;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn7;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn8;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn9;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn10;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn11;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn12;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn13;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn14;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn15;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn dataGridViewLinkColumn1;
        private System.Windows.Forms.DataGridViewLinkColumn serverDataGridViewLinkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn languageDataGridViewTextColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionModDataGridViewTextColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateModDataGridViewTextColumn;
        private System.Windows.Forms.DataGridViewLinkColumn pathDataGridViewLinkColumn;
    }
}

