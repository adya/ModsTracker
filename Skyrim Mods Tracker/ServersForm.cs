using SMT.Managers;
using SMT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SMT
{
    public partial class ServersForm : Form
    {
        private int oldHeight;
        private List<Server> servers;
        //private List<Server> original;

        public ServersForm()
        {
            InitializeComponent();
            servers = ServersManager.Servers.ToList();
       //     original = servers.Select(s => new Server(s.ID) {Name = s.Name, URL = s.URL, VersionPattern = s.VersionPattern }).ToList();
            bsServers.DataSource = servers;
        }

        private void ServersForm_SizeChanged(object sender, EventArgs e)
        {
            dgvServers.Height += this.Height - oldHeight;
            oldHeight = this.Height;
        }

        private void ServersForm_ResizeBegin(object sender, EventArgs e)
        {
            oldHeight = Height;
        }

        private void ServersForm_Load(object sender, EventArgs e)
        {
            dgvServers.ClearSelection();
        }

        private void dgvServers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = dgvServers.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    dgvServers.ClearSelection();
                    bsServers.SuspendBinding();
                    tbName.Clear();
                    tbPattern.Clear();
                    tbURL.Clear();
                }
                else if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
                    bsServers.ResumeBinding();
            }
        }

        private bool ValidateServer(Server server)
        {
            return server.Name != null && server.Name != "" &&
                    server.URL != null && server.URL != "" &&
                    server.VersionPattern != null && server.VersionPattern != "";
        }

        private bool ValidateName()
        {
            var cur = bsServers.Current as Server;
            return !(servers.FindAll(s => s.Name.Equals(tbName.Text) && !s.Equals(cur)).Count > 0);
        }

        private bool ValidateURL()
        {
            var cur = bsServers.Current as Server;
            try {
                Uri uri = new Uri(tbURL.Text);
                return uri.Host != null && uri.Host != "" && !(servers.FindAll(s => s.URL.Equals(tbURL.Text) && !s.Equals(cur)).Count > 0);
            }
            catch (Exception e) { return false; }
            
        }

        private void ServersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bsServers.IsBindingSuspended) return;
            if (ValidateName() && ValidateURL() && ValidateServer(bsServers.Current as Server))
                bsServers.EndEdit();
            ServersManager.Servers.Clear();
            foreach (var server in servers)
            {
                if (ValidateServer(server))
                    ServersManager.Servers.Add(server);
            }
        }



        private void bAdd_Click(object sender, EventArgs e)
        {
            bsServers.AddNew();
            tbName.Focus();
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            bsServers.RemoveCurrent();
        }

        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateName();
            if (e.Cancel) MessageBox.Show("Server with that name already exists");
        }

        private void tbURL_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateURL();
            if (e.Cancel) MessageBox.Show("Server with that URL already exists");
            else tbURL.Text = new Uri(tbURL.Text).Host;
        }
    }
}
