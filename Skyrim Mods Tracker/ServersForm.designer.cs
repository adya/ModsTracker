namespace SMT
{
    partial class ServersForm
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
            this.dgvServers = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewcontrolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverLanguageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uRLDataGridViewcontrolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionPatternDataGridViewcontrolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsServers = new System.Windows.Forms.BindingSource(this.components);
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.cbServerLanguage = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bAdd = new SMT.Utils.StateButton();
            this.bRemove = new SMT.Utils.StateButton();
            this.lPatternError = new System.Windows.Forms.Label();
            this.lURLError = new System.Windows.Forms.Label();
            this.lNameError = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lPattern = new System.Windows.Forms.Label();
            this.lURL = new System.Windows.Forms.Label();
            this.tbCookies = new System.Windows.Forms.TextBox();
            this.tbPattern = new System.Windows.Forms.TextBox();
            this.lName = new System.Windows.Forms.Label();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsServers)).BeginInit();
            this.gbDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvServers
            // 
            this.dgvServers.AllowUserToAddRows = false;
            this.dgvServers.AllowUserToDeleteRows = false;
            this.dgvServers.AllowUserToResizeRows = false;
            this.dgvServers.AutoGenerateColumns = false;
            this.dgvServers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvServers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvServers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvServers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvServers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvServers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewcontrolColumn,
            this.ID,
            this.serverLanguageDataGridViewTextBoxColumn,
            this.uRLDataGridViewcontrolColumn,
            this.versionPatternDataGridViewcontrolColumn});
            this.dgvServers.DataSource = this.bsServers;
            this.dgvServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvServers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvServers.EnableHeadersVisualStyles = false;
            this.dgvServers.Location = new System.Drawing.Point(0, 0);
            this.dgvServers.Name = "dgvServers";
            this.dgvServers.ReadOnly = true;
            this.dgvServers.RowHeadersVisible = false;
            this.dgvServers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServers.Size = new System.Drawing.Size(423, 261);
            this.dgvServers.TabIndex = 5;
            this.dgvServers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvServers_MouseDown);
            // 
            // nameDataGridViewcontrolColumn
            // 
            this.nameDataGridViewcontrolColumn.DataPropertyName = "Name";
            this.nameDataGridViewcontrolColumn.HeaderText = "Name";
            this.nameDataGridViewcontrolColumn.Name = "nameDataGridViewcontrolColumn";
            this.nameDataGridViewcontrolColumn.ReadOnly = true;
            this.nameDataGridViewcontrolColumn.Width = 59;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 42;
            // 
            // serverLanguageDataGridViewTextBoxColumn
            // 
            this.serverLanguageDataGridViewTextBoxColumn.DataPropertyName = "Language";
            this.serverLanguageDataGridViewTextBoxColumn.HeaderText = "Language";
            this.serverLanguageDataGridViewTextBoxColumn.Name = "serverLanguageDataGridViewTextBoxColumn";
            this.serverLanguageDataGridViewTextBoxColumn.ReadOnly = true;
            this.serverLanguageDataGridViewTextBoxColumn.Width = 79;
            // 
            // uRLDataGridViewcontrolColumn
            // 
            this.uRLDataGridViewcontrolColumn.DataPropertyName = "URL";
            this.uRLDataGridViewcontrolColumn.HeaderText = "URL";
            this.uRLDataGridViewcontrolColumn.Name = "uRLDataGridViewcontrolColumn";
            this.uRLDataGridViewcontrolColumn.ReadOnly = true;
            this.uRLDataGridViewcontrolColumn.Width = 53;
            // 
            // versionPatternDataGridViewcontrolColumn
            // 
            this.versionPatternDataGridViewcontrolColumn.DataPropertyName = "VersionPattern";
            this.versionPatternDataGridViewcontrolColumn.HeaderText = "Version pattern";
            this.versionPatternDataGridViewcontrolColumn.Name = "versionPatternDataGridViewcontrolColumn";
            this.versionPatternDataGridViewcontrolColumn.ReadOnly = true;
            this.versionPatternDataGridViewcontrolColumn.Width = 102;
            // 
            // bsServers
            // 
            this.bsServers.DataSource = typeof(SMT.Models.Server);
            this.bsServers.CurrentItemChanged += new System.EventHandler(this.bsServers_CurrentItemChanged);
            // 
            // gbDetails
            // 
            this.gbDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDetails.Controls.Add(this.cbServerLanguage);
            this.gbDetails.Controls.Add(this.label2);
            this.gbDetails.Controls.Add(this.bAdd);
            this.gbDetails.Controls.Add(this.bRemove);
            this.gbDetails.Controls.Add(this.lPatternError);
            this.gbDetails.Controls.Add(this.lURLError);
            this.gbDetails.Controls.Add(this.lNameError);
            this.gbDetails.Controls.Add(this.label1);
            this.gbDetails.Controls.Add(this.lPattern);
            this.gbDetails.Controls.Add(this.lURL);
            this.gbDetails.Controls.Add(this.tbCookies);
            this.gbDetails.Controls.Add(this.tbPattern);
            this.gbDetails.Controls.Add(this.lName);
            this.gbDetails.Controls.Add(this.tbURL);
            this.gbDetails.Controls.Add(this.tbName);
            this.gbDetails.Location = new System.Drawing.Point(4, 3);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(250, 255);
            this.gbDetails.TabIndex = 1;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "Server";
            // 
            // cbServerLanguage
            // 
            this.cbServerLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbServerLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbServerLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServerLanguage.FormattingEnabled = true;
            this.cbServerLanguage.Location = new System.Drawing.Point(75, 100);
            this.cbServerLanguage.Name = "cbServerLanguage";
            this.cbServerLanguage.Size = new System.Drawing.Size(169, 21);
            this.cbServerLanguage.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Language:";
            // 
            // bAdd
            // 
            this.bAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bAdd.BackColor = System.Drawing.Color.ForestGreen;
            this.bAdd.DisabledBackColor = System.Drawing.Color.LightGray;
            this.bAdd.DisabledForeColor = System.Drawing.Color.DimGray;
            this.bAdd.EnabledBackColor = System.Drawing.Color.ForestGreen;
            this.bAdd.EnabledForeColor = System.Drawing.SystemColors.ControlText;
            this.bAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bAdd.Location = new System.Drawing.Point(88, 223);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 23);
            this.bAdd.TabIndex = 7;
            this.bAdd.Text = "Add";
            this.bAdd.UseVisualStyleBackColor = false;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bRemove
            // 
            this.bRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bRemove.BackColor = System.Drawing.Color.LightCoral;
            this.bRemove.DisabledBackColor = System.Drawing.Color.LightGray;
            this.bRemove.DisabledForeColor = System.Drawing.Color.DimGray;
            this.bRemove.EnabledBackColor = System.Drawing.Color.LightCoral;
            this.bRemove.EnabledForeColor = System.Drawing.Color.Black;
            this.bRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRemove.ForeColor = System.Drawing.Color.Black;
            this.bRemove.Location = new System.Drawing.Point(169, 223);
            this.bRemove.Name = "bRemove";
            this.bRemove.Size = new System.Drawing.Size(75, 23);
            this.bRemove.TabIndex = 6;
            this.bRemove.Text = "Remove";
            this.bRemove.UseVisualStyleBackColor = false;
            this.bRemove.Click += new System.EventHandler(this.bRemove_Click);
            // 
            // lPatternError
            // 
            this.lPatternError.AutoSize = true;
            this.lPatternError.Location = new System.Drawing.Point(81, 163);
            this.lPatternError.Name = "lPatternError";
            this.lPatternError.Size = new System.Drawing.Size(29, 13);
            this.lPatternError.TabIndex = 5;
            this.lPatternError.Text = "Error";
            this.lPatternError.Visible = false;
            // 
            // lURLError
            // 
            this.lURLError.AutoSize = true;
            this.lURLError.Location = new System.Drawing.Point(81, 83);
            this.lURLError.Name = "lURLError";
            this.lURLError.Size = new System.Drawing.Size(29, 13);
            this.lURLError.TabIndex = 5;
            this.lURLError.Text = "Error";
            this.lURLError.Visible = false;
            // 
            // lNameError
            // 
            this.lNameError.AutoSize = true;
            this.lNameError.Location = new System.Drawing.Point(81, 43);
            this.lNameError.Name = "lNameError";
            this.lNameError.Size = new System.Drawing.Size(29, 13);
            this.lNameError.TabIndex = 5;
            this.lNameError.Text = "Error";
            this.lNameError.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cookies:";
            // 
            // lPattern
            // 
            this.lPattern.AutoSize = true;
            this.lPattern.Location = new System.Drawing.Point(10, 143);
            this.lPattern.Name = "lPattern";
            this.lPattern.Size = new System.Drawing.Size(44, 13);
            this.lPattern.TabIndex = 3;
            this.lPattern.Text = "Pattern:";
            // 
            // lURL
            // 
            this.lURL.AutoSize = true;
            this.lURL.Location = new System.Drawing.Point(10, 63);
            this.lURL.Name = "lURL";
            this.lURL.Size = new System.Drawing.Size(32, 13);
            this.lURL.TabIndex = 3;
            this.lURL.Text = "URL:";
            // 
            // tbCookies
            // 
            this.tbCookies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCookies.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServers, "Cookies", true));
            this.tbCookies.Location = new System.Drawing.Point(75, 180);
            this.tbCookies.Name = "tbCookies";
            this.tbCookies.Size = new System.Drawing.Size(169, 20);
            this.tbCookies.TabIndex = 2;
            // 
            // tbPattern
            // 
            this.tbPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPattern.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServers, "VersionPattern", true));
            this.tbPattern.Location = new System.Drawing.Point(75, 140);
            this.tbPattern.Name = "tbPattern";
            this.tbPattern.Size = new System.Drawing.Size(169, 20);
            this.tbPattern.TabIndex = 2;
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(10, 22);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(38, 13);
            this.lName.TabIndex = 3;
            this.lName.Text = "Name:";
            // 
            // tbURL
            // 
            this.tbURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbURL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServers, "URL", true));
            this.tbURL.Location = new System.Drawing.Point(75, 60);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(169, 20);
            this.tbURL.TabIndex = 1;
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServers, "Name", true));
            this.tbName.Location = new System.Drawing.Point(75, 20);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(169, 20);
            this.tbName.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvServers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbDetails);
            this.splitContainer1.Size = new System.Drawing.Size(684, 261);
            this.splitContainer1.SplitterDistance = 423;
            this.splitContainer1.TabIndex = 6;
            // 
            // ServersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 261);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(1200, 1500);
            this.MinimumSize = new System.Drawing.Size(700, 300);
            this.Name = "ServersForm";
            this.Text = "ServersForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServersForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServersForm_FormClosed);
            this.Load += new System.EventHandler(this.ServersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsServers)).EndInit();
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvServers;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.Label lURL;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lPattern;
        private System.Windows.Forms.TextBox tbPattern;
        private System.Windows.Forms.BindingSource bsServers;
        private System.Windows.Forms.Label lPatternError;
        private System.Windows.Forms.Label lURLError;
        private System.Windows.Forms.Label lNameError;
        private Utils.StateButton bRemove;
        private Utils.StateButton bAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCookies;
        private System.Windows.Forms.ComboBox cbServerLanguage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewcontrolColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverLanguageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uRLDataGridViewcontrolColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionPatternDataGridViewcontrolColumn;
    }
}