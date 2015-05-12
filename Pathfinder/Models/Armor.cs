using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    [Table("Armors")]
    public class Armor : Equipment
    {
        public int ArmorId { get; set; }
        public int EnhancementBonus { get; set; }
        public int ArmorBonus { get; set; }
        public int MaxDexterity { get; set; }
        public int ArmorCheckPenalty { get; set; }
        public int ArcaneSpellFailureChance { get; set; }
        public string Type { get; set; }
    }
}