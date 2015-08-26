using Pathfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.ViewModels
{
    public class PointsKnownSpellsView
    {
        public Spellbook Spellbook { get; set; }
        public List<PointsSpellLevel> SpellLevels { get; set; }
        public Dictionary<int, List<Spell>> KnownSpells { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public PointsKnownSpellsView() { }
        public PointsKnownSpellsView(int id)
        {
            this.Spellbook = db.Spellbooks.Find(id);

            this.SpellLevels = db.PointsSpellLevels
                .Where(m => m.SpellbookId == this.Spellbook.SpellbookId)
                .OrderBy(m => m.Level)
                .ToList<PointsSpellLevel>();

            this.KnownSpells = LoadKnownSpells();
        }

        private Dictionary<int, List<Spell>> LoadKnownSpells()
        {
            Dictionary<int, List<Spell>> knownSpells = new Dictionary<int, List<Spell>>();
            
            foreach (var item in this.SpellLevels)
            {
                List<Spell> knownSpellsAtLevel = db.Spells
                    .Where(m => m.SpellLevelId == item.SpellLevelId)
                    .OrderBy(m => m.Name)
                    .ToList<Spell>();

                knownSpells.Add(item.Level, knownSpellsAtLevel);
            }

            return knownSpells;
        }
    }
}