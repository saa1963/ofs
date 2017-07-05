using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ofs
{
    public class Ofs
    {
        [Key]
        [Column(Order = 1)]
        public int Quater { get; set; }
        [Key]
        [Column(Order = 2)]
        public int Year { get; set; }
        [Key]
        [Column(Order = 3)]
        public string Inn { get; set; }
        /// <summary>
        /// 11 строка
        /// Коэффициент финансовой независимости
        /// =ОКРУГЛ((Баланс!D41+Баланс!D51)/Баланс!D55;2)
        /// (1300+1530)/1700
        /// </summary>
        public decimal Kfn { get; set; }
        /// <summary>
        /// 12 строка
        /// Коэффициент обеспеченности собственными оборотными средствами
        /// =ОКРУГЛ((Баланс!H41+Баланс!H51-Баланс!H22)/Баланс!H30;2)
        /// (1300+1530-1100)/1200
        /// </summary>
        public decimal Kosos { get; set; }
        /// <summary>
        /// 14 строка
        /// Коэффициент соотношения оборотных и внеоборотных активов
        /// =ОКРУГЛ(Баланс!D30/Баланс!D22;2)
        /// 1200/1100
        /// </summary>
        public decimal Ksova { get; set; }
        /// <summary>
        /// 15 строка
        /// Общий коэффициент ликвидности
        /// =ОКРУГЛ(Баланс!H30/Баланс!H54;2)
        /// 1200/1500
        /// </summary>
        public decimal Okl { get; set; }
        /// <summary>
        /// 16 строка
        /// Коэффициент покрытия
        /// =ОКРУГЛ((Баланс!D24+Баланс!D25+Баланс!D26+Баланс!D27+Баланс!D28)/(Баланс!D54-Баланс!D51);2)
        /// (1210+1220+1230+1240+1250)/(1500-1530)
        /// </summary>
        public decimal Kp { get; set; }
        /// <summary>
        /// 17 строка
        /// Коэффициент оборачиваемости активов
        /// =ОКРУГЛ(((Баланс!C31+Баланс!D31)*0,5/Форма2!D10)*90;0)
        /// ((1600пред+1600)*0,5/2110)*90
        /// </summary>
        public decimal Koa { get; set; }
        /// <summary>
        /// 19 строка
        /// Рентабельность продаж
        /// =ОКРУГЛ(Форма2!H15/Форма2!H10;2)
        /// 2200/2110
        /// </summary>
        public decimal Rp { get; set; }
        /// <summary>
        /// строка 20
        /// Рентабельность собственного капитала (чистых активов)         
        /// =ОКРУГЛ(Форма2!H21/((Баланс!G41+Баланс!G51+Баланс!H41+Баланс!H51)*0,5)*365/90;2)
        /// 2300/((1300пр+1530пр+1300+1530)*0,5)*365/90
        /// </summary>
        public decimal? Rsk { get; set; }


        /// <summary>
        /// строка 24
        /// Валюта баланса
        /// =Баланс!H31
        /// 1600
        /// </summary>
        public int Vb { get; set; }
        /// <summary>
        /// строка 25
        /// Внеоборотные активы
        /// =Баланс!H22
        /// 1100
        /// </summary>
        public int Va { get; set; }
        /// <summary>
        /// строка 26
        /// основные средства
        /// =Баланс!H17
        /// 1150
        /// </summary>
        public int Os { get; set; }
        /// <summary>
        /// строка 27
        /// Оборотные активы
        /// =Баланс!H30
        /// 1200
        /// </summary>
        public int Oa { get; set; }
        /// <summary>
        /// строка 28
        /// Запасы
        /// =Баланс!H24
        /// 1210
        /// </summary>
        public int Zap { get; set; }
        /// <summary>
        /// строка 29
        /// Дебиторская задолженность
        /// =Баланс!H26
        /// 1230
        /// </summary>
        public int Dz { get; set; }
        /// <summary>
        /// строка 30
        /// Финансовые вложения
        /// =Баланс!H27
        /// 1240
        /// </summary>
        public int Fv { get; set; }
        /// <summary>
        /// строка 31
        /// Капитал и резервы
        /// =Баланс!H41
        /// 1300
        /// </summary>
        public int Kir { get; set; }
        /// <summary>
        /// строка 32
        /// нераспределенная прибыль
        /// =Баланс!H40
        /// 1370
        /// </summary>
        public int Np { get; set; }
        /// <summary>
        /// строка 33
        /// Чистые активы
        /// =Баланс!H41+Баланс!H51
        /// 1300+1530
        /// </summary>
        public int Cha { get; set; }
        /// <summary>
        /// строка 34
        /// Выручка
        /// =Форма2!H10
        /// 2110
        /// </summary>
        public int Vir { get; set; }
        /// <summary>
        /// строка 35
        /// Себестоимость продаж
        /// =Форма2!H11
        /// 2120
        /// </summary>
        public int Sp { get; set; }
        /// <summary>
        /// строка 36
        /// Прибыль от продаж
        /// =Форма2!H15
        /// 2200
        /// </summary>
        public int Pop { get; set; }
        /// <summary>
        /// строка 37
        /// Чистая прибыль
        /// =Форма2!H27
        /// 2400
        /// </summary>
        public int Chp { get; set; }
        public IGrouping<QYear, Balance> Balance { get => balance; set => balance = value; }

        private IGrouping<QYear, Balance> balance;

        public decimal getRop()
        {
            int EconomyBranch;
            var rating = new Rating();
            using (var ctx = new OfsContext())
            {
                EconomyBranch = rating.getEconomyBranch(ctx.Clients.Find(Inn).Okved);
            }
            decimal[] koefs = {Kfn, Kosos, Ksova, Okl, Kp, Koa, Rp, Rsk.Value};
            decimal sum1 = 0;
            decimal sum2 = 0;
            for (int i = 1; i <= 8; i++)
            {
                sum1 += rating.getRate(i, EconomyBranch, koefs[i - 1]) * rating.getWeight(i);
                sum2 += rating.getWeight(i);
            }
            if (!koefs.Contains(0m))
                return Decimal.Round(sum1 / sum2, 2, MidpointRounding.AwayFromZero);
            else
                return 0;
        }
    }
}
