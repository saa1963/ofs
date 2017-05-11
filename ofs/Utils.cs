using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;


namespace ofs
{
    public class Utils
    {
        public void LoadBalance(OfsContext ctx, string fname)
        {
            using (var package = new ExcelPackage(new FileInfo(fname)))
            {
                var wshF2 = package.Workbook.Worksheets["Форма2"];
                var wshNastr = package.Workbook.Worksheets["Настройки"];
                string inn = wshNastr.Cells[2, 2].Value.ToString().Trim();
                if (ctx.Clients.Find(inn) == null)
                {
                    ctx.Clients.Add(new Client()
                    {
                        Inn = inn,
                        Name = wshNastr.Cells[1, 2].Value.ToString()
                    });
                    ctx.SaveChanges();
                }
                var wsh = package.Workbook.Worksheets["Баланс"];
                int col = 3;
                int row = 10;
                var Cols = new Dictionary<int, QYear>();
                while (true)
                {
                    if (wsh.Cells[row, col].Value is DateTime)
                    {
                        Cols.Add(col, QYear.FromDate((DateTime)wsh.Cells[row, col].Value));
                    }
                    else
                    {
                        break;
                    }
                    col++;
                }
                Balance bal;
                string code;
                foreach (var cl in Cols)
                {
                    row = 13;
                    while (row < 56)
                    {
                        var ocode = wsh.Cells[row, 2].Value;
                        if (ocode is double)
                        {
                            code = ocode.ToString();
                            if (!String.IsNullOrWhiteSpace(code))
                            {
                                bal = new Balance();
                                bal.Code = code;
                                bal.Inn = wshNastr.Cells[2, 2].Value.ToString();
                                bal.Quater = cl.Value.Quater;
                                bal.Year = cl.Value.Year;
                                ocode = wsh.Cells[row, cl.Key].Value;
                                if (ocode != null)
                                {
                                    bal.Sm = Convert.ToInt32(ocode);
                                }
                                else
                                {
                                    bal.Sm = 0;
                                }
                                ctx.Balances.Add(bal);
                            }
                        }
                        row++;
                    }
                }

                col = 3;
                row = 9;
                DateTime? dt;
                Cols = new Dictionary<int, QYear>();
                while (true)
                {
                    dt = ToDate(wshF2.Cells[row, col].Value);
                    if (dt.HasValue)
                    {
                        Cols.Add(col, QYear.FromDate(dt.Value));
                    }
                    else
                    {
                        break;
                    }
                    col++;
                }

                foreach (var cl in Cols)
                {
                    row = 10;
                    while (row < 35)
                    {
                        var ocode = wshF2.Cells[row, 2].Value;
                        if (ocode is string)
                        {
                            code = ocode.ToString();
                            if (!String.IsNullOrWhiteSpace(code))
                            {
                                bal = new Balance();
                                bal.Code = code;
                                bal.Inn = wshNastr.Cells[2, 2].Value.ToString();
                                bal.Quater = cl.Value.Quater;
                                bal.Year = cl.Value.Year;
                                ocode = wshF2.Cells[row, cl.Key].Value;
                                if (ocode != null)
                                {
                                    bal.Sm = Convert.ToInt32(ocode);
                                }
                                else
                                {
                                    bal.Sm = 0;
                                }
                                if (ctx.Balances.Find(bal.Quater, bal.Year, bal.Inn, bal.Code) == null)
                                {
                                    ctx.Balances.Add(bal);
                                }
                            }
                        }
                        row++;
                    }
                }
                ctx.SaveChanges();
                System.Windows.Forms.MessageBox.Show("Загрузка закончена.");
            }
        }

        private DateTime? ToDate(object value)
        {
            if (value != null)
            {
                int year, month;
                var s = value.ToString();
                if (s.Contains("2013"))
                {
                    year = 2013;
                }
                else if (s.Contains("2014"))
                {
                    year = 2014;
                }
                else if (s.Contains("2015"))
                {
                    year = 2015;
                }
                else if (s.Contains("2016"))
                {
                    year = 2016;
                }
                else if (s.Contains("2017"))
                {
                    year = 2017;
                }
                else
                    return null;
                if (s.Contains("1 кв"))
                {
                    month = 4;
                }
                else if (s.Contains("1 полу"))
                {
                    month = 7;
                }
                else if (s.Contains("9 мес"))
                {
                    month = 10;
                }
                else
                {
                    month = 1;
                    year++;
                }
                return new DateTime(year, month, 1);
            }
            else
                return null;
        }
    }

    class QYear
    {
        public int Year { get; set; }
        public int Quater { get; set; }
        public static QYear FromDate(DateTime dt)
        {
            var o = new QYear();
            if (dt.Month == 1)
            {
                o.Quater = 4;
            }
            else if (dt.Month == 4)
            {
                o.Quater = 1;
            }
            else if (dt.Month == 7)
            {
                o.Quater = 2;
            }
            else if (dt.Month == 10)
            {
                o.Quater = 3;
            }
            else
            {
                throw new LoadExcelException($"Неверно проставлена дата {dt.ToString("dd.MM.yyyy")}");
            }
            if (o.Quater == 4)
            {
                o.Year = dt.Year - 1;
            }
            else
            {
                o.Year = dt.Year;
            }
            return o;
        }
    }

    class LoadExcelException: Exception
    {
        public LoadExcelException(string p) : base(p) { }
    }
}
