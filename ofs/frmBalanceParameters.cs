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

        public int Year
        {
            get { return (int)tbYear.SelectedItem; }
        }

        public int Quater
        {
            get { return (int)tbQuater.SelectedItem; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
