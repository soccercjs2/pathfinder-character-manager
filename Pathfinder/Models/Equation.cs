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

        public int Evaluate(PlayerCharacter character)
        {
            string replacedEquation = this.Formula;

            replacedEquation = replacedEquation.Replace("Strength", character.Strength.ToString());
            replacedEquation = replacedEquation.Replace("Dexterity", character.Dexterity.ToString());
            replacedEquation = replacedEquation.Replace("Constitution", character.Constitution.ToString());
            replacedEquation = replacedEquation.Replace("Intelligence", character.Intelligence.ToString());
            replacedEquation = replacedEquation.Replace("Wisdom", character.Wisdom.ToString());
            replacedEquation = replacedEquation.Replace("Charisma", character.Charisma.ToString());
            
            replacedEquation = replacedEquation.Replace("STR", character.StrengthMod.ToString());
            replacedEquation = replacedEquation.Replace("DEX", character.DexterityMod.ToString());
            replacedEquation = replacedEquation.Replace("CON", character.ConstitutionMod.ToString());
            replacedEquation = replacedEquation.Replace("INT", character.IntelligenceMod.ToString());
            replacedEquation = replacedEquation.Replace("WIS", character.WisdomMod.ToString());
            replacedEquation = replacedEquation.Replace("CHA", character.CharismaMod.ToString());

            replacedEquation = replacedEquation.Replace("BAB", character.BaseAttackBonus.ToString());

            replacedEquation = replacedEquation.Replace("ARMOR", character.ArmorBonus.ToString());
            replacedEquation = replacedEquation.Replace("SHIELD", character.ShieldBonus.ToString());
            replacedEquation = replacedEquation.Replace("NATURAL", character.NaturalArmorBonus.ToString());
            replacedEquation = replacedEquation.Replace("DEFLECT", character.DeflectionBonus.ToString());
            replacedEquation = replacedEquation.Replace("DODGE", character.DodgeBonus.ToString());

            replacedEquation = EvaluateClasses(replacedEquation, character);

            Expression expression = new Expression(replacedEquation);
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