﻿using Pathfinder.Models;
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

        public int FortitudeSave { get; set; }
        public int ReflexSave { get; set; }
        public int WillSave { get; set; }

        public int CurrentHealth { get; set; }
        public int MaximumHealth { get; set; }
        public int Experience { get; set; }
        public int MoveSpeed { get; set; }

        public List<KeyValuePair<string, int>> Skills { get; set; }

        private PathfinderContext db = new PathfinderContext();
        public Dictionary<string, int> BonusResults { get; set; }
        public Dictionary<string, int> EquationResults { get; set; }

        public CharacterView() { }
        public CharacterView(int characterId)
        {
            Character character = LoadCharacter(characterId);
            this.Classes = LoadClasses(characterId);
            this.BonusResults = new Dictionary<string, int>();
            this.EquationResults = new Dictionary<string, int>();

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

        private List<KeyValuePair<string, int>> LoadSkills(int characterId)
        {
            List<Skill> skills = db.Skills.OrderBy(s => s.Name).Where(s => s.CharacterId == characterId).ToList<Skill>();
            List<KeyValuePair<string, int>> skillViews = new List<KeyValuePair<string, int>>();

            foreach (Skill skill in skills)
            {
                if (skill.Ranks > 0 || skill.UseUntrained)
                {
                    string name = skill.Name;
                    int bonus = 0;

                    if (skill.Type != null) { name += " (" + skill.Type + ")"; }
                    string skillBonusString = "Skills.[" + name + "]";

                    bonus = skill.Ranks + LoadAbilityModifierFromTag(skill.Ability);

                    if (skill.Ranks > 0 && skill.ClassSkill) { bonus += 3; }
                    if (this.BonusResults.Keys.Contains(skillBonusString)) { bonus += this.BonusResults[skillBonusString]; }

                    skillViews.Add(new KeyValuePair<string, int>(name, bonus));
                }
            }

            return skillViews;
        }

        private int LoadAbilityModifierFromTag(string tag)
        {
            switch (tag)
            {
                case "STR": return (Strength - 10) / 2;
                case "DEX": return (Dexterity - 10) / 2;
                case "CON": return (Constitution - 10) / 2;
                case "INT": return (Intelligence - 10) / 2;
                case "WIS": return (Wisdom - 10) / 2;
                case "CHA": return (Charisma - 10) / 2;
                default: return 0;
            }
        }

        private void ApplyBaseStatBonuses(int characterId)
        {
            if (this.BonusResults.Keys.Contains("Strength")) { this.Strength += this.BonusResults["Strength"]; }
            if (this.BonusResults.Keys.Contains("Dexterity")) { this.Dexterity += this.BonusResults["Dexterity"]; }
            if (this.BonusResults.Keys.Contains("Constitution")) { this.Constitution += this.BonusResults["Constitution"]; }
            if (this.BonusResults.Keys.Contains("Intelligence")) { this.Intelligence += this.BonusResults["Intelligence"]; }
            if (this.BonusResults.Keys.Contains("Wisdom")) { this.Wisdom += this.BonusResults["Wisdom"]; }
            if (this.BonusResults.Keys.Contains("Charisma")) { this.Charisma += this.BonusResults["Charisma"]; }
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
                if (this.BonusResults.Keys.Contains(equation.Name)) { this.EquationResults[equation.Name] += this.BonusResults[equation.Name]; }

                //apply bonuses applied to the equations category if the category exists
                if (category != null)
                {
                    if (this.BonusResults.Keys.Contains(category.Name))
                    {
                        this.EquationResults[equation.Name] += this.BonusResults[category.Name];
                    }
                }
            }
        }

        private void CalculateModifiers(Dictionary<string, int> equationResults)
        {
            this.StrengthMod = equationResults["STR"];
            this.DexterityMod = equationResults["DEX"];
            this.ConstitutionMod = equationResults["CON"];
            this.IntelligenceMod = equationResults["INT"];
            this.WisdomMod = equationResults["WIS"];
            this.CharismaMod = equationResults["CHA"];
        }

        private void CalculateBaseStats(Dictionary<string, int> equationResults)
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
        }
    }
}