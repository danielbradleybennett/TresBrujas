using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TresBrujas.Models
{
    public class SpellSpellCaster
    {
        public int Id { get; set; }

        public int SpellId { get; set; }

        public int SpellCasterId { get; set; }
    }
}
