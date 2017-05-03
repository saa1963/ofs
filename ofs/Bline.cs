using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ofs
{
    public class Bline
    {
        [Key]
        [Column(Order=1)]
        [MaxLength(1)]
        public string Part { get; set; }
        [Key]
        [Column(Order = 2)]
        [MaxLength(2)]
        public string Line { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
