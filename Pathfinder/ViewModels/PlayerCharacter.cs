using Pathfinder.Models;
using Pathfinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.ViewModels
{
    public class PlayerCharacter
    {
        public Character MyCharacter { get; set; }
        public List<Class> Classes { get; set; }
        public List<AttackView> Attacks { get; set; }
        public List<SkillView> Skills { get; set; }
        public AbilityViewer AbilityViewer { get; set; }

        public int Level { get; set; }
        public int Experience { get; set; }
        public int MoveSpeed { get; set; }
        public string Name { get; set; }
        
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        public int StrengthMod { get; set; }
        public int DexterityMod { get; set; }
        public int ConstitutionMod { get; set; }
        public int IntelligenceMod { get; set; }
        public int WisdomMod { get; set; }
        public int CharismaMod { get; set; }

        public int BaseAttackBonus { get; set; }
        public int CombatManeuverBonus { get; set; }
        public int CombatManeuverDefense { get; set; }

        public int ArmorClass { get; set; }
        public int TouchArmorClass { get; set; }
        public int FlatFootedArmorClass { get; set; }

        public int ArmorBonus { get; set; }
        public int ShieldBonus { get; set; }
        public int NaturalArmorBonus { get; set; }
        public int DeflectionBonus { get; set; }
        public int DodgeBonus { get; set; }

        public int FortitudeSave { get; set; }
        public int ReflexSave { get; set; }
        public int WillSave { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public PlayerCharacter() { }
        public PlayerCharacter(int characterId)
        {
            this.MyCharacter = LoadCharacter(characterId);
            this.Classes = LoadClasses(characterId);
            this.AbilityViewer = new AbilityViewer(characterId);
            
            CalculateModifiers();
            CalculateBaseStats();

            this.Skills = LoadSkills(characterId);
            this.Attacks = LoadAttacks(characterId);
        }

        private Character LoadCharacter(int characterId)
        {
            Character character = db.Characters.Find(characterId);

            this.Name = character.Name;
            this.Experience = character.Experience;
            this.Strength = character.Strength;
            this.Dexterity = character.Dexterity;
            this.Constitution = character.Constitution;
            this.Intelligence = character.Intelligence;
            this.Wisdom = character.Wisdom;
            this.Charisma = character.Charisma;

            return character;
        }

        private List<Class> LoadClasses(int characterId)
        {
            List<Class> classes = (from playerClass in db.Classes
                                   where playerClass.CharacterId == characterId
                                   select playerClass).ToList<Class>();

            foreach (Class playerClass in classes)
            {
                this.Level += playerClass.Levels;
            }

            return classes;
        }

        private List<SkillView> LoadSkills(int characterId)
        {
            List<Skill> skills = db.Skills.OrderBy(s => s.Name).Where(s => s.CharacterId == characterId).ToList<Skill>();
            List<SkillView> skillViews = new List<SkillView>();

            foreach (Skill skill in skills)
            {
                if (skill.Ranks > 0 || skill.UseUntrained)
                {
                    SkillView skillView = new SkillView();
                    skillView.Name = skill.Name;
                    if (skill.Type != null) { skillView.Name += " (" + skill.Type + ")"; }
                    skillView.Bonus = skill.Ranks + LoadAbilityModifierFromTag(skill.Ability);
                    if (skill.Ranks > 0 && skill.ClassSkill) { skillView.Bonus += 3; }
                    skillViews.Add(skillView);
                }
            }
            
            return skillViews;
        }

        private int LoadAbilityModifierFromTag(string tag)
        {
            switch (tag)
            {
                case "STR": return this.StrengthMod;
                case "DEX": return this.DexterityMod;
                case "CON": return this.ConstitutionMod;
                case "INT": return this.IntelligenceMod;
                case "WIS": return this.WisdomMod;
                case "CHA": return this.CharismaMod;
                default: return 0;
            }
        }

        private List<AttackView> LoadAttacks(int characterId)
        {
            List<Attack> attacks = db.Attacks.Where(m => m.CharacterId == characterId).ToList<Attack>();
            List<AttackView> attackViews = new List<AttackView>();

            foreach (Attack attack in attacks)
            {
                attackViews.Add(new AttackView(attack.AttackId, this));
            }

            return attackViews;
        }

        private void CalculateModifiers()
        {
            this.StrengthMod = db.Equations.Where(m => m.Name == "STR").FirstOrDefault<Equation>().Evaluate(this);
            this.DexterityMod = db.Equations.Where(m => m.Name == "DEX").FirstOrDefault<Equation>().Evaluate(this);
            this.ConstitutionMod = db.Equations.Where(m => m.Name == "CON").FirstOrDefault<Equation>().Evaluate(this);
            this.IntelligenceMod = db.Equations.Where(m => m.Name == "INT").FirstOrDefault<Equation>().Evaluate(this);
            this.WisdomMod = db.Equations.Where(m => m.Name == "WIS").FirstOrDefault<Equation>().Evaluate(this);
            this.CharismaMod = db.Equations.Where(m => m.Name == "CHA").FirstOrDefault<Equation>().Evaluate(this);
        }

        private void CalculateBaseStats()
        {
            this.MoveSpeed = 30;
            this.BaseAttackBonus = db.Equations.Where(m => m.Name == "BAB").FirstOrDefault<Equation>().Evaluate(this);
            this.CombatManeuverBonus = db.Equations.Where(m => m.Name == "CMB").FirstOrDefault<Equation>().Evaluate(this);
            this.CombatManeuverDefense = db.Equations.Where(m => m.Name == "CMD").FirstOrDefault<Equation>().Evaluate(this);
            this.ArmorClass = db.Equations.Where(m => m.Name == "AC").FirstOrDefault<Equation>().Evaluate(this);
            this.TouchArmorClass = db.Equations.Where(m => m.Name == "TAC").FirstOrDefault<Equation>().Evaluate(this);
            this.FlatFootedArmorClass = db.Equations.Where(m => m.Name == "FFAC").FirstOrDefault<Equation>().Evaluate(this);
            this.FortitudeSave = db.Equations.Where(m => m.Name == "FORT").FirstOrDefault<Equation>().Evaluate(this);
            this.ReflexSave = db.Equations.Where(m => m.Name == "REF").FirstOrDefault<Equation>().Evaluate(this);
            this.WillSave = db.Equations.Where(m => m.Name == "WILL").FirstOrDefault<Equation>().Evaluate(this);
        }
    }

    public class SkillView
    {
        public string Name { get; set; }
        public int Bonus { get; set; }
    }
}