using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class SubAttack
    {
        public int SubAttackId { get; set; }
        public int AttackId { get; set; }
        public int EquationId { get; set; }

        public Equation GetEquation()
        {
            PathfinderContext db = new PathfinderContext();
            return db.Equations.Find(this.EquationId);
        }
    }
}