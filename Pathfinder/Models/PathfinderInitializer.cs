using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class PathfinderInitializer : DropCreateDatabaseIfModelChanges<PathfinderContext>
    {
        protected override void Seed(PathfinderContext context)
        {
            List<Character> characters = GetCharacterList();
            List<Class> classes = GetClassList();
            List<Skill> skills = GetSkillList();
            List<Weapon> weapons = GetWeaponList();
            List<Equation> equations = GetEquationList();
            List<EquationCategory> equationCategories = GetEquationCategoryList();
            //List<Tag> tags = GetTagList();

            foreach (Character character in characters)
            {
                context.Characters.Add(character);
            }

            foreach (Class playerClass in classes)
            {
                context.Classes.Add(playerClass);
            }

            foreach (Skill skill in skills)
            {
                context.Skills.Add(skill);
            }

            foreach (Weapon weapon in weapons)
            {
                context.Weapons.Add(weapon);
            }

            foreach (Equation equation in equations)
            {
                context.Equations.Add(equation);
            }

            foreach (EquationCategory equationCategory in equationCategories)
            {
                context.EquationCategories.Add(equationCategory);
            }

            //foreach (Tag tag in tags)
            //{
            //    context.Tags.Add(tag);
            //}

            context.SaveChanges();
        }

        private List<Character> GetCharacterList()
        {
            List<Character> characters = new List<Character>
            {
                new Character{Name = "Wolverine", Experience = 0, Health = 0, Strength = 22, Dexterity = 16, Constitution = 24, Intelligence = 8, Wisdom = 16, Charisma = 6},
                new Character{Name = "Balerion", Experience = 0, Health = 0, Strength = 18, Dexterity = 14, Constitution = 16, Intelligence = 8, Wisdom = 12, Charisma = 18},
                new Character{Name = "Leroy Beauregard", Experience = 0, Health = 0, Strength = 18, Dexterity = 17, Constitution = 16, Intelligence = 12, Wisdom = 11, Charisma = 10}
            };

            return characters;
        }

        private List<Class> GetClassList()
        {
            List<Class> classes = new List<Class>
            {
                new Class{ CharacterId = 1, Name = "Barbarian", Levels = 2, SkillPoints = 2, BaseAttackBonus = 2, ForitudeSave = 3, ReflexSave = 0, WillSave = 0},
                new Class{ CharacterId = 1, Name = "Fighter (Unbreakable)", Levels = 2, SkillPoints = 2, BaseAttackBonus = 2, ForitudeSave = 3, ReflexSave = 0, WillSave = 0},
                new Class{ CharacterId = 2, Name = "Sorcerer", Levels = 7, SkillPoints = 2, BaseAttackBonus = 3, ForitudeSave = 2, ReflexSave = 2, WillSave = 5},
                new Class{ CharacterId = 2, Name = "Dragon Disciple", Levels = 8, SkillPoints = 2, BaseAttackBonus = 6, ForitudeSave = 4, ReflexSave = 3, WillSave = 4},
                new Class{ CharacterId = 3, Name = "Fighter (Two Weapon)", Levels = 15, SkillPoints = 2, BaseAttackBonus = 15, ForitudeSave = 9, ReflexSave = 5, WillSave = 5}
            };
            
            return classes;
        }

        private List<Skill> GetSkillList()
        {
            List<Skill> skills = new List<Skill>
            {
                new Skill{ CharacterId = 1, Name = "Acrobatics", Ability = "DEX", UseUntrained = true, ArmorCheckPenalty = true, Ranks = 5, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Appraise", Ability = "INT", UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Bluff", Ability = "CHA", UseUntrained = true, ArmorCheckPenalty = false, Ranks = 3, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Climb", Ability = "STR", UseUntrained = true, ArmorCheckPenalty = true, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Craft", Ability = "INT", UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Diplomacy", Ability = "CHA", UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Disable Device", Ability = "DEX", UseUntrained = false, ArmorCheckPenalty = true, Ranks = 1, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Disguise", Ability = "CHA", UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Escape Artist", Ability = "DEX", UseUntrained = true, ArmorCheckPenalty = true, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Fly", Ability = "DEX", UseUntrained = true, ArmorCheckPenalty = true, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Handle Animal", Ability = "CHA", UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Heal", Ability = "WIS", UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Intimidate", Ability = "CHA", UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Arcana", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Dungeoneering", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false, Ranks = 6, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Engineering", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Geography", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "History", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Local", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false, Ranks = 2, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Nature", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Nobility", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Planes", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Religion", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Linguistics", Ability = "INT", UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Perception", Ability = "WIS", UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Perform", Ability = "CHA", UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Profession", Ability = "WIS", UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Ride", Ability = "DEX", UseUntrained = true, ArmorCheckPenalty = true, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Sense Motive", Ability = "WIS", UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Slight of Hand", Ability = "DEX", UseUntrained = false, ArmorCheckPenalty = true, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Spellcraft", Ability = "INT", UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Stealth", Ability = "DEX", UseUntrained = true, ArmorCheckPenalty = true, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Survival", Ability = "WIS", UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Swim", Ability = "STR", UseUntrained = true, ArmorCheckPenalty = true, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Use Magic Device", Ability = "CHA", UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false}
            };

            return skills;
        }

        private List<Weapon> GetWeaponList()
        {
            List<Weapon> weapons = new List<Weapon>
            {
                new Weapon{ CharacterId = 1, Name = "Claw", EnhancementBonus = 0, Damage = "1d4", CriticalMinimum = 20, CriticalMaximum = 20, CriticalModifier = 2, Range = 0, Type = "S" }
            };

            return weapons;
        }

        private List<Equation> GetEquationList()
        {
            List<Equation> equations = new List<Equation>
            {
                new Equation{ CharacterId = 1, Name = "STR", Formula = "(Strength - 10) / 2", EquationCategoryId = 1 },
                new Equation{ CharacterId = 1, Name = "DEX", Formula = "(Dexterity - 10) / 2", EquationCategoryId = 1 },
                new Equation{ CharacterId = 1, Name = "CON", Formula = "(Constitution - 10) / 2", EquationCategoryId = 1 },
                new Equation{ CharacterId = 1, Name = "INT", Formula = "(Intelligence - 10) / 2", EquationCategoryId = 1 },
                new Equation{ CharacterId = 1, Name = "WIS", Formula = "(Wisdom - 10) / 2", EquationCategoryId = 1 },
                new Equation{ CharacterId = 1, Name = "CHA", Formula = "(Charisma - 10) / 2", EquationCategoryId = 1 },
                new Equation{ CharacterId = 1, Name = "BAB", Formula = "Classes.BaseAttackBonus", EquationCategoryId = 2 },
                new Equation{ CharacterId = 1, Name = "CMB", Formula = "BAB + STR", EquationCategoryId = 2 },
                new Equation{ CharacterId = 1, Name = "CMD", Formula = "10 + BAB + STR + DEX", EquationCategoryId = 2 },
                new Equation{ CharacterId = 1, Name = "AC", Formula = "10 + ARMOR + SHIELD + NATURAL + DEX + DODGE + DEFLECT", EquationCategoryId = 2 },
                new Equation{ CharacterId = 1, Name = "TAC", Formula = "10 + DEX + DODGE + DEFLECT", EquationCategoryId = 2 },
                new Equation{ CharacterId = 1, Name = "FFAC", Formula = "10 + ARMOR + SHIELD + NATURAL + DEFLECT", EquationCategoryId = 2 },
                new Equation{ CharacterId = 1, Name = "FORT", Formula = "Classes.FortitudeSave + CON", EquationCategoryId = 2 },
                new Equation{ CharacterId = 1, Name = "REF", Formula = "Classes.ReflexSave + DEX", EquationCategoryId = 2 },
                new Equation{ CharacterId = 1, Name = "WILL", Formula = "Classes.WillSave + WIS", EquationCategoryId = 2 },
                new Equation{ CharacterId = 1, Name = "MELEE_ATTACK", Formula = "BAB + STR", EquationCategoryId = 3 },
                new Equation{ CharacterId = 1, Name = "RANGED_ATTACK", Formula = "BAB + DEX", EquationCategoryId = 3 }
            };

            return equations;
        }

        public List<EquationCategory> GetEquationCategoryList()
        {
            List<EquationCategory> categories = new List<EquationCategory>
            {
                new EquationCategory{ CharacterId = 1, Name = "Ability Modifier" },
                new EquationCategory{ CharacterId = 1, Name = "Base Stats" },
                new EquationCategory{ CharacterId = 1, Name = "Attacks" }
            };

            return categories;
        }

        //private List<Tag> GetTagList()
        //{
        //    List<Tag> tags = new List<Tag>
        //    {
        //        new Tag{ CharacterId = 1, Name = "Strength", Value = "[STRENGTH]", TagCategoryId = 1 },
        //        new Tag{ CharacterId = 1, Name = "Dexterity", Value = "[DEXTERITY]", TagCategoryId = 1 },
        //        new Tag{ CharacterId = 1, Name = "Constitution", Value = "[CONSTITUTION]", TagCategoryId = 1 },
        //        new Tag{ CharacterId = 1, Name = "Intelligence", Value = "[INTELLIGENCE]", TagCategoryId = 1 },
        //        new Tag{ CharacterId = 1, Name = "Wisdom", Value = "[WISDOM]", TagCategoryId = 1 },
        //        new Tag{ CharacterId = 1, Name = "Charisma", Value = "[CHARISMA]", TagCategoryId = 1 },
        //        new Tag{ CharacterId = 1, Name = "Strength Modifier", Value = "[STR]", TagCategoryId = 2 },
        //        new Tag{ CharacterId = 1, Name = "Dexterity Modifier", Value = "[DEX]", TagCategoryId = 2 },
        //        new Tag{ CharacterId = 1, Name = "Constitution Modifier", Value = "[CON]", TagCategoryId = 2 },
        //        new Tag{ CharacterId = 1, Name = "Intelligence Modifier", Value = "[INT]", TagCategoryId = 2 },
        //        new Tag{ CharacterId = 1, Name = "Wisdom Modifier", Value = "[WIS]", TagCategoryId = 2 },
        //        new Tag{ CharacterId = 1, Name = "Charisma Modifier", Value = "[CHA]", TagCategoryId = 2 },
        //        new Tag{ CharacterId = 1, Name = "Base Attack Bonus", Value = "[BAB]", TagCategoryId = 3 },
        //        new Tag{ CharacterId = 1, Name = "Combat Maneuver Bonus", Value = "[CMB]", TagCategoryId = 3 },
        //        new Tag{ CharacterId = 1, Name = "Combat Maneuver Defense", Value = "[CMD]", TagCategoryId = 3 },
        //        new Tag{ CharacterId = 1, Name = "Armor Class", Value = "[AC]", TagCategoryId = 4 },
        //        new Tag{ CharacterId = 1, Name = "Touch Armor Class", Value = "[TAC]", TagCategoryId = 4 },
        //        new Tag{ CharacterId = 1, Name = "Flat Footed Armor Class", Value = "[FFAC]", TagCategoryId = 4 },
        //        new Tag{ CharacterId = 1, Name = "Fortitude Save", Value = "[FORT]", TagCategoryId = 5 },
        //        new Tag{ CharacterId = 1, Name = "Reflex Save", Value = "[REF]", TagCategoryId = 5 },
        //        new Tag{ CharacterId = 1, Name = "Will Save", Value = "[WILL]", TagCategoryId = 5 },
        //        new Tag{ CharacterId = 1, Name = "Move Speed", Value = "[MOVESPEED]", TagCategoryId = 6 }
        //    };

        //    return tags;
        //}
    }
}