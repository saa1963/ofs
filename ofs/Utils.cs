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
        public void LoadBalance(string fname)
        {
            using (var package = new ExcelPackage(new FileInfo(fname)))
            {
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
                foreach (var cl in Cols)
                {
                    row = 13;
                    col = 2;
                    wsh.Cells[row, col]
                    while (row < 56)
                    {

                    }
                }
                //for (int i = 2; i <= wsh.Dimension.End.Column;
                //        i++)
                //{
                //    for (int j = wsh.Dimension.Start.Row;
                //            j <= wsh.Dimension.End.Row;
                //            j++)
                //    {
                //        object cellValue = wsh.Cells[i, j].Value;
                //    }
                //}
            }
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
            else if (dt.Month == 2)
            {
                o.Quater = 1;
            }
            else if (dt.Month == 3)
            {
                o.Quater = 2;
            }
            else if (dt.Month == 4)
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
