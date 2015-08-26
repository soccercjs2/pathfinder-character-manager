﻿using Pathfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.ViewModels
{
    public class SpontaneousCastSpellsView
    {
        public Spellbook Spellbook { get; set; }
        public List<SpontaneousSpellLevel> SpellLevels { get; set; }
        public Dictionary<int, List<Spell>> PreparedSpells { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public SpontaneousCastSpellsView() { }
        public SpontaneousCastSpellsView(int id)
        {
            this.Spellbook = db.Spellbooks.Find(id);

            this.SpellLevels = db.SpontaneousSpellLevels
                .Where(m => m.SpellbookId == this.Spellbook.SpellbookId)
                .OrderBy(m => m.Level)
                .ToList<SpontaneousSpellLevel>();

            this.PreparedSpells = LoadPreparedSpells();
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
    }
}