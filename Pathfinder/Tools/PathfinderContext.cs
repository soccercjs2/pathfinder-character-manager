﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class PathfinderContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassHealth> ClasseHealths { get; set; }

        public DbSet<Counter> Counters { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public DbSet<Ability> Abilities { get; set; }
        public DbSet<AbilityType> AbilityTypes { get; set; }

        public DbSet<EquationCategory> EquationCategories { get; set; }
        public DbSet<Equation> Equations { get; set; }

        public DbSet<AttackGroup> AttackGroups { get; set; }
        public DbSet<Attack> Attacks { get; set; }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Armor> Armors { get; set; }
        public DbSet<MagicItem> MagicItems { get; set; }
        public DbSet<Gear> Gears { get; set; }

        public DbSet<Spellbook> Spellbooks { get; set; }

        public DbSet<SpellLevel> SpellLevels { get; set; }
        public DbSet<PreparedSpellLevel> PreparedSpellLevels { get; set; }
        public DbSet<SpontaneousSpellLevel> SpontaneousSpellLevels { get; set; }
        public DbSet<PointsSpellLevel> PointsSpellLevels { get; set; }

        public DbSet<Spell> Spells { get; set; }
        
        //public DbSet<Temp> Temps { get; set; }
    }
}