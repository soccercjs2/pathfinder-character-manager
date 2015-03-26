using Pathfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.ViewModels
{
    public class SkillManager
    {
        public int CharacterId { get; set; }
        public List<Skill> Skills { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public SkillManager() 
        {
            this.Skills = new List<Skill>();
        }

        public SkillManager(int characterId)
        {
            this.CharacterId = characterId;
            this.Skills = (from skill in db.Skills 
                           where skill.CharacterId == characterId
                           select skill).OrderBy(m => m.Type).OrderBy(m => m.Name).ToList<Skill>();
        }
    }
}