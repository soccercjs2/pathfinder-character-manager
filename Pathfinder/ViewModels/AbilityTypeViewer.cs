using Pathfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.ViewModels
{
    public class AbilityTypeViewer
    {
        public int AbilityTypeId { get; set; }
        public string Name { get; set; }
        public List<Ability> Abilities { get; set; }
    }
}