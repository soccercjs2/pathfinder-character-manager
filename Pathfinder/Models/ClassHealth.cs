using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class ClassHealth
    {
        public int ClassHealthId { get; set; }
        public int CharacterId { get; set; }
        public int ClassId { get; set; }
        public int Health { get; set; }
    }
}