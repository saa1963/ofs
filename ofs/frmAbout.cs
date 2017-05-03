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
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            // Get the version of the executing assembly (that is, this assembly).
            Assembly assem = Assembly.GetEntryAssembly();
            AssemblyName assemName = assem.GetName();
            Version ver = assemName.Version;
            lblProductName.Text = Application.ProductName;
            lblVersion.Text = ver.ToString();
            lblVersionDate.Text = (new FileInfo(Application.ExecutablePath)).LastWriteTime.ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
