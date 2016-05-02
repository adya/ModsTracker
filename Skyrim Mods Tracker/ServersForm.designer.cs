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
            this.bsServers = new System.Windows.Forms.BindingSource(this.components);
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.bAdd = new SMT.Utils.StateButton();
            this.bRemove = new SMT.Utils.StateButton();
            this.lPatternError = new System.Windows.Forms.Label();
            this.lURLError = new System.Windows.Forms.Label();
            this.lNameError = new System.Windows.Forms.Label();
            this.lPattern = new System.Windows.Forms.Label();
            this.lURL = new System.Windows.Forms.Label();
            this.tbPattern = new System.Windows.Forms.TextBox();
            this.lName = new System.Windows.Forms.Label();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.nameDataGridViewcontrolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uRLDataGridViewcontrolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerAvailable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.versionPatternDataGridViewcontrolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsServers)).BeginInit();
            this.gbDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvServers
            // 
            this.dgvServers.AllowUserToAddRows = false;
            this.dgvServers.AllowUserToDeleteRows = false;
            this.dgvServers.AllowUserToResizeRows = false;
            this.dgvServers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvServers.AutoGenerateColumns = false;
            this.dgvServers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvServers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvServers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvServers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewcontrolColumn,
            this.ID,
            this.uRLDataGridViewcontrolColumn,
            this.ServerAvailable,
            this.versionPatternDataGridViewcontrolColumn});
            this.dgvServers.DataSource = this.bsServers;
            this.dgvServers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvServers.Location = new System.Drawing.Point(12, 12);
            this.dgvServers.Name = "dgvServers";
            this.dgvServers.ReadOnly = true;
            this.dgvServers.RowHeadersVisible = false;
            this.dgvServers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServers.Size = new System.Drawing.Size(416, 243);
            this.dgvServers.TabIndex = 5;
            this.dgvServers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvServers_MouseDown);
            // 
            // bsServers
            // 
            this.bsServers.DataSource = typeof(SMT.Models.Server);
            this.bsServers.CurrentItemChanged += new System.EventHandler(this.bsServers_CurrentItemChanged);
            // 
            // gbDetails
            // 
            this.gbDetails.Controls.Add(this.bAdd);
            this.gbDetails.Controls.Add(this.bRemove);
            this.gbDetails.Controls.Add(this.lPatternError);
            this.gbDetails.Controls.Add(this.lURLError);
            this.gbDetails.Controls.Add(this.lNameError);
            this.gbDetails.Controls.Add(this.lPattern);
            this.gbDetails.Controls.Add(this.lURL);
            this.gbDetails.Controls.Add(this.tbPattern);
            this.gbDetails.Controls.Add(this.lName);
            this.gbDetails.Controls.Add(this.tbURL);
            this.gbDetails.Controls.Add(this.tbName);
            this.gbDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbDetails.Location = new System.Drawing.Point(434, 0);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(250, 261);
            this.gbDetails.TabIndex = 1;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "Server";
            // 
            // bAdd
            // 
            this.bAdd.BackColor = System.Drawing.Color.ForestGreen;
            this.bAdd.DisabledBackColor = System.Drawing.Color.LightGray;
            this.bAdd.DisabledForeColor = System.Drawing.Color.DimGray;
            this.bAdd.EnabledBackColor = System.Drawing.Color.ForestGreen;
            this.bAdd.EnabledForeColor = System.Drawing.SystemColors.ControlText;
            this.bAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bAdd.Location = new System.Drawing.Point(89, 232);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 23);
            this.bAdd.TabIndex = 7;
            this.bAdd.Text = "Add";
            this.bAdd.UseVisualStyleBackColor = false;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bRemove
            // 
            this.bRemove.BackColor = System.Drawing.Color.LightCoral;
            this.bRemove.DisabledBackColor = System.Drawing.Color.LightGray;
            this.bRemove.DisabledForeColor = System.Drawing.Color.DimGray;
            this.bRemove.EnabledBackColor = System.Drawing.Color.LightCoral;
            this.bRemove.EnabledForeColor = System.Drawing.Color.Black;
            this.bRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRemove.ForeColor = System.Drawing.Color.Black;
            this.bRemove.Location = new System.Drawing.Point(170, 232);
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
            this.lPatternError.Location = new System.Drawing.Point(60, 123);
            this.lPatternError.Name = "lPatternError";
            this.lPatternError.Size = new System.Drawing.Size(29, 13);
            this.lPatternError.TabIndex = 5;
            this.lPatternError.Text = "Error";
            this.lPatternError.Visible = false;
            // 
            // lURLError
            // 
            this.lURLError.AutoSize = true;
            this.lURLError.Location = new System.Drawing.Point(60, 83);
            this.lURLError.Name = "lURLError";
            this.lURLError.Size = new System.Drawing.Size(29, 13);
            this.lURLError.TabIndex = 5;
            this.lURLError.Text = "Error";
            this.lURLError.Visible = false;
            // 
            // lNameError
            // 
            this.lNameError.AutoSize = true;
            this.lNameError.Location = new System.Drawing.Point(60, 43);
            this.lNameError.Name = "lNameError";
            this.lNameError.Size = new System.Drawing.Size(29, 13);
            this.lNameError.TabIndex = 5;
            this.lNameError.Text = "Error";
            this.lNameError.Visible = false;
            // 
            // lPattern
            // 
            this.lPattern.AutoSize = true;
            this.lPattern.Location = new System.Drawing.Point(10, 103);
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
            // tbPattern
            // 
            this.tbPattern.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServers, "VersionPattern", true));
            this.tbPattern.Location = new System.Drawing.Point(54, 100);
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
            this.tbURL.Location = new System.Drawing.Point(54, 60);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(190, 20);
            this.tbURL.TabIndex = 1;
            // 
            // tbName
            // 
            this.tbName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServers, "Name", true));
            this.tbName.Location = new System.Drawing.Point(54, 20);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(190, 20);
            this.tbName.TabIndex = 0;
            // 
            // nameDataGridViewcontrolColumn
            // 
            this.nameDataGridViewcontrolColumn.DataPropertyName = "Name";
            this.nameDataGridViewcontrolColumn.HeaderText = "Name";
            this.nameDataGridViewcontrolColumn.Name = "nameDataGridViewcontrolColumn";
            this.nameDataGridViewcontrolColumn.ReadOnly = true;
            this.nameDataGridViewcontrolColumn.Width = 58;
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
            // uRLDataGridViewcontrolColumn
            // 
            this.uRLDataGridViewcontrolColumn.DataPropertyName = "URL";
            this.uRLDataGridViewcontrolColumn.HeaderText = "URL";
            this.uRLDataGridViewcontrolColumn.Name = "uRLDataGridViewcontrolColumn";
            this.uRLDataGridViewcontrolColumn.ReadOnly = true;
            this.uRLDataGridViewcontrolColumn.Width = 52;
            // 
            // ServerAvailable
            // 
            this.ServerAvailable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ServerAvailable.HeaderText = "Status";
            this.ServerAvailable.Name = "ServerAvailable";
            this.ServerAvailable.ReadOnly = true;
            this.ServerAvailable.Width = 41;
            // 
            // versionPatternDataGridViewcontrolColumn
            // 
            this.versionPatternDataGridViewcontrolColumn.DataPropertyName = "VersionPattern";
            this.versionPatternDataGridViewcontrolColumn.HeaderText = "Version pattern";
            this.versionPatternDataGridViewcontrolColumn.Name = "versionPatternDataGridViewcontrolColumn";
            this.versionPatternDataGridViewcontrolColumn.ReadOnly = true;
            this.versionPatternDataGridViewcontrolColumn.Width = 101;
            // 
            // ServersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 261);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.dgvServers);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewcontrolColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn uRLDataGridViewcontrolColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ServerAvailable;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionPatternDataGridViewcontrolColumn;
    }
}