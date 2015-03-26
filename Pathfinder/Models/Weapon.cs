using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Weapon
    {
        public int WeaponId { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int EnhancementBonus { get; set; }
        public string Damage { get; set; }
        public int CriticalMinimum { get; set; }
        public int CriticalMaximum { get; set; }
        public int CriticalModifier { get; set; }
        public int Range { get; set; }
        public string Type { get; set; }
    }
}