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
            bs.DataSource = ctx.Blines.OrderBy(s => s.Part).ThenBy(s => s.Line).ToList();
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
                if (ctx.Blines.Find(f.Part, f.Line) != null)
                {
                    MessageBox.Show($"Строка с кодом 1{f.Part}{f.Line} уже существует");
                }
                else
                {
                    o = new Bline() { Part = f.Part, Line = f.Line, Name = f.NameLine };
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
            if (ctx.Balances.Any(s => s.Part == o.Part && s.Line == o.Line))
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
            f.Part = o.Part;
            f.Line = o.Line;
            f.NameLine = o.Name;
            f.tbPart.Enabled = false;
            f.tbLine.Enabled = false;
            if (f.ShowDialog() == DialogResult.OK)
            {
                o.Name = f.NameLine;
                ctx.SaveChanges();
                RefreshData(o);
            }
        }
    }
}
