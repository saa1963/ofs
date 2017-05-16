using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ofs
{
    public partial class frmBalanceParameters : Form
    {
        BindingSource bs = new BindingSource();
        OfsContext ctx = new OfsContext();
        public frmBalanceParameters()
        {
            InitializeComponent();
            var lstYears = new List<int>();
            for (int i = DateTime.Now.Year - 10; i < DateTime.Now.Year + 20; i++)
            {
                lstYears.Add(i);
            }
            tbYear.DataSource = lstYears;
            tbYear.SelectedItem = DateTime.Now.Year;
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

        public int Year
        {
            get { return (int)tbYear.SelectedItem; }
        }

        public int Quater
        {
            get { return tbQuater.SelectedIndex + 1; }
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
