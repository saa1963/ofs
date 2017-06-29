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
    public partial class frmClientsEdit : Form
    {
        public frmClientsEdit()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public string Inn
        {
            get { return tbInn.Text; }
            set { tbInn.Text = value; }
        }

        public string NameClient
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }

        public string Okved
        {
            get { return tbOkved.Text; }
            set { tbOkved.Text = value; }
        }
    }
}
