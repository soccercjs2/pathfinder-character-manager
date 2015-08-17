﻿using Pathfinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Equation
    {
        public int EquationId { get; set; }
        public int CharacterId { get; set; }
        public int EquationCategoryId { get; set; }
        public string Name { get; set; }
        public string Formula { get; set; }
        public int AbilityId { get; set; }
        public int SpellId { get; set; }
        public bool ShowFormula { get; set; }
        public string BonusType { get; set; }
        public int EvaluationOrder { get; set; }
        public bool Editable { get; set; }
        public bool Deletable { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public string Evaluate(CharacterView character)
        {
            string replacedEquation = this.Formula;

            replacedEquation = replacedEquation.Replace("Strength", character.Strength.ToString());
            replacedEquation = replacedEquation.Replace("Dexterity", character.Dexterity.ToString());
            replacedEquation = replacedEquation.Replace("Constitution", character.Constitution.ToString());
            replacedEquation = replacedEquation.Replace("Intelligence", character.Intelligence.ToString());
            replacedEquation = replacedEquation.Replace("Wisdom", character.Wisdom.ToString());
            replacedEquation = replacedEquation.Replace("Charisma", character.Charisma.ToString());
            
            if (character.EquationResults != null)
            {
                foreach (string key in character.EquationResults.Keys)
                {
                    int startIndex = replacedEquation.IndexOf(key);

                    //if you find the equation finds a matched equation result
                    if (startIndex >= 0)
                    {
                        //the end of the sub equation
                        int endIndex = startIndex + key.Length - 1;

                        //check if the characters before and after the sub equation are symbols or whitespace
                        bool validPrefix = startIndex == 0 || Char.IsWhiteSpace(replacedEquation[startIndex - 1]) || Char.IsSymbol(replacedEquation[startIndex - 1]);
                        bool validSuffix = endIndex == replacedEquation.Length - 1 || Char.IsWhiteSpace(replacedEquation[endIndex + 1]) || Char.IsSymbol(replacedEquation[endIndex + 1]);

                        //if both of the above conditions are true, you know you found a sub equation by itself, and not just part of a word
                        if (validPrefix && validSuffix)
                        {
                            replacedEquation = replacedEquation.Substring(0, startIndex) + character.EquationResults[key].ToString() + replacedEquation.Substring(endIndex + 1);
                        }
                    }

                    //replacedEquation = replacedEquation.Replace(key, character.EquationResults[key].ToString());
                }
            }
            
            replacedEquation = EvaluateClass(replacedEquation, character);
            replacedEquation = EvaluateClasses(replacedEquation, character);

            return replacedEquation;
        }

        public string EvaluateClass(string replacedEquation, CharacterView character)
        {
            //see if evaluation is needed
            if (replacedEquation.Contains("Class."))
            {
                //get start of class name
                int start = replacedEquation.IndexOf("Class.[") + "Class.[".Length;

                //get end of class name
                int classNameEnd = replacedEquation.IndexOf("]");

                //get class name
                string className = replacedEquation.Substring(start, classNameEnd - start);
                int value = 0;

                //load class based on class name
                Class playerClass = db.Classes
                    .Where(m => m.Name == className && m.CharacterId == character.CharacterId)
                    .FirstOrDefault<Class>();

                //find end of class attribute
                int classAttributeEnd = classNameEnd + 3;
                while (classAttributeEnd < replacedEquation.Length 
                    && Char.IsLetter(replacedEquation[classAttributeEnd])) { classAttributeEnd++; }

                //get class attribute
                string classAttribute = replacedEquation.Substring(classNameEnd + 2, classAttributeEnd - (classNameEnd + 2));

                //if a valid class is found
                if (playerClass != null)
                {
                    value = ReplaceClassAttribute(classAttribute, playerClass);
                }

                //get stuff before this section started
                string prefix = "";
                if (start - "Class.[".Length > 0)
                {
                    prefix = replacedEquation.Substring(0, Math.Max(start - "Class:[".Length, 0));
                }

                //evaluate the stuff after the stuff we just evaluated
                string suffix = "";
                if (classAttributeEnd < replacedEquation.Length)
                {
                    suffix = EvaluateClass(replacedEquation.Substring(classAttributeEnd), character);
                }

                //put equation back together
                return prefix + value + suffix;
            }
            else
            {
                return replacedEquation;
            }
        }

        public string EvaluateClasses(string replacedEquation, CharacterView character)
        {
            if (replacedEquation.Contains("Classes."))
            {
                int start = replacedEquation.IndexOf("Classes.") + "Classes.".Length;
                int end = start + 1;
                while (end < replacedEquation.Length && Char.IsLetter(replacedEquation[end])) { end++; }

                string classAttribute = replacedEquation.Substring(start, end - start);
                int value = 0;
                
                foreach(Class playerClass in character.Classes)
                {
                    value += ReplaceClassAttribute(classAttribute, playerClass);
                }

                string prefix = "";
                if (start - "Classes.".Length > 0)
                {
                    prefix = replacedEquation.Substring(0, Math.Max(start - "Classes:".Length, 0));
                }

                string suffix = "";
                if (end < replacedEquation.Length)
                {
                    suffix = EvaluateClasses(replacedEquation.Substring(end), character);
                }

                return prefix + value + suffix;
            }
            else
            {
                return replacedEquation;
            }
        }

        private int ReplaceClassAttribute(string classAttribute, Class playerClass)
        {
            switch (classAttribute)
            {
                case "Level": return playerClass.Levels;
                case "BaseAttackBonus": return playerClass.BaseAttackBonus;
                case "FortitudeSave": return playerClass.ForitudeSave;
                case "ReflexSave": return playerClass.ReflexSave;
                case "WillSave": return playerClass.WillSave;
                case "SkillPoints": return (playerClass.SkillPoints * playerClass.Levels);
                case "ClassHealth": return GetClassHealth(playerClass);

                default: return 0;
            }
        }

        private int GetClassHealth(Class playerClass)
        {
            List<ClassHealth> classHealths = db.ClasseHealths
                .Where(m => m.CharacterId == playerClass.CharacterId 
                    && m.ClassId == playerClass.ClassId)
                .ToList<ClassHealth>();

            int health = 0;
            foreach (ClassHealth classHealth in classHealths)
            {
                health += classHealth.Health;
            }

            return health;
        }
    }
}