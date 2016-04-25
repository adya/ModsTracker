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
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.lPattern = new System.Windows.Forms.Label();
            this.lURL = new System.Windows.Forms.Label();
            this.tbPattern = new System.Windows.Forms.TextBox();
            this.lName = new System.Windows.Forms.Label();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.bAdd = new System.Windows.Forms.Button();
            this.bDelete = new System.Windows.Forms.Button();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerAvailable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uRLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionPatternDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsServers = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServers)).BeginInit();
            this.gbDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsServers)).BeginInit();
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
            this.dgvServers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.ID,
            this.uRLDataGridViewTextBoxColumn,
            this.ServerAvailable,
            this.versionPatternDataGridViewTextBoxColumn});
            this.dgvServers.DataSource = this.bsServers;
            this.dgvServers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvServers.Location = new System.Drawing.Point(12, 12);
            this.dgvServers.Name = "dgvServers";
            this.dgvServers.ReadOnly = true;
            this.dgvServers.RowHeadersVisible = false;
            this.dgvServers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServers.Size = new System.Drawing.Size(404, 237);
            this.dgvServers.TabIndex = 5;
            this.dgvServers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvServers_MouseDown);
            // 
            // gbDetails
            // 
            this.gbDetails.Controls.Add(this.lPattern);
            this.gbDetails.Controls.Add(this.lURL);
            this.gbDetails.Controls.Add(this.tbPattern);
            this.gbDetails.Controls.Add(this.lName);
            this.gbDetails.Controls.Add(this.tbURL);
            this.gbDetails.Controls.Add(this.tbName);
            this.gbDetails.Controls.Add(this.bAdd);
            this.gbDetails.Controls.Add(this.bDelete);
            this.gbDetails.Location = new System.Drawing.Point(422, 12);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(250, 237);
            this.gbDetails.TabIndex = 1;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "Server";
            // 
            // lPattern
            // 
            this.lPattern.AutoSize = true;
            this.lPattern.Location = new System.Drawing.Point(10, 74);
            this.lPattern.Name = "lPattern";
            this.lPattern.Size = new System.Drawing.Size(44, 13);
            this.lPattern.TabIndex = 3;
            this.lPattern.Text = "Pattern:";
            // 
            // lURL
            // 
            this.lURL.AutoSize = true;
            this.lURL.Location = new System.Drawing.Point(10, 48);
            this.lURL.Name = "lURL";
            this.lURL.Size = new System.Drawing.Size(32, 13);
            this.lURL.TabIndex = 3;
            this.lURL.Text = "URL:";
            // 
            // tbPattern
            // 
            this.tbPattern.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServers, "VersionPattern", true));
            this.tbPattern.Location = new System.Drawing.Point(54, 71);
            this.tbPattern.Name = "tbPattern";
            this.tbPattern.Size = new System.Drawing.Size(190, 20);
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
            this.tbURL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServers, "URL", true));
            this.tbURL.Location = new System.Drawing.Point(54, 45);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(190, 20);
            this.tbURL.TabIndex = 1;
            this.tbURL.Validating += new System.ComponentModel.CancelEventHandler(this.tbURL_Validating);
            // 
            // tbName
            // 
            this.tbName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServers, "Name", true));
            this.tbName.Location = new System.Drawing.Point(54, 19);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(190, 20);
            this.tbName.TabIndex = 0;
            this.tbName.Validating += new System.ComponentModel.CancelEventHandler(this.tbName_Validating);
            // 
            // bAdd
            // 
            this.bAdd.BackColor = System.Drawing.Color.YellowGreen;
            this.bAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAdd.Location = new System.Drawing.Point(88, 208);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 23);
            this.bAdd.TabIndex = 3;
            this.bAdd.Text = "Add";
            this.bAdd.UseVisualStyleBackColor = false;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bDelete
            // 
            this.bDelete.BackColor = System.Drawing.Color.IndianRed;
            this.bDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDelete.Location = new System.Drawing.Point(169, 208);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(75, 23);
            this.bDelete.TabIndex = 4;
            this.bDelete.Text = "Delete";
            this.bDelete.UseVisualStyleBackColor = false;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 41;
            // 
            // ServerAvailable
            // 
            this.ServerAvailable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ServerAvailable.HeaderText = "Status";
            this.ServerAvailable.Name = "ServerAvailable";
            this.ServerAvailable.ReadOnly = true;
            this.ServerAvailable.Width = 41;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 58;
            // 
            // uRLDataGridViewTextBoxColumn
            // 
            this.uRLDataGridViewTextBoxColumn.DataPropertyName = "URL";
            this.uRLDataGridViewTextBoxColumn.HeaderText = "URL";
            this.uRLDataGridViewTextBoxColumn.Name = "uRLDataGridViewTextBoxColumn";
            this.uRLDataGridViewTextBoxColumn.ReadOnly = true;
            this.uRLDataGridViewTextBoxColumn.Width = 52;
            // 
            // versionPatternDataGridViewTextBoxColumn
            // 
            this.versionPatternDataGridViewTextBoxColumn.DataPropertyName = "VersionPattern";
            this.versionPatternDataGridViewTextBoxColumn.HeaderText = "VersionPattern";
            this.versionPatternDataGridViewTextBoxColumn.Name = "versionPatternDataGridViewTextBoxColumn";
            this.versionPatternDataGridViewTextBoxColumn.ReadOnly = true;
            this.versionPatternDataGridViewTextBoxColumn.Width = 99;
            // 
            // bsServers
            // 
            this.bsServers.DataSource = typeof(SMT.Models.Server);
            // 
            // ServersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 261);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.dgvServers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(700, 500);
            this.MinimumSize = new System.Drawing.Size(700, 300);
            this.Name = "ServersForm";
            this.Text = "ServersForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServersForm_FormClosing);
            this.Load += new System.EventHandler(this.ServersForm_Load);
            this.ResizeBegin += new System.EventHandler(this.ServersForm_ResizeBegin);
            this.SizeChanged += new System.EventHandler(this.ServersForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServers)).EndInit();
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsServers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvServers;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Label lURL;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lPattern;
        private System.Windows.Forms.TextBox tbPattern;
        private System.Windows.Forms.BindingSource bsServers;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn uRLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ServerAvailable;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionPatternDataGridViewTextBoxColumn;
    }
}