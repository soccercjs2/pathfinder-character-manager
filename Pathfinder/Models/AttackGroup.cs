using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class AttackGroup
    {
        public int AttackGroupId { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; }

        public AttackGroup() { }
        public AttackGroup(int characterId)
        {
            this.CharacterId = characterId;
        }
    }
}