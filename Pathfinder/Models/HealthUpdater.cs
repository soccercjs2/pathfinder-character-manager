using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class HealthUpdater
    {
        public int CharacterId { get; set; }
        public int Health { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
    }
}