using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class ExperienceUpdater
    {
        public int CharacterId { get; set; }
        public int Experience { get; set; }
        public int CurrentExperience { get; set; }
    }
}