using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ofs
{
    public class Client
    {
        [Key]
        [MaxLength(12), MinLength(10)]
        public string Inn { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(10)]
        public string Okved { get; set; }
    }
}
