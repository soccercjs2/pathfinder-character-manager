using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class EquationCategory
    {
        public int EquationCategoryId { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; }
    }
}