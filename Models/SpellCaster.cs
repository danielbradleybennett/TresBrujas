using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TresBrujas.Models
{
    public class SpellCaster
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        
    }
}
