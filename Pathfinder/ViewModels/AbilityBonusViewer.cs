using Pathfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.ViewModels
{
    public class AbilityBonusViewer
    {
        public Ability Ability { get; set; }
        public List<Equation> Bonuses { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public AbilityBonusViewer() { }
        public AbilityBonusViewer(int abilityId)
        {
            this.Ability = db.Abilities.Find(abilityId);
            this.Bonuses = db.Equations.Where(m => m.CharacterId == this.Ability.CharacterId && m.AbilityId == abilityId).ToList<Equation>();
        }
    }
}