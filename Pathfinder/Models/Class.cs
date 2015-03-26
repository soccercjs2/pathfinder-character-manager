using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int Levels { get; set; }
        public int SkillPoints { get; set; }
        public int BaseAttackBonus { get; set; }
        public int ForitudeSave { get; set; }
        public int ReflexSave { get; set; }
        public int WillSave { get; set; }
    }
}