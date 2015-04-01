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

        public Dictionary<string, int> EquationResults { get; set; }

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

            this.EquationResults = new Dictionary<string, int>();
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
            this.StrengthMod = EvaluateAndKeepTrackOfEquation("STR");
            this.DexterityMod = EvaluateAndKeepTrackOfEquation("DEX");
            this.ConstitutionMod = EvaluateAndKeepTrackOfEquation("CON");
            this.IntelligenceMod = EvaluateAndKeepTrackOfEquation("INT");
            this.WisdomMod = EvaluateAndKeepTrackOfEquation("WIS");
            this.CharismaMod = EvaluateAndKeepTrackOfEquation("CHA");
        }

        private void CalculateBaseStats()
        {
            this.MoveSpeed = EvaluateAndKeepTrackOfEquation("MOVESPEED");
            this.BaseAttackBonus = EvaluateAndKeepTrackOfEquation("BAB");
            this.CombatManeuverBonus = EvaluateAndKeepTrackOfEquation("CMB");
            this.CombatManeuverDefense = EvaluateAndKeepTrackOfEquation("CMD");
            this.ArmorClass = EvaluateAndKeepTrackOfEquation("AC");
            this.TouchArmorClass = EvaluateAndKeepTrackOfEquation("TAC");
            this.FlatFootedArmorClass = EvaluateAndKeepTrackOfEquation("FFAC");
            this.FortitudeSave = EvaluateAndKeepTrackOfEquation("FORT");
            this.ReflexSave = EvaluateAndKeepTrackOfEquation("REF");
            this.WillSave = EvaluateAndKeepTrackOfEquation("WILL");
        }

        private int EvaluateAndKeepTrackOfEquation(string equationName)
        {
            int value = db.Equations
                .Where(m => m.Name == equationName)
                .FirstOrDefault<Equation>()
                .Evaluate(this);

            this.EquationResults.Add(equationName, value);
            return value;
        }
    }

    public class SkillView
    {
        public string Name { get; set; }
        public int Bonus { get; set; }
    }
}