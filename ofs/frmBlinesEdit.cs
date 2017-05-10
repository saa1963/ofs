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

        public string Code
        {
            get { return tbCode.Text; }
            set { tbCode.Text = value; }
        }

        public string NameLine
        {
            get { return tbNameLine.Text; }
            set { tbNameLine.Text = value; }
        }

        public bool IsNegative
        {
            get { return tbIsNegative.Checked; }
            set { tbIsNegative.Checked = value; }
        }

        public string Calculated
        {
            get { return tbCalculated.Text; }
            set { tbCalculated.Text = value; }
        }

        public int CodeSort
        {
            get { return (int)tbCodeSort.Value; }
            set { tbCodeSort.Value = value; }
        }
    }
}
