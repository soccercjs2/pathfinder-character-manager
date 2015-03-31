using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Ability
    {
        public int AbilityId { get; set; }
        public int CharacterId { get; set; }
        public int AbilityTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsConditional { get; set; }
    }
}