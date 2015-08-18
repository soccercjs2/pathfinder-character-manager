using Pathfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.ViewModels
{
    public class SpellsKnownView
    {
        public Spellbook Spellbook { get; set; }
        public List<KnownSpellsByLevel> KnownSpellsByLevel { get; set; }
        public Dictionary<int, List<Spell>> KnownSpells { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public SpellsKnownView() { }
        public SpellsKnownView(int id)
        {
            this.Spellbook = db.Spellbooks.Find(id);
            
            this.KnownSpellsByLevel = db.KnownSpellsByLevels
                .Where(m => m.SpellbookId == this.Spellbook.SpellbookId)
                .OrderBy(m => m.SpellLevel)
                .ToList<KnownSpellsByLevel>();

            this.KnownSpells = LoadKnownSpells();
        }

        private Dictionary<int, List<Spell>> LoadKnownSpells()
        {
            foreach (var item in this.KnownSpellsByLevel)
            {

            }
        }
    }
}