using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ofs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = Application.ProductName;
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();
        }

        private void mnuClients_Click(object sender, EventArgs e)
        {
            new frmClients().ShowDialog();
        }

        private void mnuBlines_Click(object sender, EventArgs e)
        {
            new frmBlines().ShowDialog();
        }
    }
}
