using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Data.SqlClient;

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

                ctx.Database.ExecuteSqlCommand("delete from balances where inn = @inn", new SqlParameter("@inn", inn));

                var wsh = package.Workbook.Worksheets["Баланс"];
                int col = 3;
                int row = 10;
                var Cols = new Dictionary<int, QYear>();
                while (true)
                {
                    if (wsh.Cells[row, col].Value is DateTime)
                    {
                        if (Convert.ToInt32(wsh.Cells[31, col].Value) > 0)
                        {
                            Cols.Add(col, QYear.FromDate((DateTime)wsh.Cells[row, col].Value));
                        }
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
                                if (ctx.Balances.Find(bal.Quater, bal.Year, bal.Inn, bal.Code) == null)
                                {
                                    if (bal.Year < 2017)
                                    {
                                        ctx.Balances.Add(bal);
                                    }
                                }
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
                        if (Convert.ToInt32(wsh.Cells[31, col].Value) > 0)
                        {
                            Cols.Add(col, QYear.FromDate(dt.Value));
                        }
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
                        if (ocode is double)
                        {
                            ocode = ocode.ToString();
                        }
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
                                    if (bal.Year < 2017)
                                    {
                                        ctx.Balances.Add(bal);
                                    }
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

        internal void DoF2(Client selectedClient)
        {
            using (var ctx = new OfsContext())
            {
                using (var package = new ExcelPackage())
                {
                    var wsh = package.Workbook.Worksheets.Add("Лист1");

                    wsh.Column(1).Width = 32.75;
                    wsh.DefaultColWidth = 12;
                    wsh.Column(1).Style.WrapText = true;
                    wsh.Row(7).Style.WrapText = true;
                    wsh.Row(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    wsh.Row(8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    wsh.Cells[1, 1].Value = "Отчет о прибылях и убытках";
                    wsh.Cells[1, 1].Style.Font.Bold = true;
                    wsh.Cells[1, 1].Style.Font.Size = 14;
                    wsh.Cells[3, 1].Value = "Организация";
                    wsh.Cells[3, 2].Value = ctx.Clients.Find(selectedClient.Inn).Name;
                    wsh.Cells[3, 2].Style.Font.Bold = true;
                    wsh.Cells[4, 1].Value = "ИНН";
                    wsh.Cells[4, 2].Value = selectedClient.Inn;
                    wsh.Cells[4, 2].Style.Font.Bold = true;
                    wsh.Cells[5, 1].Value = "Отрасль";
                    wsh.Cells[5, 2].Style.Font.Bold = true;
                    wsh.Cells[6, 1].Value = "Единица измерения (абсолютные показатели)";
                    wsh.Cells[6, 2].Value = "тыс.руб.";
                    wsh.Cells[6, 2].Style.Font.Bold = true;

                    wsh.Cells[10, 1].Value = "Наименование показателя";
                    wsh.Cells[10, 2].Value = "Код";

                    var bals = ctx.Balances.Where(s => s.Inn == selectedClient.Inn && s.Code == "1100").OrderBy(s => s.Year).ThenBy(s => s.Quater).ToList();
                    var col = 3;
                    foreach (var b in bals)
                    {
                        wsh.Cells[10, col].Value =
                            QYear.StringFromQuater(new QYear() { Year = b.Year, Quater = b.Quater });
                        col++;
                    }

                    //var bal = ctx.Balances.Include("Bline").Where(s => s.Inn == selectedClient.Inn && s.Bline.Code.Substring(0, 1) == "1").
                    var bl = ctx.Blines.Where(s => s.Code.Substring(0, 1) == "2").OrderBy(s => s.CodeSort).ToList();
                    //var ins = bl.IndexOf(bl.Single(s => s.Code == "1110"));
                    //bl.Insert(ins, new Bline() { Name = "АКТИВ", Code = "" });
                    //bl.Insert(ins + 1, new Bline() { Name = "I. Внеоборотные активы", Code = "" });
                    //ins = bl.IndexOf(bl.Single(s => s.Code == "1210"));
                    //bl.Insert(ins, new Bline() { Name = "II. Оборотные активы", Code = "" });
                    //ins = bl.IndexOf(bl.Single(s => s.Code == "1310"));
                    //bl.Insert(ins, new Bline() { Name = "ПАССИВ", Code = "" });
                    //bl.Insert(ins + 1, new Bline() { Name = "III. Капитал и резервы", Code = "" });
                    //ins = bl.IndexOf(bl.Single(s => s.Code == "1410"));
                    //bl.Insert(ins, new Bline() { Name = "IV. Долгосрочные обязательства", Code = "" });
                    //ins = bl.IndexOf(bl.Single(s => s.Code == "1510"));
                    //bl.Insert(ins, new Bline() { Name = "IV. Краткосрочные обязательства", Code = "" });

                    var row = 11;
                    foreach (var bline in bl)
                    {
                        wsh.Cells[row, 1].Value = bline.Name;
                        wsh.Cells[row, 2].Value = bline.Code;
                        if (bline.Code != "")
                        {
                            bals = ctx.Balances.Where(s => s.Inn == selectedClient.Inn && s.Code == bline.Code).OrderBy(s => s.Year).ThenBy(s => s.Quater).ToList();
                            col = 3;
                            foreach (var b in bals)
                            {
                                wsh.Cells[row, col].Value = b.Sm;
                                col++;
                            }
                        }
                        row++;
                    }


                    wsh.Cells[10, 1, wsh.Dimension.End.Row, wsh.Dimension.End.Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    wsh.Cells[10, 1, wsh.Dimension.End.Row, wsh.Dimension.End.Column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    wsh.Cells[10, 1, wsh.Dimension.End.Row, wsh.Dimension.End.Column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    wsh.Cells[10, 1, wsh.Dimension.End.Row, wsh.Dimension.End.Column].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    package.File = new FileInfo(Path.Combine(Path.GetTempPath(), "__ofs__" + Guid.NewGuid().ToString() + ".xlsx"));
                    package.Save();

                    Process prc = new Process();
                    prc.StartInfo.Arguments = "\"" + package.File + "\"";
                    prc.StartInfo.FileName = "excel.exe";
                    prc.Start();
                }
            }
        }

        internal void DoBalances(Client selectedClient)
        {
            using (var ctx = new OfsContext())
            {
                using (var package = new ExcelPackage())
                {
                    var wsh = package.Workbook.Worksheets.Add("Лист1");

                    wsh.Column(1).Width = 32.75;
                    wsh.DefaultColWidth = 12;
                    wsh.Column(1).Style.WrapText = true;
                    wsh.Row(7).Style.WrapText = true;
                    wsh.Row(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    wsh.Row(8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    wsh.Cells[1, 1].Value = "Бухгалтерский баланс";
                    wsh.Cells[1, 1].Style.Font.Bold = true;
                    wsh.Cells[1, 1].Style.Font.Size = 14;
                    wsh.Cells[3, 1].Value = "Организация";
                    wsh.Cells[3, 2].Value = ctx.Clients.Find(selectedClient.Inn).Name;
                    wsh.Cells[3, 2].Style.Font.Bold = true;
                    wsh.Cells[4, 1].Value = "ИНН";
                    wsh.Cells[4, 2].Value = selectedClient.Inn;
                    wsh.Cells[4, 2].Style.Font.Bold = true;
                    wsh.Cells[5, 1].Value = "Отрасль";
                    wsh.Cells[5, 2].Style.Font.Bold = true;
                    wsh.Cells[6, 1].Value = "Единица измерения (абсолютные показатели)";
                    wsh.Cells[6, 2].Value = "тыс.руб.";
                    wsh.Cells[6, 2].Style.Font.Bold = true;

                    wsh.Cells[10, 1].Value = "Наименование показателя";
                    wsh.Cells[10, 2].Value = "Код";

                    var bals = ctx.Balances.Where(s => s.Inn == selectedClient.Inn && s.Code == "1100").OrderBy(s => s.Year).ThenBy(s => s.Quater).ToList();
                    var col = 3;
                    foreach (var b in bals)
                    {
                        wsh.Cells[10, col].Value = 
                            $"на {QYear.DateFromQuater(new QYear() { Year = b.Year, Quater = b.Quater}).ToString("dd.MM.yyyy")}";
                        col++;
                    }

                    //var bal = ctx.Balances.Include("Bline").Where(s => s.Inn == selectedClient.Inn && s.Bline.Code.Substring(0, 1) == "1").
                    var bl = ctx.Blines.Where(s => s.Code.Substring(0, 1) == "1").OrderBy(s => s.CodeSort).ToList();
                    var ins = bl.IndexOf(bl.Single(s => s.Code == "1110"));
                    bl.Insert(ins, new Bline() { Name = "АКТИВ", Code = "" });
                    bl.Insert(ins + 1, new Bline() { Name = "I. Внеоборотные активы", Code = "" });
                    ins = bl.IndexOf(bl.Single(s => s.Code == "1210"));
                    bl.Insert(ins, new Bline() { Name = "II. Оборотные активы", Code = "" });
                    ins = bl.IndexOf(bl.Single(s => s.Code == "1310"));
                    bl.Insert(ins, new Bline() { Name = "ПАССИВ", Code = "" });
                    bl.Insert(ins + 1, new Bline() { Name = "III. Капитал и резервы", Code = "" });
                    ins = bl.IndexOf(bl.Single(s => s.Code == "1410"));
                    bl.Insert(ins, new Bline() { Name = "IV. Долгосрочные обязательства", Code = "" });
                    ins = bl.IndexOf(bl.Single(s => s.Code == "1510"));
                    bl.Insert(ins, new Bline() { Name = "IV. Краткосрочные обязательства", Code = "" });

                    var row = 11;
                    foreach (var bline in bl)
                    {
                        wsh.Cells[row, 1].Value = bline.Name;
                        wsh.Cells[row, 2].Value = bline.Code;
                        if (bline.Code != "")
                        {
                            bals = ctx.Balances.Where(s => s.Inn == selectedClient.Inn && s.Code == bline.Code).OrderBy(s => s.Year).ThenBy(s => s.Quater).ToList();
                            col = 3;
                            foreach (var b in bals)
                            {
                                wsh.Cells[row, col].Value = b.Sm;
                                col++;
                            }
                        }
                        row++;
                    }


                    wsh.Cells[10, 1, wsh.Dimension.End.Row, wsh.Dimension.End.Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    wsh.Cells[10, 1, wsh.Dimension.End.Row, wsh.Dimension.End.Column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    wsh.Cells[10, 1, wsh.Dimension.End.Row, wsh.Dimension.End.Column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    wsh.Cells[10, 1, wsh.Dimension.End.Row, wsh.Dimension.End.Column].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    package.File = new FileInfo(Path.Combine(Path.GetTempPath(), "__ofs__" + Guid.NewGuid().ToString() + ".xlsx"));
                    package.Save();

                    Process prc = new Process();
                    prc.StartInfo.Arguments = "\"" + package.File + "\"";
                    prc.StartInfo.FileName = "excel.exe";
                    prc.Start();
                }
            }
        }

        internal void OfsToExcel(Ofs[] ofs)
        {
            if (ofs.Length == 0) return;
            using (var ctx = new OfsContext())
            {
                using (var package = new ExcelPackage())
                {
                    var wsh = package.Workbook.Worksheets.Add("Лист1");

                    wsh.Column(1).Width = 32.75;
                    wsh.DefaultColWidth = 12;
                    wsh.Column(1).Style.WrapText = true;
                    wsh.Row(7).Style.WrapText = true;
                    wsh.Row(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    wsh.Row(8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    wsh.Cells[1, 1].Value = "Оценка финансового состояния";
                    wsh.Cells[1, 1].Style.Font.Bold = true;
                    wsh.Cells[1, 1].Style.Font.Size = 14;
                    wsh.Cells[3, 1].Value = "Организация";
                    wsh.Cells[3, 2].Value = ctx.Clients.Find(ofs[0].Inn).Name;
                    wsh.Cells[3, 2].Style.Font.Bold = true;
                    wsh.Cells[4, 1].Value = "ИНН";
                    wsh.Cells[4, 2].Value = ofs[0].Inn;
                    wsh.Cells[4, 2].Style.Font.Bold = true;
                    wsh.Cells[5, 1].Value = "Единица измерения (абсолютные показатели)";
                    wsh.Cells[5, 2].Value = "тыс.руб.";
                    wsh.Cells[5, 2].Style.Font.Bold = true;

                    wsh.Cells[7, 1].Value = "Наименование показателей";
                    wsh.Cells[8, 1].Value = "1";
                    wsh.Cells[9, 1].Value = "Коэффициент финансовой независимости";
                    wsh.Cells[10, 1].Value = "Коэффициент обеспеченности собственными оборотными средствами";
                    wsh.Cells[11, 1].Value = "Коэффициент соотношения оборотных и внеоборотных активов";
                    wsh.Cells[12, 1].Value = "Общий коэффициент ликвидности";
                    wsh.Cells[13, 1].Value = "Коэффициент покрытия";
                    wsh.Cells[14, 1].Value = "Коэффициент оборачиваемости активов";
                    wsh.Cells[15, 1].Value = "Рентабельность продаж";
                    wsh.Cells[16, 1].Value = "Рентабельность собственного капитала (чистых активов)";

                    wsh.Cells[18, 1].Value = "Динамика отдельных показателей";
                    wsh.Cells[18, 1].Style.Font.Bold = true;

                    wsh.Cells[20, 1].Value = "Валюта баланса";
                    wsh.Cells[21, 1].Value = "Внеоборотные активы в т.ч.";
                    wsh.Cells[22, 1].Value = "основные средства";
                    wsh.Cells[23, 1].Value = "Оборотные активы в т.ч.";
                    wsh.Cells[24, 1].Value = "запасы";
                    wsh.Cells[25, 1].Value = "дебиторская задолженность";
                    wsh.Cells[26, 1].Value = "финансовые вложения";
                    wsh.Cells[27, 1].Value = "Капитал и резервы в т.ч.";
                    wsh.Cells[28, 1].Value = "нераспределенная прибыль";
                    wsh.Cells[29, 1].Value = "Чистые активы";
                    wsh.Cells[30, 1].Value = "Выручка";
                    wsh.Cells[31, 1].Value = "Себестоимость продаж";
                    wsh.Cells[32, 1].Value = "Прибыль от продаж";
                    wsh.Cells[33, 1].Value = "Чистая прибыль";

                    int j = 2;
                    for (int i = 0; i < ofs.Length; i++)
                    {
                        wsh.Cells[7, j].Value = DateFromQuater(ofs[i].Year, ofs[i].Quater);
                        wsh.Cells[7, j].Style.Numberformat.Format = "dd.MM.yyyy";
                        wsh.Cells[7, j].Style.Font.Bold = true;
                        wsh.Cells[8, j].Value = j.ToString();
                        wsh.Cells[9, j].Value = ofs[i].Kfn;
                        wsh.Cells[10, j].Value = ofs[i].Kosos;
                        wsh.Cells[11, j].Value = ofs[i].Ksova;
                        wsh.Cells[12, j].Value = ofs[i].Okl;
                        wsh.Cells[13, j].Value = ofs[i].Kp;
                        wsh.Cells[14, j].Value = ofs[i].Koa;
                        wsh.Cells[15, j].Value = ofs[i].Rp;
                        wsh.Cells[16, j].Value = ofs[i].Rsk;

                        wsh.Cells[20, j].Value = ofs[i].Vb;
                        wsh.Cells[21, j].Value = ofs[i].Va;
                        wsh.Cells[22, j].Value = ofs[i].Os;
                        wsh.Cells[23, j].Value = ofs[i].Oa;
                        wsh.Cells[24, j].Value = ofs[i].Zap;
                        wsh.Cells[25, j].Value = ofs[i].Dz;
                        wsh.Cells[26, j].Value = ofs[i].Fv;
                        wsh.Cells[27, j].Value = ofs[i].Kir;
                        wsh.Cells[28, j].Value = ofs[i].Np;
                        wsh.Cells[29, j].Value = ofs[i].Cha;
                        wsh.Cells[30, j].Value = ofs[i].Vir;
                        wsh.Cells[31, j].Value = ofs[i].Sp;
                        wsh.Cells[32, j].Value = ofs[i].Pop;
                        wsh.Cells[33, j].Value = ofs[i].Chp;
                        j++;
                        if (j == 3) continue;
                        if (j == 4)
                            wsh.Cells[7, j].Value = $"Изменения (гр.{j-1} - гр.{j-2})";
                        else
                            wsh.Cells[7, j].Value = $"Изменения (гр.{j - 1} - гр.{j - 3})";
                        wsh.Cells[8, j].Value = j.ToString();
                        wsh.Cells[9, j].Value = ofs[i].Kfn - ofs[i - 1].Kfn;
                        wsh.Cells[10, j].Value = ofs[i].Kosos - ofs[i - 1].Kosos;
                        wsh.Cells[11, j].Value = ofs[i].Ksova - ofs[i - 1].Ksova;
                        wsh.Cells[12, j].Value = ofs[i].Okl - ofs[i - 1].Okl;
                        wsh.Cells[13, j].Value = ofs[i].Kp - ofs[i - 1].Kp;
                        wsh.Cells[14, j].Value = ofs[i].Koa - ofs[i - 1].Koa;
                        wsh.Cells[15, j].Value = ofs[i].Rp - ofs[i - 1].Rp;
                        wsh.Cells[16, j].Value = ofs[i].Rsk - ofs[i - 1].Rsk;

                        wsh.Cells[20, j].Value = ofs[i].Vb - ofs[i - 1].Vb;
                        wsh.Cells[21, j].Value = ofs[i].Va - ofs[i - 1].Va;
                        wsh.Cells[22, j].Value = ofs[i].Os - ofs[i - 1].Os;
                        wsh.Cells[23, j].Value = ofs[i].Oa - ofs[i - 1].Oa;
                        wsh.Cells[24, j].Value = ofs[i].Zap - ofs[i - 1].Zap;
                        wsh.Cells[25, j].Value = ofs[i].Dz - ofs[i - 1].Dz;
                        wsh.Cells[26, j].Value = ofs[i].Fv - ofs[i - 1].Fv;
                        wsh.Cells[27, j].Value = ofs[i].Kir - ofs[i - 1].Kir;
                        wsh.Cells[28, j].Value = ofs[i].Np - ofs[i - 1].Np;
                        wsh.Cells[29, j].Value = ofs[i].Cha - ofs[i - 1].Cha;
                        wsh.Cells[30, j].Value = ofs[i].Vir - ofs[i - 1].Vir;
                        wsh.Cells[31, j].Value = ofs[i].Sp - ofs[i - 1].Sp;
                        wsh.Cells[32, j].Value = ofs[i].Pop - ofs[i - 1].Pop;
                        wsh.Cells[33, j].Value = ofs[i].Chp - ofs[i - 1].Chp;
                        j++;
                    }
                    wsh.Cells[9, 2, wsh.Dimension.End.Row, wsh.Dimension.End.Column].Style.Font.Bold = true;
                    wsh.Cells[7, 1, 16, wsh.Dimension.End.Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    wsh.Cells[7, 1, 16, wsh.Dimension.End.Column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    wsh.Cells[7, 1, 16, wsh.Dimension.End.Column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    wsh.Cells[7, 1, 16, wsh.Dimension.End.Column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    wsh.Cells[20, 1, 33, wsh.Dimension.End.Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    wsh.Cells[20, 1, 33, wsh.Dimension.End.Column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    wsh.Cells[20, 1, 33, wsh.Dimension.End.Column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    wsh.Cells[20, 1, 33, wsh.Dimension.End.Column].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    package.File = new FileInfo(Path.Combine(Path.GetTempPath(), "__ofs__" + Guid.NewGuid().ToString() + ".xlsx"));
                    package.Save();

                    Process prc = new Process();
                    prc.StartInfo.Arguments = "\"" + package.File + "\"";
                    prc.StartInfo.FileName = "excel.exe";
                    prc.Start();
                }
            }
        }

        private object DateFromQuater(int year, int quater)
        {
            if (quater == 4)
            {
                return new DateTime(year + 1, 1, 1);
            }
            else if (quater == 3)
            {
                return new DateTime(year, 10, 1);
            }
            else if (quater == 2)
            {
                return new DateTime(year, 7, 1);
            }
            else if (quater == 1)
            {
                return new DateTime(year, 4, 1);
            }
            else
            {
                return null;
            }
        }

        internal Ofs[] DoOfs(string inn, int quater, IGrouping<QYear, Balance>[] q)
        {
            var days = new Dictionary<int, decimal>()
                { {1, 90m}, { 2, 181m}, {3, 273m}, {4, 365m} };
            Ofs[] rt = new Ofs[q.Length];
            for (int i = 0; i < q.Length; i++)
            {
                var o = q[i];
                if (o.Key.Quater != quater) continue;
                var o1 = q.FirstOrDefault(s => s.Key.MyEquals(o.Key.БлижайшийМинимальный4Квартал()));
                var ofs = new Ofs();
                ofs.Cha = o.Single(s => s.Code == "1300").Sm + o.Single(s => s.Code == "1530").Sm;
                ofs.Chp = o.Single(s => s.Code == "2400").Sm;
                ofs.Dz = o.Single(s => s.Code == "1230").Sm;
                ofs.Fv = o.Single(s => s.Code == "1240").Sm;
                ofs.Inn = inn;
                if (o.Single(s => s.Code == "1700").Smd != 0)
                {
                    ofs.Kfn = Decimal.Round((o.Single(s => s.Code == "1300").Smd + o.Single(s => s.Code == "1530").Smd)
                        / o.Single(s => s.Code == "1700").Smd, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    ofs.Kfn = 0;
                }
                ofs.Kir = o.Single(s => s.Code == "1300").Sm;

                if (o1 != null)
                    if (o.Single(s => s.Code == "2110").Smd != 0)
                    {
                        ofs.Koa = Decimal.Round(((o1.Single(s => s.Code == "1600").Smd + o.Single(s => s.Code == "1600").Smd)
                            * 0.5m / o.Single(s => s.Code == "2110").Smd) * days[quater], 0, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        ofs.Koa = 0;
                    }
                else
                {
                    ofs.Koa = 0;
                }

                if (o.Single(s => s.Code == "1200").Smd != 0)
                {
                    ofs.Kosos = Decimal.Round((o.Single(s => s.Code == "1300").Smd + o.Single(s => s.Code == "1530").Smd - o.Single(s => s.Code == "1100").Smd)
                        / o.Single(s => s.Code == "1200").Smd, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    ofs.Kosos = 0;
                }
                if (o.Single(s => s.Code == "1500").Smd - o.Single(s => s.Code == "1530").Smd != 0)
                {
                    ofs.Kp = Decimal.Round((o.Single(s => s.Code == "1210").Smd + o.Single(s => s.Code == "1220").Smd +
                        o.Single(s => s.Code == "1230").Smd + o.Single(s => s.Code == "1240").Smd + o.Single(s => s.Code == "1250").Smd)
                        / (o.Single(s => s.Code == "1500").Smd - o.Single(s => s.Code == "1530").Smd), 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    ofs.Kosos = 0;
                }
                if (o.Single(s => s.Code == "1100").Smd != 0)
                {
                    ofs.Ksova = Decimal.Round(o.Single(s => s.Code == "1200").Smd / o.Single(s => s.Code == "1100").Smd, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    ofs.Ksova = 0;
                }
                ofs.Np = o.Single(s => s.Code == "1370").Sm;
                ofs.Oa = o.Single(s => s.Code == "1200").Sm;
                if (o.Single(s => s.Code == "1500").Smd != 0)
                {
                    ofs.Okl = Decimal.Round(o.Single(s => s.Code == "1200").Smd / o.Single(s => s.Code == "1500").Smd, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    ofs.Okl = 0;
                }
                ofs.Os = o.Single(s => s.Code == "1150").Sm;
                ofs.Pop = o.Single(s => s.Code == "2200").Sm;
                ofs.Quater = quater;
                if (o.Single(s => s.Code == "2110").Smd != 0)
                {
                    ofs.Rp = Decimal.Round(o.Single(s => s.Code == "2200").Smd / o.Single(s => s.Code == "2110").Smd, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    ofs.Rp = 0;
                }
                if (o1 != null)
                    if (o1.Single(s => s.Code == "1300").Smd +
                        o1.Single(s => s.Code == "1530").Smd + o.Single(s => s.Code == "1300").Smd + o.Single(s => s.Code == "1530").Smd != 0)
                    {
                        ofs.Rsk = Decimal.Round(o.Single(s => s.Code == "2300").Smd / ((o1.Single(s => s.Code == "1300").Smd +
                            o1.Single(s => s.Code == "1530").Smd + o.Single(s => s.Code == "1300").Smd + o.Single(s => s.Code == "1530").Smd)
                            * 0.5m) * 365m / days[quater], 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        ofs.Rsk = 0;
                    }
                else
                {
                    ofs.Rsk = 0;
                }
                ofs.Sp = o.Single(s => s.Code == "2120").Sm;
                ofs.Va = o.Single(s => s.Code == "1100").Sm;
                ofs.Vb = o.Single(s => s.Code == "1600").Sm;
                ofs.Vir = o.Single(s => s.Code == "2110").Sm;
                ofs.Year = o.Key.Year;
                ofs.Zap = o.Single(s => s.Code == "1210").Sm;
                rt[i] = ofs;
            }
            rt = rt.Where(s => s != null).OrderBy(s => s.Year).ThenBy(s => s.Quater).ToArray();
            return rt;
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

    public class QYear
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

        public static DateTime DateFromQuater(QYear q)
        {
            switch (q.Quater)
            {
                case 1:
                    return new DateTime(q.Year, 4, 1);
                case 2:
                    return new DateTime(q.Year, 7, 1);
                case 3:
                    return new DateTime(q.Year, 10, 1);
                case 4:
                    return new DateTime(q.Year + 1, 1, 1);
                default:
                    return DateTime.MinValue;
            }
        }

        public static QYear operator -(QYear q1, int n)
        {
            int qv = (q1.Year * 4 + q1.Quater - n);
            return new QYear() { Year = qv / 4, Quater = qv % 4 };
        }

        public QYear БлижайшийМинимальный4Квартал()
        {
            return new QYear() { Year = this.Year - 1, Quater = 4};
        }

        public bool MyEquals(QYear obj)
        {
            var o = obj as QYear;
            return this.Year == o.Year && this.Quater == o.Quater;
        }

        internal static object StringFromQuater(QYear qYear)
        {
            string s = "";
            if (qYear.Quater == 1) s = "1 кв.";
            else if (qYear.Quater == 2) s = "1 полуг.";
            else if (qYear.Quater == 3) s = "9 мес.";
            else if (qYear.Quater == 4) s = "";
            return s + " " + qYear.Year.ToString();
        }
    }

    class LoadExcelException: Exception
    {
        public LoadExcelException(string p) : base(p) { }
    }
}
