using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ofs
{
    public partial class frmBalance : Form
    {
        OfsContext ctx;
        BindingSource bs = new BindingSource();
        private List<Balance> lst;

        public frmBalance(OfsContext ctx, List<Balance> lst)
        {
            InitializeComponent();
            this.lst = lst;
            this.ctx = ctx;
            g.AutoGenerateColumns = false;
            RefreshData();
        }

        private void RefreshData()
        {
            bs.DataSource = null;
            bs.DataSource = lst;
            g.DataSource = bs;
        }

        private void g_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var balance = g.Rows[e.RowIndex].DataBoundItem as Balance;
            if (!String.IsNullOrWhiteSpace(balance.Bline.Calculated))
            {
                g.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                g.Rows[e.RowIndex].ReadOnly = true;
            }
            else if (balance.Bline.IsNegative)
            {
                g.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Cyan;
            }
        }

        private void g_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CalculateFields();
        }

        private void CalculateFields()
        {
            int pos = bs.Position;
            for (int i = 0; i < g.Rows.Count - 1; i++)
            {
                var balance = g.Rows[i].DataBoundItem as Balance;
                if (!String.IsNullOrWhiteSpace(balance.Bline.Calculated))
                {
                    var rpn = RPN.CreateRPN(RPN.StandartToRPN(balance.Bline.Calculated));
                    foreach (var lx in rpn.Ast)
                    {
                        if (lx.Type == LexemType.Variable)
                        {
                            rpn.SetVariable(lx.Value, lst.Single(s => s.Code == lx.Value.Substring(1)).Sm);
                        }
                    }
                    rpn.Execute();
                    balance.Sm = Convert.ToInt32(rpn.Result);
                }
            }
            RefreshData();
            bs.Position = pos;
        }

        

        private void frmBalance_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lst.Single(s => s.Code == "1600").Sm != lst.Single(s => s.Code == "1700").Sm)
            {
                var rt = MessageBox.Show("Актив и пассив баланса не равны. Выйти без сохранения результатов?", "", MessageBoxButtons.YesNo);
                if (rt != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if (lst.Single(s => s.Code == "1600").Sm > 0 || lst.Single(s => s.Code == "2400").Sm > 0)
                {
                    ctx.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Нет данных для сохранения.");
                }
            }
        }
    }
}
