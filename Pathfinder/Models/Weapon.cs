using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    [Table("Weapons")]
    public class Weapon : Equipment
    {
        public int WeaponId { get; set; }
        public int EnhancementBonus { get; set; }
        public string Damage { get; set; }
        public int CriticalMinimum { get; set; }
        //public int CriticalMaximum { get; set; }
        public int CriticalModifier { get; set; }
        public int Range { get; set; }
        public string Type { get; set; }
        public bool Masterwork { get; set; }
    }
}