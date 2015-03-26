using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Attack
    {
        public int AttackId { get; set; }
        public int CharacterId { get; set; }
        public int WeaponId { get; set; }
        public int DamageEquationId { get; set; }
    }
}