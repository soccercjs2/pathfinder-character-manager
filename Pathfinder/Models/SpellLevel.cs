using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class SpellLevel
    {
        public int SpellLevelId { get; set; }
        public int SpellbookId { get; set; }
        public int Level { get; set; }

        public int GetPreparedSpellCount()
        {
            PathfinderContext db = new PathfinderContext();
            List<Spell> preparedSpells = db.Spells
                .Where(m => m.SpellLevelId == this.SpellLevelId && m.Prepared > 0)
                .ToList<Spell>();

            int count = 0;
            foreach (Spell spell in preparedSpells)
            {
                count += spell.Prepared;
            }

            return count;
        }
    }
}