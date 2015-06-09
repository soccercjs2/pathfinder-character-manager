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
            CreateEquationCategories();
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

        private void CreateEquationCategories()
        {
            List<EquationCategory> categories = new List<EquationCategory>
            {
                new EquationCategory{ CharacterId = this.CharacterId, Name = "Ability Modifier" },
                new EquationCategory{ CharacterId = this.CharacterId, Name = "Base Stats" },
                new EquationCategory{ CharacterId = this.CharacterId, Name = "Maneuvers" },
                new EquationCategory{ CharacterId = this.CharacterId, Name = "Armor Classes" },
                new EquationCategory{ CharacterId = this.CharacterId, Name = "Saves" },
                new EquationCategory{ CharacterId = this.CharacterId, Name = "Movement" },
                new EquationCategory{ CharacterId = this.CharacterId, Name = "Attacks" },
                new EquationCategory{ CharacterId = this.CharacterId, Name = "Damage"}
            };

            for (int i = 0; i < categories.Count; i++)
            {
                db.EquationCategories.Add(categories[i]);
                db.SaveChanges();

                if (categories[i].Name == "Ability Modifier") { CreateAbilityModEquations(categories[i].EquationCategoryId); }
                if (categories[i].Name == "Base Stats") { CreateBaseStatsEquations(categories[i].EquationCategoryId); }
                if (categories[i].Name == "Maneuvers") { CreateManeuverEquations(categories[i].EquationCategoryId); }
                if (categories[i].Name == "Armor Classes") { CreateArmorClassEquations(categories[i].EquationCategoryId); }
                if (categories[i].Name == "Saves") { CreateSaveEquations(categories[i].EquationCategoryId); }
                if (categories[i].Name == "Movement") { CreateMovementEquations(categories[i].EquationCategoryId); }
                if (categories[i].Name == "Attacks") { CreateAttackEquations(categories[i].EquationCategoryId); }
                if (categories[i].Name == "Damage") { CreateDamageEquations(categories[i].EquationCategoryId); }
            }
        }

        private void CreateAbilityModEquations(int equationCategoryId)
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = equationCategoryId; //ability modifier
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

        private void CreateBaseStatsEquations(int equationCategoryId)
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = equationCategoryId; //base stats
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

        private void CreateManeuverEquations(int equationCategoryId)
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = equationCategoryId; //maneuvers
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

        private void CreateArmorClassEquations(int equationCategoryId)
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = equationCategoryId; //armor class
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

        private void CreateSaveEquations(int equationCategoryId)
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = equationCategoryId; //save
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

        private void CreateMovementEquations(int equationCategoryId)
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = equationCategoryId; //movement
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

        private void CreateAttackEquations(int equationCategoryId)
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = equationCategoryId; //attack
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

        private void CreateDamageEquations(int equationCategoryId)
        {
            Equation equation = new Equation();
            equation.CharacterId = this.CharacterId;
            equation.EquationCategoryId = equationCategoryId; //damage
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