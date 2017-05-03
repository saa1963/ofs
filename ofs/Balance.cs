﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ofs
{
    public class Balance
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
        [Key]
        [Column(Order = 4)]
        public string Part { get; set; }
        [Key]
        [Column(Order = 5)]
        public string Line { get; set; }
        public int Sm { get; set; }
    }
}
