using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public int Health { get; set; }
        public int Experience { get; set; }
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        //public ICollection<CharacterClass> CharacterClasses { get; set; }
    }
}