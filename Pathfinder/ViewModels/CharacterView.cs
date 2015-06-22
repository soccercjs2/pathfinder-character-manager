using Pathfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.ViewModels
{
    public class CharacterView
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public List<Class> Classes { get; set; }

        public string Strength { get; set; }
        public string Dexterity { get; set; }
        public string Constitution { get; set; }
        public string Intelligence { get; set; }
        public string Wisdom { get; set; }
        public string Charisma { get; set; }

        public string StrengthMod { get; set; }
        public string DexterityMod { get; set; }
        public string ConstitutionMod { get; set; }
        public string IntelligenceMod { get; set; }
        public string WisdomMod { get; set; }
        public string CharismaMod { get; set; }

        public string BaseAttackBonus { get; set; }
        public string CombatManeuverBonus { get; set; }
        public string CombatManeuverDefense { get; set; }

        public string ArmorClass { get; set; }
        public string TouchArmorClass { get; set; }
        public string FlatFootedArmorClass { get; set; }

        public string FortitudeSave { get; set; }
        public string ReflexSave { get; set; }
        public string WillSave { get; set; }

        public int CurrentHealth { get; set; }
        public string MaximumHealth { get; set; }
        public int Experience { get; set; }
        public string MoveSpeed { get; set; }

        public string SkillPoints { get; set; }
        public List<KeyValuePair<string, string>> Skills { get; set; }

        private PathfinderContext db = new PathfinderContext();
        public Dictionary<string, string> BonusResults { get; set; }
        public Dictionary<string, string> EquationResults { get; set; }

        public CharacterView() { }
        public CharacterView(int characterId)
        {
            Character character = LoadCharacter(characterId);
            this.Classes = LoadClasses(characterId);
            this.BonusResults = new Dictionary<string, string>();
            this.EquationResults = new Dictionary<string, string>();

            EvaluateBonuses(characterId);
            ApplyBaseStatBonuses(characterId);
            EvaluateEquations(characterId);

            CalculateModifiers(this.EquationResults);
            CalculateBaseStats(this.EquationResults);

            this.Skills = LoadSkills(characterId);
        }

        private Character LoadCharacter(int characterId)
        {
            Character character = db.Characters.Find(characterId);

            this.CharacterId = character.CharacterId;
            this.Name = character.Name;
            this.Experience = character.Experience;
            this.Strength = character.Strength.ToString();
            this.Dexterity = character.Dexterity.ToString();
            this.Constitution = character.Constitution.ToString();
            this.Intelligence = character.Intelligence.ToString();
            this.Wisdom = character.Wisdom.ToString();
            this.Charisma = character.Charisma.ToString();
            this.CurrentHealth = character.CurrentHealth;

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

        private List<KeyValuePair<string, string>> LoadSkills(int characterId)
        {
            List<Skill> skills = db.Skills.OrderBy(s => s.Name).Where(s => s.CharacterId == characterId).ToList<Skill>();
            List<KeyValuePair<string, string>> skillViews = new List<KeyValuePair<string, string>>();

            foreach (Skill skill in skills)
            {
                if (skill.Ranks > 0 || skill.UseUntrained)
                {
                    string name = skill.Name;
                    string bonus = "";

                    if (skill.Type != null) { name += " (" + skill.Type + ")"; }
                    string skillBonusString = "Skills.[" + name + "]";

                    bonus = (skill.Ranks + " + " + LoadAbilityModifierFromTag(skill.Ability)).ToString();

                    if (skill.Ranks > 0 && skill.ClassSkill) { bonus += " + " + 3; }
                    if (this.BonusResults.Keys.Contains(skillBonusString)) { bonus += " + " + this.BonusResults[skillBonusString]; }

                    skillViews.Add(new KeyValuePair<string, string>(name, bonus));
                }
            }

            return skillViews;
        }

        private string LoadAbilityModifierFromTag(string tag)
        {
            switch (tag)
            {
                case "STR": return "(" + Strength + " - 10) / 2";
                case "DEX": return "(" + Dexterity + " - 10) / 2";
                case "CON": return "(" + Constitution + " - 10) / 2"; ;
                case "INT": return "(" + Intelligence + " - 10) / 2"; ;
                case "WIS": return "(" + Wisdom + " - 10) / 2";
                case "CHA": return "(" + Charisma + " - 10) / 2";
                default: return "0";
            }
        }

        private void ApplyBaseStatBonuses(int characterId)
        {
            if (this.BonusResults.Keys.Contains("Strength")) { this.Strength += " + " + this.BonusResults["Strength"]; }
            if (this.BonusResults.Keys.Contains("Dexterity")) { this.Dexterity += " + " + this.BonusResults["Dexterity"]; }
            if (this.BonusResults.Keys.Contains("Constitution")) { this.Constitution += " + " + this.BonusResults["Constitution"]; }
            if (this.BonusResults.Keys.Contains("Intelligence")) { this.Intelligence += " + " + this.BonusResults["Intelligence"]; }
            if (this.BonusResults.Keys.Contains("Wisdom")) { this.Wisdom += " + " + this.BonusResults["Wisdom"]; }
            if (this.BonusResults.Keys.Contains("Charisma")) { this.Charisma += " + " + this.BonusResults["Charisma"]; }
        }

        private void EvaluateBonuses(int characterId)
        {
            List<Equation> equations = db.Equations
                .Where(m => m.CharacterId == characterId
                    && m.BonusType != null
                    && m.AbilityId > 0)
                .ToList<Equation>();

            foreach (Equation equation in equations)
            {
                Ability ability = db.Abilities.Find(equation.AbilityId);

                if (ability.Active)
                {
                    if (this.BonusResults.Keys.Contains(equation.BonusType))
                    {
                        this.BonusResults[equation.BonusType] += equation.Evaluate(this);
                    }
                    else
                    {
                        this.BonusResults.Add(equation.BonusType, equation.Evaluate(this));
                    }
                }
            }
        }

        private void EvaluateEquations(int characterId)
        {
            //load equations
            List<Equation> equations = db.Equations
                .Where(m => m.CharacterId == characterId
                    && m.BonusType == null
                    && m.AbilityId == 0)
                .ToList<Equation>();

            //evaluate each equation and apply bonuses
            foreach (Equation equation in equations)
            {
                //load the equations category
                EquationCategory category = db.EquationCategories.Find(equation.EquationCategoryId);

                //evaluate the equation and apply equation specific bonuses
                this.EquationResults.Add(equation.Name, equation.Evaluate(this));
                if (this.BonusResults.Keys.Contains(equation.Name)) { this.EquationResults[equation.Name] += " + " + this.BonusResults[equation.Name]; }

                //apply bonuses applied to the equations category if the category exists
                if (category != null)
                {
                    if (this.BonusResults.Keys.Contains(category.Name))
                    {
                        this.EquationResults[equation.Name] += " + " + this.BonusResults[category.Name];
                    }
                }
            }
        }

        private void CalculateModifiers(Dictionary<string, string> equationResults)
        {
            this.StrengthMod = equationResults["STR"];
            this.DexterityMod = equationResults["DEX"];
            this.ConstitutionMod = equationResults["CON"];
            this.IntelligenceMod = equationResults["INT"];
            this.WisdomMod = equationResults["WIS"];
            this.CharismaMod = equationResults["CHA"];
        }

        private void CalculateBaseStats(Dictionary<string, string> equationResults)
        {
            this.MoveSpeed = equationResults["MOVESPEED"];
            this.BaseAttackBonus = equationResults["BAB"];
            this.CombatManeuverBonus = equationResults["CMB"];
            this.CombatManeuverDefense = equationResults["CMD"];
            this.ArmorClass = equationResults["AC"];
            this.TouchArmorClass = equationResults["TAC"];
            this.FlatFootedArmorClass = equationResults["FFAC"];
            this.FortitudeSave = equationResults["FORT"];
            this.ReflexSave = equationResults["REF"];
            this.WillSave = equationResults["WILL"];
            this.MaximumHealth = equationResults["MAX_HEALTH"];
            this.SkillPoints = equationResults["SKILL_POINTS"];
        }
    }
}