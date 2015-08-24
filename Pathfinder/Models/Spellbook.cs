using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Spellbook
    {
        public int SpellbookId { get; set; }
        public int CharacterId { get; set; }
        public string Type { get; set; }
        public string PrimaryStatistic { get; set; }
        public string Name { get; set; }
        public int PointsCounterId { get; set; }
    }
}