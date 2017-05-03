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
    public partial class frmBlinesEdit : Form
    {
        public frmBlinesEdit()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public string Part
        {
            get { return tbPart.Text; }
            set { tbPart.Text = value; }
        }

        public string Line
        {
            get { return tbLine.Text; }
            set { tbLine.Text = value; }
        }

        public string NameLine
        {
            get { return tbNameLine.Text; }
            set { tbNameLine.Text = value; }
        }
    }
}
