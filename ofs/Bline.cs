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
        [MaxLength(4), MinLength(4)]
        public string Code { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public bool IsNegative { get; set; }
        [MaxLength(200)]
        public string Calculated { get; set; }
        public int CodeSort { get; set; }
    }
}
