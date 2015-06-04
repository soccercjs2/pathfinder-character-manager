using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public int Health { get; set; }
        public int Experience { get; set; }
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public void InitializeCharacter()
        {
            CreateEquations();
            CreateSkills();
        }

        private void CreateSkills()
        {
            List<Skill> skills = new List<Skill>
            {
                new Skill{ CharacterId = this.CharacterId, Name = "Acrobatics", Ability = "DEX", UseUntrained = true, ArmorCheckPenalty = true},
                new Skill{ CharacterId = this.CharacterId, Name = "Appraise", Ability = "INT", UseUntrained = true, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Bluff", Ability = "CHA", UseUntrained = true, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Climb", Ability = "STR", UseUntrained = true, ArmorCheckPenalty = true},
                new Skill{ CharacterId = this.CharacterId, Name = "Craft", Ability = "INT", UseUntrained = true, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Diplomacy", Ability = "CHA", UseUntrained = true, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Disable Device", Ability = "DEX", UseUntrained = false, ArmorCheckPenalty = true},
                new Skill{ CharacterId = this.CharacterId, Name = "Disguise", Ability = "CHA", UseUntrained = true, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Escape Artist", Ability = "DEX", UseUntrained = true, ArmorCheckPenalty = true},
                new Skill{ CharacterId = this.CharacterId, Name = "Fly", Ability = "DEX", UseUntrained = true, ArmorCheckPenalty = true},
                new Skill{ CharacterId = this.CharacterId, Name = "Handle Animal", Ability = "CHA", UseUntrained = false, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Heal", Ability = "WIS", UseUntrained = true, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Intimidate", Ability = "CHA", UseUntrained = true, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Knowledge", Type = "Arcana", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Knowledge", Type = "Dungeoneering", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Knowledge", Type = "Engineering", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Knowledge", Type = "Geography", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Knowledge", Type = "History", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Knowledge", Type = "Local", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Knowledge", Type = "Nature", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Knowledge", Type = "Nobility", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Knowledge", Type = "Planes", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Knowledge", Type = "Religion", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Linguistics", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Perception", Ability = "WIS", UseUntrained = true, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Perform", Ability = "CHA", UseUntrained = true, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Profession", Ability = "WIS", UseUntrained = false, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Ride", Ability = "DEX", UseUntrained = true, ArmorCheckPenalty = true},
                new Skill{ CharacterId = this.CharacterId, Name = "Sense Motive", Ability = "WIS", UseUntrained = true, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Slight of Hand", Ability = "DEX", UseUntrained = false, ArmorCheckPenalty = true},
                new Skill{ CharacterId = this.CharacterId, Name = "Spellcraft", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Stealth", Ability = "DEX", UseUntrained = true, ArmorCheckPenalty = true},
                new Skill{ CharacterId = this.CharacterId, Name = "Survival", Ability = "WIS", UseUntrained = true, ArmorCheckPenalty = false},
                new Skill{ CharacterId = this.CharacterId, Name = "Swim", Ability = "STR", UseUntrained = true, ArmorCheckPenalty = true},
                new Skill{ CharacterId = this.CharacterId, Name = "Use Magic Device", Ability = "CHA", UseUntrained = false, ArmorCheckPenalty = false}
            };

            for (int i = 0; i < skills.Count; i++)
            {
                db.Skills.Add(skills[i]);
                db.SaveChanges();
            }
        }

        private void CreateEquations()
        {
            CreateAbilityModEquations();
            CreateBaseStatsEquations();
            CreateManeuverEquations();
            CreateArmorClassEquations();
            CreateSaveEquations();
            CreateMovementEquations();
            CreateAttackEquations();
            CreateDamageEquations();
        }

        private void CreateAbilityModEquations()
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = 1; //ability modifier
            equation.ShowFormula = true;
            equation.Editable = false;
            equation.Deletable = false;

            String[] equationNames = { "STR", "DEX", "CON", "INT", "WIS", "CHA" };
            String[] equationValues = { "(Strength - 10) / 2", "(Dexterity - 10) / 2", "(Constitution - 10) / 2", 
                                                 "(Intelligence - 10) / 2", "(Wisdom - 10) / 2", "(Charisma - 10) / 2" };

            for (int i = 0; i < equationNames.Length; i++)
            {
                equation.Name = equationNames[i];
                equation.Formula = equationValues[i];
                db.Equations.Add(equation);
                db.SaveChanges();
            }
        }

        private void CreateBaseStatsEquations()
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = 2; //base stats
            equation.ShowFormula = true;
            equation.Editable = true;
            equation.Deletable = false;

            String[] equationNames = { "BAB", "ARMOR", "SHIELD", "NATURAL", "DODGE", "DEFLECT" };
            String[] equationValues = { "Classes.BaseAttackBonus", "0", "0", "0", "0", "0" };

            for (int i = 0; i < equationNames.Length; i++)
            {
                equation.Name = equationNames[i];
                equation.Formula = equationValues[i];
                db.Equations.Add(equation);
                db.SaveChanges();
            }
        }

        private void CreateManeuverEquations()
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = 3; //maneuvers
            equation.ShowFormula = true;
            equation.Editable = true;
            equation.Deletable = false;

            String[] equationNames = { "CMB", "CMD" };
            String[] equationValues = { "BAB + STR", "10 + BAB + STR + DEX + DODGE + DEFLECT" };

            for (int i = 0; i < equationNames.Length; i++)
            {
                equation.Name = equationNames[i];
                equation.Formula = equationValues[i];
                db.Equations.Add(equation);
                db.SaveChanges();
            }
        }

        private void CreateArmorClassEquations()
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = 4; //armor class
            equation.ShowFormula = true;
            equation.Editable = true;
            equation.Deletable = false;

            String[] equationNames = { "AC", "TAC", "FFAC" };
            String[] equationValues = { "10 + ARMOR + SHIELD + NATURAL + DEX + DODGE + DEFLECT", "10 + DEX + DODGE + DEFLECT", "10 + ARMOR + SHIELD + NATURAL" };

            for (int i = 0; i < equationNames.Length; i++)
            {
                equation.Name = equationNames[i];
                equation.Formula = equationValues[i];
                db.Equations.Add(equation);
                db.SaveChanges();
            }
        }

        private void CreateSaveEquations()
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = 5; //save
            equation.ShowFormula = true;
            equation.Editable = true;
            equation.Deletable = false;

            String[] equationNames = { "FORT", "REF", "WILL" };
            String[] equationValues = { "Classes.FortitudeSave + CON", "Classes.ReflexSave + DEX", "Classes.WillSave + WIS" };

            for (int i = 0; i < equationNames.Length; i++)
            {
                equation.Name = equationNames[i];
                equation.Formula = equationValues[i];
                db.Equations.Add(equation);
                db.SaveChanges();
            }
        }

        private void CreateMovementEquations()
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = 6; //movement
            equation.ShowFormula = true;
            equation.Editable = true;
            equation.Deletable = false;

            String[] equationNames = { "MOVESPEED" };
            String[] equationValues = { "30" };

            for (int i = 0; i < equationNames.Length; i++)
            {
                equation.Name = equationNames[i];
                equation.Formula = equationValues[i];
                db.Equations.Add(equation);
                db.SaveChanges();
            }
        }

        private void CreateAttackEquations()
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = 7; //attack
            equation.ShowFormula = true;
            equation.Editable = true;
            equation.Deletable = false;

            String[] equationNames = { "MELEE_ATTACK", "RANGED_ATTACK" };
            String[] equationValues = { "BAB + STR", "BAB + DEX" };

            for (int i = 0; i < equationNames.Length; i++)
            {
                equation.Name = equationNames[i];
                equation.Formula = equationValues[i];
                db.Equations.Add(equation);
                db.SaveChanges();
            }
        }

        private void CreateDamageEquations()
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = 8; //damage
            equation.ShowFormula = true;
            equation.Editable = true;
            equation.Deletable = false;

            String[] equationNames = { "PRIMARY_HAND_DAMAGE", "TWO_HANDED_DAMAGE", "OFF_HAND_DAMAGE" };
            String[] equationValues = { "STR", "STR * 1.5", "STR / 2" };

            for (int i = 0; i < equationNames.Length; i++)
            {
                equation.Name = equationNames[i];
                equation.Formula = equationValues[i];
                db.Equations.Add(equation);
                db.SaveChanges();
            }
        }
    }
}