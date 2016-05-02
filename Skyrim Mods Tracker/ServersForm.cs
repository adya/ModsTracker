using SMT.Managers;
using SMT.Models;
using SMT.Utils;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SMT
{
    public partial class ServersForm : Form
    {
        private BindingList<Server> servers;

        public ServersForm()
        {
            InitializeComponent();
            servers = new BindingList<Server>(ServersManager.Servers.ToList());
            bsServers.DataSource = servers;
        }

        private void ServersForm_Load(object sender, EventArgs e)
        {
            dgvServers.AutoResizeColumns();
            tbName.BindLabel(lNameError);
            tbURL.BindLabel(lURLError);
            tbPattern.BindLabel(lPatternError);

            tbURL.TextChanged += OnTextChanged;
            tbName.TextChanged += OnTextChanged;
            tbPattern.TextChanged += OnTextChanged;

            SetServerEditable(false);
        }

        private void SetServerEditable(bool isEditable)
        {
            bsServers.RaiseListChangedEvents = isEditable;
            if (!isEditable)
            {
                dgvServers.ClearSelection();
                bsServers.SuspendBinding();
                tbName.Clear();
                tbPattern.Clear();
                tbURL.Clear();
                tbName.ClearMessage();
                tbURL.ClearMessage();
                tbPattern.ClearMessage();
            }
            else
                bsServers.ResumeBinding();

            tbName.Enabled = isEditable;
            tbURL.Enabled = isEditable;
            tbPattern.Enabled = isEditable;
            bRemove.Enabled = isEditable;
        }

        private void ValidateName()
        {
            var cur = bsServers.Current as Server;
            if (cur == null) return;

            if (!cur.HasValidName) tbName.SetError("Name must not be empty.");
            else if (!cur.HasUniqueName()) tbName.SetWarning("Name must be unique.");
            else tbName.SetValidMessage(); 
        }
        private void ValidateURL()
        {
            if (bsServers.IsBindingSuspended) return;
            var cur = bsServers.Current as Server;
            if (cur == null) return;

            if (cur.HasValidURL)
            {
                if (ServersManager.Servers.FirstOrDefault(s => !s.Equals(cur) && s.URL == cur.URL) != null)
                    tbURL.SetWarning("Server with that URL already exists.");
                else
                    tbURL.SetValidMessage();
            }
            else tbURL.SetError("Malformed URL.");
        }
        private void ValidatePattern()
        {
            if (bsServers.IsBindingSuspended) return;
            var cur = bsServers.Current as Server;
            if (cur == null) return;

            if (cur.HasValidPattern) tbPattern.SetValidMessage();
            else tbPattern.SetError("Invalid pattern");
        }
        private void ValidateFields()
        {
            if (bsServers.IsBindingSuspended) return;
            ValidateName();
            ValidateURL();
            ValidatePattern();
        }

        private void EditRow(DataGridView dgv, int index)
        {
            dgv.ClearSelection();
            dgv.Rows[index].Selected = true;
            tbName.Focus();
            tbName.DeselectAll();
        }

        private void dgvServers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = dgvServers.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                    SetServerEditable(false);
                else if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
                    SetServerEditable(true);
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            SetServerEditable(true);
            bsServers.AddNew();
            EditRow(dgvServers, servers.Count - 1);
        }

        private void bRemove_Click(object sender, EventArgs e)
        {
            bsServers.RemoveCurrent();
            SetServerEditable(servers.Count > 0);
            dgvServers.ClearSelection();
            //if (servers.Count > 0) EditRow(dgvServers, servers.Count - 1);
        }

        private void bsServers_CurrentItemChanged(object sender, EventArgs e)
        {
            ValidateFields();  
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (bsServers.IsBindingSuspended) return;
            bsServers.EndEdit();
            (sender as TextBox).TextChanged -= OnTextChanged;
            bsServers.ResetCurrentItem();
            (sender as TextBox).TextChanged += OnTextChanged;
            ValidateFields();
        }

        private void ServersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var invalidServers = servers.Where(s => !s.IsValid).Select(s => s.Name).ToList();

            if (invalidServers.Count == 0 ||
                (DialogResult.OK == MessageBox.Show("Some of the servers has invalid configuration.\n" +
                                                    "Closing this window will save these configurations and may break associated mod sources." +
                                                    "\n\nFix servers: [" + string.Join(", ", invalidServers) + "]" +
                                                    "\n\n Continue?", "Invalid servers", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)))
            {
                ServersManager.Servers.Clear();
                foreach (var server in servers)
                    ServersManager.Servers.Add(server);
            }
            else e.Cancel = true;
        }

        private void ServersForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            tbName.UnbindLabel();
            tbPattern.UnbindLabel();
            tbURL.UnbindLabel();
            ServersManager.NormalizeServers();
        }

    }
}
