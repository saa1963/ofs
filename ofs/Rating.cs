using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ofs
{
    public class Rating
    {
        Dictionary<int, int> weights = new Dictionary<int, int>()
        {
            {1, 3}, {2, 2}, {3, 2}, {4, 3}, {5, 3}, {6, 3}, {7, 3}, {8, 2}
        };
        Dictionary<string, decimal[]> rates = new Dictionary<string, decimal[]>()
        {
            {"11", new decimal[] { 0.3m, 0.4m, 0.7m } },
            {"12", new decimal[] { 0.1m, 0.3m, 0.7m } },
            {"13", new decimal[] { 0.2m, 0.3m, 0.5m } },
            {"14", new decimal[] { 0.2m, 0.3m, 0.7m } },
            {"15", new decimal[] { 0.1m, 0.2m, 0.3m } },
            {"16", new decimal[] { 0.3m, 0.4m, 0.8m } },
            {"17", new decimal[] { 0.1m, 0.2m, 0.4m } },
            {"21", new decimal[] { 0.05m, 0.3m, 0.8m } },
            {"22", new decimal[] { 0.05m, 0.1m, 0.5m } },
            {"23", new decimal[] { 0.05m, 0.3m, 0.8m } },
            {"24", new decimal[] { 0.05m, 0.2m, 0.6m } },
            {"25", new decimal[] { 0.05m, 0.3m, 0.8m } },
            {"26", new decimal[] { 0.05m, 0.3m, 0.8m } },
            {"27", new decimal[] { 0.05m, 0.1m, 0.2m } },
            {"31", new decimal[] { 0.2m, 0.7m, 1.2m } },
            {"32", new decimal[] { 0.2m, 0.5m, 1.2m } },
            {"33", new decimal[] { 0.2m, 0.4m, 1.1m } },
            {"34", new decimal[] { 0.4m, 1.0m, 2.5m } },
            {"35", new decimal[] { 0.2m, 1.1m, 2.4m } },
            {"36", new decimal[] { 0.1m, 0.5m, 1.0m } },
            {"37", new decimal[] { 0.1m, 0.5m, 1.2m } },
            {"41", new decimal[] { 0.2m, 0.6m, 1.5m } },
            {"42", new decimal[] { 0.5m, 0.9m, 1.5m } },
            {"43", new decimal[] { 0.2m, 0.5m, 1.2m } },
            {"44", new decimal[] { 0.5m, 0.9m, 1.8m } },
            {"45", new decimal[] { 0.2m, 0.7m, 1.8m } },
            {"46", new decimal[] { 0.1m, 0.5m, 1.2m } },
            {"47", new decimal[] { 0.1m, 0.6m, 1.5m } },
            {"51", new decimal[] { 0.1m, 0.3m, 0.9m } },
            {"52", new decimal[] { 0.2m, 0.4m, 1.3m } },
            {"53", new decimal[] { 0.1m, 0.3m, 0.9m } },
            {"54", new decimal[] { 0.1m, 0.3m, 1.1m } },
            {"55", new decimal[] { 0.1m, 0.3m, 0.9m } },
            {"56", new decimal[] { 0.1m, 0.3m, 1.1m } },
            {"57", new decimal[] { 0.1m, 0.3m, 0.8m } },
            {"61", new decimal[] { 900m, 500m, 50m } },
            {"62", new decimal[] { 600m, 300m, 35m } },
            {"63", new decimal[] { 900m, 550m, 200m } },
            {"64", new decimal[] { 450m, 350m, 70m } },
            {"65", new decimal[] { 500m, 350m, 80m } },
            {"66", new decimal[] { 500m, 400m, 100m } },
            {"67", new decimal[] { 600m, 400m, 100m } },
            {"71", new decimal[] { 0.01m, 0.07m, 0.3m } },
            {"72", new decimal[] { 0.02m, 0.05m, 0.2m } },
            {"73", new decimal[] { 0.02m, 0.05m, 0.1m } },
            {"74", new decimal[] { 0.02m, 0.05m, 0.2m } },
            {"75", new decimal[] { 0.01m, 0.07m, 0.3m } },
            {"76", new decimal[] { 0.02m, 0.05m, 0.4m } },
            {"77", new decimal[] { 0.02m, 0.05m, 0.2m } },
            {"81", new decimal[] { 0.01m, 0.02m, 0.05m } },
            {"82", new decimal[] { 0.01m, 0.05m, 0.2m } },
            {"83", new decimal[] { 0.01m, 0.05m, 0.2m } },
            {"84", new decimal[] { 0.01m, 0.03m, 0.08m } },
            {"85", new decimal[] { 0.01m, 0.03m, 0.06m } },
            {"86", new decimal[] { 0.01m, 0.03m, 0.06m } },
            {"87", new decimal[] { 0.01m, 0.03m, 0.07m } }
        };

        public int getWeight(int VidKoeff)
        {
            return weights[VidKoeff];
        }

        public int getRate(int VidKoeff, int EconomyBranch, decimal val)
        {
            int rt;
            decimal[] ln = rates[VidKoeff.ToString() + EconomyBranch.ToString()];
            if (VidKoeff != 6)
            {
                if (val < ln[0])
                    rt = 1;
                else if (val >= ln[0] && val < ln[1])
                    rt = 2;
                else if (val >= ln[1] && val < ln[2])
                    rt = 3;
                else
                    rt = 4;
            }
            else
            {
                if (val > ln[0])
                    rt = 1;
                else if (val > ln[1] && val <= ln[0])
                    rt = 2;
                else if (val > ln[2] && val <= ln[1])
                    rt = 3;
                else
                    rt = 4;
            }
            return rt;
        }

        public int getEconomyBranch(string okved)
        {
            int rt = -1;
            int okved2;
            if (!Int32.TryParse(okved.Substring(0, 2), out okved2)) return -1;
            // Сельское хозяйство
            if (okved2 >= 1 && okved2 <= 2)
            {
                rt = 1;
            }
            // Обрабатывающие производства
            else if (okved2 >= 15 && okved2 <= 37)
            {
                rt = 2;
            }
            // Производство и распределение электроэнергии, газа и воды
            else if (okved2 >= 40 && okved2 <= 41)
            {
                rt = 3;
            }
            // Строительство
            else if (okved2 == 45)
            {
                rt = 4;
            }
            // Оптовая и розничная торговля; ремонт
            else if (okved2 >= 50 && okved2 <= 52)
            {
                rt = 5;
            }
            // Транспорт и связь
            else if (okved2 >= 60 && okved2 <= 64)
            {
                rt = 6;
            }
            // Прочее
            else
            {
                rt = 7;
            }
            return rt;
        }
    }
}
