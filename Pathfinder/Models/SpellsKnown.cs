using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class KnownSpellsByLevel
    {
        public int SpellsKnownId { get; set; }
        public int SpellbookId { get; set; }
        public int SpellLevel { get; set; }
        public int Count { get; set; }
    }
}