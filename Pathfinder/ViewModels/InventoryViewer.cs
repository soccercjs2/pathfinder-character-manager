using Pathfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.ViewModels
{
    public class InventoryViewer
    {
        public int CharacterId { get; set; }
        public List<Weapon> Weapons { get; set; }
        public List<Armor> Armors { get; set; }
        public List<MagicItem> Wands { get; set; }
        public List<MagicItem> Scrolls { get; set; }
        public List<MagicItem> Rods { get; set; }
        public List<MagicItem> Potions { get; set; }
        public List<MagicItem> EquippedWondrousItems { get; set; }
        public List<MagicItem> UnequippedWondrousItems { get; set; }
        public List<Gear> Gears { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public InventoryViewer() { }
        public InventoryViewer(int characterId)
        {
            this.CharacterId = characterId;

            this.Weapons = LoadWeapons(characterId);
            this.Armors = LoadArmors(characterId);
            this.Wands = LoadMagicItems(characterId, "wand");
            this.Scrolls = LoadMagicItems(characterId, "scroll");
            this.Rods = LoadMagicItems(characterId, "rod");
            this.Potions = LoadMagicItems(characterId, "potion");
            this.EquippedWondrousItems = LoadMagicItems(characterId, "wondrous").Where(m => m.Equipped == true ).ToList<MagicItem>();
            this.UnequippedWondrousItems = LoadMagicItems(characterId, "wondrous").Where(m => m.Equipped == false).ToList<MagicItem>();
            this.Gears = LoadGears(characterId);
        }

        public List<Weapon> LoadWeapons(int characterId)
        {
            return db.Weapons.Where(m => m.CharacterId == characterId).ToList<Weapon>();
        }

        public List<Armor> LoadArmors(int characterId)
        {
            return db.Armors.Where(m => m.CharacterId == characterId).ToList<Armor>();
        }

        public List<MagicItem> LoadMagicItems(int characterId, string type)
        {
            return db.MagicItems
                .Where(m => m.CharacterId == characterId && m.Type == type)
                .ToList<MagicItem>();
        }

        public List<Gear> LoadGears(int characterId)
        {
            return db.Gears
                .Where(m => m.CharacterId == characterId)
                .ToList<Gear>();
        }
    }
}