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
using ofs.Properties;

namespace ofs
{
    public partial class frmMain : Form
    {
        BindingSource bs = new BindingSource();
        OfsContext ctx = new OfsContext();
        public frmMain()
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

        private void mnuBalanceEditor_Click(object sender, EventArgs e)
        {
            var lst = new List<Balance>();
            var f = new frmBalanceParameters();
            if (f.ShowDialog() != DialogResult.OK) return;
            var bExistBalance = ctx.Balances.Any(s => s.Inn == f.Inn && s.Year == f.Year && s.Quater == f.Quater);
            if (!bExistBalance)
            {
                if (MessageBox.Show($"Баланс не существует. Добавить?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                else
                {
                    var bl = ctx.Blines.OrderBy(s => s.Code);
                    foreach (var o in bl)
                    {
                        var bal = new Balance()
                            { Inn = f.Inn, Year = f.Year, Quater = f.Quater, Code = o.Code, Sm = 0 };
                        ctx.Balances.Add(bal);
                    }
                    ctx.SaveChanges();
                }
            }
            lst = ctx.Balances.Where
                (s => s.Inn == f.Inn && s.Year == f.Year && s.Quater == f.Quater)
                .OrderBy(s => s.Code).ToList();

            if (Controls.ContainsKey("g"))
            {
                Controls.RemoveByKey("g");
            }
        }

        private void mnuLoadFromExcel_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = Settings.Default.ExcelPath;
            ofd.Filter = "Файлы Excel (*.xlsx)|*.xlsx";
            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                new Utils().LoadBalance(ctx, ofd.FileName);
            }
            catch (LoadExcelException e1)
            {
                MessageBox.Show(e1.Message);
                return;
            }
        }
    }
}
