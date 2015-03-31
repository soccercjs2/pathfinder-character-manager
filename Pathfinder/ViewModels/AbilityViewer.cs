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
        public List<AbilityTypeViewer> Abilities { get; set; }

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

        public List<AbilityTypeViewer> LoadAbilities(int characterId, int typeId)
        {
            List<AbilityTypeViewer> abilitieByType = new List<AbilityTypeViewer>();
            List<AbilityType> displayType;

            if (typeId == 0) { displayType = db.AbilityTypes.Where(m => m.CharacterId == characterId).ToList<AbilityType>(); }
            else { displayType = db.AbilityTypes.Where(m => m.CharacterId == characterId && m.AbilityTypeId == typeId).ToList<AbilityType>(); }
            
            foreach (AbilityType type in displayType)
            {
                AbilityTypeViewer viewer = new AbilityTypeViewer();
                viewer.Name = type.Name;
                viewer.AbilityTypeId = type.AbilityTypeId;
                viewer.Abilities = db.Abilities.Where(m => m.CharacterId == characterId && m.AbilityTypeId == type.AbilityTypeId).ToList<Ability>();

                if (viewer != null)
                {
                    abilitieByType.Add(viewer);
                }
            }

            return abilitieByType;
        }
    }
}