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
    public partial class frmBlines : Form
    {
        SortableBindingList<Client> lst = new SortableBindingList<Client>();
        BindingSource bs = new BindingSource();
        OfsContext ctx = new OfsContext();
        public frmBlines()
        {
            InitializeComponent();
            g.AutoGenerateColumns = false;
            RefreshData();
        }

        private void RefreshData(Bline o = null)
        {
            bs.DataSource = null;
            bs.DataSource = ctx.Blines.OrderBy(s => s.CodeSort).ToList();
            g.DataSource = bs;
            if (o != null)
            {
                bs.Position = bs.IndexOf(o);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Bline o;
            var f = new frmBlinesEdit();
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (ctx.Blines.Find(f.Code) != null)
                {
                    MessageBox.Show($"Строка с кодом {f.Code} уже существует");
                }
                else
                {
                    o = new Bline() { Code = f.Code, Name = f.NameLine, IsNegative = f.IsNegative,
                                            Calculated = f.Calculated, CodeSort = f.CodeSort };
                    ctx.Blines.Add(o);
                    ctx.SaveChanges();
                    RefreshData(o);
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (bs.Current == null) return;
            var o = bs.Current as Bline;
            if (ctx.Balances.Any(s => s.Code == o.Code))
            {
                MessageBox.Show($"Данная строка присутствует в балансах. Удалить нельзя.");
                return;
            }
            ctx.Blines.Remove(o);
            ctx.SaveChanges();
            RefreshData();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (bs.Current == null) return;
            var o = bs.Current as Bline;
            var f = new frmBlinesEdit();
            f.Code = o.Code;
            f.NameLine = o.Name;
            f.IsNegative = o.IsNegative;
            f.Calculated = o.Calculated;
            f.CodeSort = o.CodeSort;
            f.tbCode.Enabled = false;
            if (f.ShowDialog() == DialogResult.OK)
            {
                o.Name = f.NameLine;
                o.Calculated = f.Calculated;
                o.CodeSort = f.CodeSort;
                o.IsNegative = f.IsNegative;
                ctx.SaveChanges();
                RefreshData(o);
            }
        }
    }
}
