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
    public partial class frmClients : Form
    {
        SortableBindingList<Client> lst = new SortableBindingList<Client>();
        BindingSource bs = new BindingSource();
        OfsContext ctx = new OfsContext();
        public frmClients()
        {
            InitializeComponent();
            g.AutoGenerateColumns = false;
            //foreach(var o in ctx.Clients)
            //{
            //    lst.Add(o);
            //}
            RefreshData();
        }

        private void RefreshData(Client o = null)
        {
            bs.DataSource = null;
            bs.DataSource = ctx.Clients.OrderBy(s => s.Inn).ToList();
            g.DataSource = bs;
            if (o != null)
            {
                bs.Position = bs.IndexOf(o);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Client o;
            var f = new frmClientsEdit();
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (ctx.Clients.Find(f.Inn) != null)
                {
                    MessageBox.Show($"Клиент с ИНН {f.Inn} уже существует.");
                }
                else
                {
                    o = new Client() { Inn = f.Inn, Name = f.NameClient };
                    ctx.Clients.Add(o);
                    ctx.SaveChanges();
                    RefreshData(o);
                }
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (bs.Current == null) return;
            var o = bs.Current as Client;
            var f = new frmClientsEdit();
            f.Inn = o.Inn;
            f.NameClient = o.Name;
            f.tbInn.Enabled = false;
            if (f.ShowDialog() == DialogResult.OK)
            {
                o.Name = f.NameClient;
                ctx.SaveChanges();
                RefreshData(o);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (bs.Current == null) return;
            var o = bs.Current as Client;
            if (ctx.Balances.Any(s => s.Inn == o.Inn))
            {
                MessageBox.Show($"Для клиента {o.Name} введены балансы. Удалить нельзя.");
                return;
            }
            ctx.Clients.Remove(o);
            ctx.SaveChanges();
            RefreshData();
        }
    }
}
