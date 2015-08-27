using Pathfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.ViewModels
{
    public class PointsCastSpellsView
    {
        public Spellbook Spellbook { get; set; }
        public List<PointsSpellLevel> SpellLevels { get; set; }
        public Dictionary<int, List<Spell>> PreparedSpells { get; set; }
        public Counter Counter { get; set; }
        public CharacterView Character { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public PointsCastSpellsView() { }
        public PointsCastSpellsView(int id)
        {
            this.Spellbook = db.Spellbooks.Find(id);
            this.Character = new CharacterView(this.Spellbook.CharacterId);

            this.SpellLevels = db.PointsSpellLevels
                .Where(m => m.SpellbookId == this.Spellbook.SpellbookId)
                .OrderBy(m => m.Level)
                .ToList<PointsSpellLevel>();

            this.PreparedSpells = LoadPreparedSpells();
            this.Counter = LoadCounter();
        }

        private Dictionary<int, List<Spell>> LoadPreparedSpells()
        {
            Dictionary<int, List<Spell>> preparedSpells = new Dictionary<int, List<Spell>>();

            foreach (var item in this.SpellLevels)
            {
                List<Spell> preparedSpellsAtLevel = db.Spells
                    .Where(m => m.SpellLevelId == item.SpellLevelId && m.Prepared > 0)
                    .OrderBy(m => m.Name)
                    .ToList<Spell>();

                preparedSpells.Add(item.Level, preparedSpellsAtLevel);
            }

            return preparedSpells;
        }

        private Counter LoadCounter()
        {
            int counterId = this.Spellbook.PointsCounterId;
            
            if (counterId >= 0)
            {
                return db.Counters.Find(counterId);
            }
            else
            {
                return null;
            }
        }
    }
}