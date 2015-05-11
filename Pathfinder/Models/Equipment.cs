using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
    }
}