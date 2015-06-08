using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Attack
    {
        public int AttackId { get; set; }
        public int AttackGroupId { get; set; }
        public int WeaponId { get; set; }
        public int AttackEquationId { get; set; }
        public int DamageEquationId { get; set; }

        public Attack() { }
        public Attack(int attackGroupId)
        {
            this.AttackGroupId = attackGroupId;
        }
    }
}