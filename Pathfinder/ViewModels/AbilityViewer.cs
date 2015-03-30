using Pathfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.ViewModels
{
    public class AbilityViewer
    {
        public int CharacterId { get; set; }
        public int TypeId { get; set; }
        public List<AbilityType> Types { get; set; }
        public List<List<Ability>> Abilities { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public AbilityViewer() { }
        public AbilityViewer(int characterId) : this(characterId, 0) { }

        public AbilityViewer(int characterId, int typeId)
        {
            this.CharacterId = characterId;
            this.Types = LoadTypes(characterId);
            this.Abilities = LoadAbilities(characterId, typeId);
        }

        public List<AbilityType> LoadTypes(int characterId)
        {
            List<AbilityType> types = db.AbilityTypes.Where(m => m.CharacterId == characterId).ToList<AbilityType>();

            AbilityType type = new AbilityType();
            type.CharacterId = characterId;
            type.AbilityTypeId = 0;
            type.Name = "All";
            types.Insert(0, type);

            return types;
        }

        public List<List<Ability>> LoadAbilities(int characterId, int typeId)
        {
            List<List<Ability>> abilitieByType = new List<List<Ability>>();
            List<AbilityType> displayType;

            if (typeId == 0) { displayType = this.Types; displayType.RemoveAt(0); }
            else { displayType = db.AbilityTypes.Where(m => m.CharacterId == characterId && m.AbilityTypeId == typeId).ToList<AbilityType>(); }
            
            foreach (AbilityType type in displayType)
            {
                List<Ability> abilities = db.Abilities.Where(m => m.CharacterId == characterId && m.AbilityTypeId == type.AbilityTypeId).ToList<Ability>();

                if (abilities != null)
                {
                    abilitieByType.Add(abilities);
                }
            }

            return abilitieByType;
        }
    }
}