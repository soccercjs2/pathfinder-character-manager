using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class MagicItem
    {
        public int MagicItemId { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int Charges { get; set; }
        public int CasterLevel { get; set; }
        public string Type { get; set; }
    }
}