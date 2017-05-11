using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ofs
{
    public partial class frmOfsParameters : Form
    {
        BindingSource bs = new BindingSource();
        OfsContext ctx = new OfsContext();
        public frmOfsParameters()
        {
            InitializeComponent();
            var lstQuaters = new List<int>() { 1, 2, 3, 4 };
            tbQuater.DataSource = lstQuaters;

            bs.DataSource = ctx.Clients.OrderBy(s => s.Inn).ToList();
            tbClient.DataSource = bs;
            tbClient.DisplayMember = "Name";
            tbClient.ValueMember = "Inn";
        }

        public string Inn
        {
            get { return tbClient.SelectedValue.ToString(); }
        }

        public int Quater
        {
            get { return (int)tbQuater.SelectedItem; }
        }

        public Client Client
        {
            get { return tbClient.SelectedItem as Client; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            var f = new frmClients();
            if (f.ShowDialog() == DialogResult.OK)
            {
                tbClient.SelectedValue = f.SelectedClient.Inn;
            }
        }
    }
}
