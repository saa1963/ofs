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

        private void mnuOfs_Click(object sender, EventArgs e)
        {
            var f = new frmOfsParameters();
            if (f.ShowDialog() == DialogResult.OK)
            {
                var q = ctx.Balances.Where(s => s.Inn == f.Inn).GroupBy(s => new { year = s.Year, quater = s.Quater })
                    .OrderBy(s => s.Key.year).ThenBy(s => s.Key.quater).ToArray();
                if (q.Length == 0)
                {
                    MessageBox.Show("Не введены данные.");
                    return;
                }
                for (int i = 0; i < q.Length; i++)
                {
                    if (i == 0) continue;
                    if (Subtraction(q[i].Key.year, q[i].Key.quater, q[i - 1].Key.year, q[i - 1].Key.quater) != 1)
                    {
                        q = q.Take(i).ToArray();
                        break;
                    }
                }
                var ofs = new Utils().DoOfs(f.Inn, f.Quater, q);
                new Utils().OfsToExcel(ofs);
            }
        }

        private int Subtraction(int year1, int quater1, int year2, int quater2)
        {
            if (year1 == year2)
            {
                return quater1 - quater2;
            }
            else if (year1 - year2 == 1)
            {
                if (quater1 == 1 && quater2 == 4)
                    return 1;
                else
                    return 0;
            }
            else
                return 0;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DeleteTempFiles();
        }

        private void DeleteTempFiles()
        {
            var files = Directory.GetFiles(Path.GetTempPath(), "__ofs__*.xlsx");
            foreach (var f in files)
            {
                try
                {
                    File.Delete(f);
                }
                catch
                { }
            }
        }
    }
}
