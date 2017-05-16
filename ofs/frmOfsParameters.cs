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
            var lstQuaters = new List<string>() { "1 квартал", "1 полугодие", "9 месяцев", "Год" };
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
            get { return tbQuater.SelectedIndex + 1; }
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

        private void btnOk_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
