using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TresBrujas.Models
{
    public class Spell
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ApplicationUser User {get; set;}

        public string UserId { get; set; }

        public int SpellTypeId { get; set; }

        public SpellType SpellType { get; set; }

        public virtual ICollection<SpellCaster> SpellCasters { get; set; }

        public virtual ICollection<SpellType> SpellTypes { get; set; }

        public virtual ICollection<SpellSpellCaster> SpellSpellCasters { get; set; }
    }
}
