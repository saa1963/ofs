using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ofs
{
    public class OfsContext : System.Data.Entity.DbContext
    {
        public OfsContext() : base("Ofs") { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Bline> Blines { get; set; }
        public DbSet<Balance> Balances { get; set; }

        public override int SaveChanges()
        {
            int rt = 0;
            try
            {
                rt = base.SaveChanges();
            }
            catch (DbEntityValidationException e1)
            {
                foreach (var e2 in e1.EntityValidationErrors)
                {
                    foreach (var e3 in e2.ValidationErrors)
                    {
                        System.Windows.Forms.MessageBox.Show(e3.ErrorMessage);
                    }
                }
            }
            catch (Exception e1)
            {
                System.Windows.Forms.MessageBox.Show(e1.Message);
                if (e1.InnerException != null)
                {
                    System.Windows.Forms.MessageBox.Show(e1.InnerException.Message);
                    if (e1.InnerException.InnerException != null)
                    {
                        System.Windows.Forms.MessageBox.Show(e1.InnerException.InnerException.Message);
                    }
                }
            }
            return rt;
        }
    }
}
