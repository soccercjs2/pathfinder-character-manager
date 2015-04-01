﻿using Pathfinder.Models;
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

        public Dictionary<string, int> BonusResults { get; set; }
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
            EvaluateEquations(characterId);

            this.BonusResults = new Dictionary<string, int>();
            EvaluateBonuses(characterId);

            CalculateModifiers(this.EquationResults);
            CalculateBaseStats(this.EquationResults);

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

        private void EvaluateEquations(int characterId)
        {
            List<Equation> equations = db.Equations
                .Where(m => m.CharacterId == characterId
                    && m.BonusType == null
                    && m.AbilityId == 0)
                .ToList<Equation>();

            foreach (Equation equation in equations)
            {
                this.EquationResults.Add(equation.Name, equation.Evaluate(this));
            }
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

            ApplyBonuses(characterId);
        }

        private void ApplyBonuses(int characterId)
        {
            foreach (string equationName in this.EquationResults.Keys)
            {
                Equation equation = db.Equations
                    .Where(m => m.CharacterId == characterId && m.Name == equationName)
                    .FirstOrDefault<Equation>();

                EquationCategory equationCategory = db.EquationCategories.Find(equation.EquationCategoryId);

                if (this.BonusResults.Keys.Contains(equationName))
                {
                    this.EquationResults[equationName] += this.BonusResults[equationName];
                }
                
                if (this.BonusResults.Keys.Contains(equationCategory.Name))
                {
                    this.EquationResults[equationName] += this.BonusResults[equationCategory.Name];
                }
            }
        }
    }

    public class SkillView
    {
        public string Name { get; set; }
        public int Bonus { get; set; }
    }
}