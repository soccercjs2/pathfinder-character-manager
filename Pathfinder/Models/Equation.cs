using NCalc;
using Pathfinder.ViewModels;
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
        public bool ShowFormula { get; set; }
        public string BonusType { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public int Evaluate(PlayerCharacter character)
        {
            string replacedEquation = this.Formula;

            replacedEquation = replacedEquation.Replace("Strength", character.Strength.ToString());
            replacedEquation = replacedEquation.Replace("Dexterity", character.Dexterity.ToString());
            replacedEquation = replacedEquation.Replace("Constitution", character.Constitution.ToString());
            replacedEquation = replacedEquation.Replace("Intelligence", character.Intelligence.ToString());
            replacedEquation = replacedEquation.Replace("Wisdom", character.Wisdom.ToString());
            replacedEquation = replacedEquation.Replace("Charisma", character.Charisma.ToString());

            if (character.BonusResults.Keys.Contains("ARMOR")) { replacedEquation = replacedEquation.Replace("ARMOR", character.BonusResults["ARMOR"].ToString()); }
            if (character.BonusResults.Keys.Contains("SHIELD")) { replacedEquation = replacedEquation.Replace("SHIELD", character.BonusResults["SHIELD"].ToString()); }
            if (character.BonusResults.Keys.Contains("NATURAL")) { replacedEquation = replacedEquation.Replace("NATURAL", character.BonusResults["NATURAL"].ToString()); }
            if (character.BonusResults.Keys.Contains("DODGE")) { replacedEquation = replacedEquation.Replace("DODGE", character.BonusResults["DODGE"].ToString()); }
            if (character.BonusResults.Keys.Contains("DEFLECT")) { replacedEquation = replacedEquation.Replace("DEFLECT", character.BonusResults["DEFLECT"].ToString()); }
            
            if (character.EquationResults != null)
            {
                foreach (string key in character.EquationResults.Keys)
                {
                    replacedEquation = replacedEquation.Replace(key, character.EquationResults[key].ToString());
                }
            }
            
            replacedEquation = EvaluateClasses(replacedEquation, character);

            Expression expression = new Expression(replacedEquation, EvaluateOptions.NoCache);
            return (int)Math.Floor(Convert.ToDecimal(expression.Evaluate()));
        }

        public string EvaluateClasses(string replacedEquation, PlayerCharacter character)
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
                    switch (classAttribute)
                    {
                        case "Level": value += playerClass.Levels; break;
                        case "BaseAttackBonus": value += playerClass.BaseAttackBonus; break;
                        case "FortitudeSave": value += playerClass.ForitudeSave; break;
                        case "ReflexSave": value += playerClass.ReflexSave; break;
                        case "WillSave": value += playerClass.WillSave; break;
                        default: value += 0; break;
                    }
                }

                string prefix = "";
                if (start - "Classes.".Length > 0)
                {
                    prefix = replacedEquation.Substring(0, Math.Max(start - "Classes:".Length, 0));
                }

                string suffix = "";
                if (end < replacedEquation.Length)
                {
                    suffix = replacedEquation.Substring(end + 1);
                }

                return prefix + value + suffix;
            }
            else
            {
                return replacedEquation;
            }
        }
    }
}