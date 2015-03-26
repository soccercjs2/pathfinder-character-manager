using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Ability { get; set; }
        public int Ranks { get; set; }
        public bool UseUntrained { get; set; }
        public bool ArmorCheckPenalty { get; set; }
        public bool ClassSkill { get; set; }
    }
}