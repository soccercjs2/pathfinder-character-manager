using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Counter
    {
        public int CounterId { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int MaxValueEquationId { get; set; }

        public string GetEquationName()
        {
            PathfinderContext db = new PathfinderContext();
            return db.Equations.Find(MaxValueEquationId).Name;
        }
    }
}