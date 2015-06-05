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
            List<AttackGroup> attacks = GetAttackList();
            List<Attack> subAttacks = GetSubAttackList();
            List<Equation> equations = GetEquationList();
            List<EquationCategory> equationCategories = GetEquationCategoryList();
            List<AbilityType> abilityTypes = GetAbilityTypeList();
            List<Ability> abilities = GetAbilityList();

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

            foreach (AttackGroup attack in attacks)
            {
                context.AttackGroups.Add(attack);
            }

            foreach (Attack subAttack in subAttacks)
            {
                context.SubAttacks.Add(subAttack);
            }

            foreach (Equation equation in equations)
            {
                context.Equations.Add(equation);
            }

            foreach (EquationCategory equationCategory in equationCategories)
            {
                context.EquationCategories.Add(equationCategory);
            }

            foreach (AbilityType type in abilityTypes)
            {
                context.AbilityTypes.Add(type);
            }

            foreach (Ability ability in abilities)
            {
                context.Abilities.Add(ability);
            }

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
                new Class{ CharacterId = 1, Name = "Samurai", Levels = 1, SkillPoints = 4, BaseAttackBonus = 1, ForitudeSave = 2, ReflexSave = 0, WillSave = 0},
                new Class{ CharacterId = 1, Name = "Ranger (Woodland Skirmisher)", Levels = 2, SkillPoints = 6, BaseAttackBonus = 2, ForitudeSave = 3, ReflexSave = 3, WillSave = 0},
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
                new Skill{ CharacterId = 1, Name = "Acrobatics", Ability = "DEX", Ranks = 2, UseUntrained = true, ArmorCheckPenalty = true, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Appraise", Ability = "INT", Ranks = 0, UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Bluff", Ability = "CHA", Ranks = 0, UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Climb", Ability = "STR", Ranks = 3, UseUntrained = true, ArmorCheckPenalty = true, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Craft", Ability = "INT", Ranks = 0, UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Diplomacy", Ability = "CHA", Ranks = 0, UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Disable Device", Ability = "DEX", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = true, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Disguise", Ability = "CHA", Ranks = 0, UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Escape Artist", Ability = "DEX", Ranks = 0, UseUntrained = true, ArmorCheckPenalty = true, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Fly", Ability = "DEX", Ranks = 0, UseUntrained = true, ArmorCheckPenalty = true, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Handle Animal", Ability = "CHA", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Heal", Ability = "WIS", Ranks = 0, UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Intimidate", Ability = "CHA", Ranks = 2, UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Arcana", Ability = "INT", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Dungeoneering", Ability = "INT", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Engineering", Ability = "INT", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Geography", Ability = "INT", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "History", Ability = "INT", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Local", Ability = "INT", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Nature", Ability = "INT", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Nobility", Ability = "INT", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Planes", Ability = "INT", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Knowledge", Type = "Religion", Ability = "INT", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Linguistics", Ability = "INT", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Perception", Ability = "WIS", Ranks = 2, UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Perform", Ability = "CHA", Ranks = 0, UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Profession", Ability = "WIS", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Ride", Ability = "DEX", Ranks = 0, UseUntrained = true, ArmorCheckPenalty = true, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Sense Motive", Ability = "WIS", Ranks = 0, UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Slight of Hand", Ability = "DEX", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = true, ClassSkill = false},
                new Skill{ CharacterId = 1, Name = "Spellcraft", Ability = "INT", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Stealth", Ability = "DEX", Ranks = 4, UseUntrained = true, ArmorCheckPenalty = true, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Survival", Ability = "WIS", Ranks = 2, UseUntrained = true, ArmorCheckPenalty = false, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Swim", Ability = "STR", Ranks = 1, UseUntrained = true, ArmorCheckPenalty = true, ClassSkill = true},
                new Skill{ CharacterId = 1, Name = "Use Magic Device", Ability = "CHA", Ranks = 0, UseUntrained = false, ArmorCheckPenalty = false, ClassSkill = false}
            };

            return skills;
        }

        private List<Weapon> GetWeaponList()
        {
            List<Weapon> weapons = new List<Weapon>
            {
                new Weapon{ CharacterId = 1, Name = "Claw", EnhancementBonus = 0, Damage = "1d6", CriticalMinimum = 20, CriticalModifier = 2, Range = 0, Type = "S" }
            };

            return weapons;
        }

        private List<AttackGroup> GetAttackList()
        {
            List<AttackGroup> attacks = new List<AttackGroup>
            {
                //new Attack{ CharacterId = 1, WeaponId = 1, DamageEquationId = 25 }
            };

            return attacks;
        }

        private List<Attack> GetSubAttackList()
        {
            List<Attack> subAttacks = new List<Attack>
            {
                new Attack{ AttackGroupId = 1, AttackEquationId = 24 },
                new Attack{ AttackGroupId = 1, AttackEquationId = 24 }
            };

            return subAttacks;
        }

        private List<Equation> GetEquationList()
        {
            List<Equation> equations = new List<Equation>
            {
                new Equation{ CharacterId = 1, Name = "STR", Formula = "(Strength - 10) / 2", EquationCategoryId = 1, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "DEX", Formula = "(Dexterity - 10) / 2", EquationCategoryId = 1, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "CON", Formula = "(Constitution - 10) / 2", EquationCategoryId = 1, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "INT", Formula = "(Intelligence - 10) / 2", EquationCategoryId = 1, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "WIS", Formula = "(Wisdom - 10) / 2", EquationCategoryId = 1, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "CHA", Formula = "(Charisma - 10) / 2", EquationCategoryId = 1, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "BAB", Formula = "Classes.BaseAttackBonus", EquationCategoryId = 2, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "ARMOR", Formula = "0", EquationCategoryId = 2, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "SHIELD", Formula = "0", EquationCategoryId = 2, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "NATURAL", Formula = "0", EquationCategoryId = 2, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "DODGE", Formula = "0", EquationCategoryId = 2, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "DEFLECT", Formula = "0", EquationCategoryId = 2, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "CMB", Formula = "BAB + STR", EquationCategoryId = 3, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "CMD", Formula = "10 + BAB + STR + DEX + DODGE + DEFLECT", EquationCategoryId = 3, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "AC", Formula = "10 + ARMOR + SHIELD + NATURAL + DEX + DODGE + DEFLECT", EquationCategoryId = 4, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "TAC", Formula = "10 + DEX + DODGE + DEFLECT", EquationCategoryId = 4, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "FFAC", Formula = "10 + ARMOR + SHIELD + NATURAL + DEFLECT", EquationCategoryId = 4, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "FORT", Formula = "Classes.FortitudeSave + CON", EquationCategoryId = 5, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "REF", Formula = "Classes.ReflexSave + DEX", EquationCategoryId = 5, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "WILL", Formula = "Classes.WillSave + WIS", EquationCategoryId = 5, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "MOVESPEED", Formula = "0", EquationCategoryId = 6, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "MELEE_ATTACK", Formula = "BAB + STR", EquationCategoryId = 7, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "RANGED_ATTACK", Formula = "BAB + DEX", EquationCategoryId = 7, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "CLAW_ATTACK", Formula = "BAB + STR", EquationCategoryId = 7, ShowFormula = true },
                new Equation{ CharacterId = 1, Name = "ONE_HANDED_DAMAGE", Formula = "STR", EquationCategoryId = 8, ShowFormula = true },

                new Equation{ CharacterId = 1, Name = "Will Bonus", BonusType = "WILL", Formula = "1", AbilityId = 1, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Movespeed Bonus", BonusType = "MOVESPEED", Formula = "10", AbilityId = 2, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Strength Bonus", BonusType = "Strength", Formula = "4", AbilityId = 3, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Constitution Bonus", BonusType = "Constitution", Formula = "4", AbilityId = 3, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "WILL Bonus", BonusType = "WILL", Formula = "2", AbilityId = 3, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Armor Classes Bonus", BonusType = "Armor Classes", Formula = "-2", AbilityId = 3, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "CMB Bonus", BonusType = "CMB", Formula = "2", AbilityId = 5, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Damage Bonus", BonusType = "Damage", Formula = "Class.[Samurai].Level", AbilityId = 6, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Armor Classes Bonus", BonusType = "Armor Classes", Formula = "-2", AbilityId = 6, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Attacks Bonus", BonusType = "Attacks", Formula = "2", AbilityId = 10, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Damage Bonus", BonusType = "Damage", Formula = "2", AbilityId = 10, EquationCategoryId = 0, ShowFormula = false },

                new Equation{ CharacterId = 1, Name = "Dodge Bonus", BonusType = "DODGE", Formula = "2", AbilityId = 11, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Dodge Bonus", BonusType = "DODGE", Formula = "4", AbilityId = 12, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Natural Armor Bonus", BonusType = "NATURAL", Formula = "1", AbilityId = 13, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Natural Armor Bonus", BonusType = "NATURAL", Formula = "1", AbilityId = 14, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Saves Bonus", BonusType = "Saves", Formula = "2", AbilityId = 15, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Saves Bonus", BonusType = "Saves", Formula = "2", AbilityId = 16, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Saves Bonus", BonusType = "Saves", Formula = "1", AbilityId = 17, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Movespeed Bonus", BonusType = "MOVESPEED", Formula = "30", AbilityId = 29, EquationCategoryId = 0, ShowFormula = false },

                new Equation{ CharacterId = 1, Name = "Attacks Bonus", BonusType = "Attacks", Formula = "-2", AbilityId = 25, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Damage Bonus", BonusType = "Damage", Formula = "4", AbilityId = 25, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Claw Attack Bonus", BonusType = "CLAW_ATTACK", Formula = "1", AbilityId = 26, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Maneuvers Bonus", BonusType = "Maneuvers", Formula = "2", AbilityId = 27, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Maneuvers Bonus", BonusType = "Maneuvers", Formula = "2", AbilityId = 30, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Armor Bonus", BonusType = "ARMOR", Formula = "9", AbilityId = 31, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Claw Attack Bonus", BonusType = "CLAW_ATTACK", Formula = "1", AbilityId = 32, EquationCategoryId = 0, ShowFormula = false },

                new Equation{ CharacterId = 1, Name = "Attack Bonus", BonusType = "Attacks", Formula = "1", AbilityId = 33, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Dodge Bonus", BonusType = "DODGE", Formula = "1", AbilityId = 33, EquationCategoryId = 0, ShowFormula = false },
                new Equation{ CharacterId = 1, Name = "Reflex Bonus", BonusType = "REF", Formula = "1", AbilityId = 33, EquationCategoryId = 0, ShowFormula = false }
            };

            return equations;
        }

        public List<EquationCategory> GetEquationCategoryList()
        {
            List<EquationCategory> categories = new List<EquationCategory>
            {
                new EquationCategory{ CharacterId = 1, Name = "Ability Modifier" },
                new EquationCategory{ CharacterId = 1, Name = "Base Stats" },
                new EquationCategory{ CharacterId = 1, Name = "Maneuvers" },
                new EquationCategory{ CharacterId = 1, Name = "Armor Classes" },
                new EquationCategory{ CharacterId = 1, Name = "Saves" },
                new EquationCategory{ CharacterId = 1, Name = "Movement" },
                new EquationCategory{ CharacterId = 1, Name = "Attacks" },
                new EquationCategory{ CharacterId = 1, Name = "Damage"}
            };

            return categories;
        }

        public List<AbilityType> GetAbilityTypeList()
        {
            List<AbilityType> types = new List<AbilityType>
            {
                new AbilityType{ CharacterId = 1, Name = "Class Ability" },
                new AbilityType{ CharacterId = 1, Name = "Racial Ability" },
                new AbilityType{ CharacterId = 1, Name = "Feat" }
            };

            return types;
        }

        public List<Ability> GetAbilityList()
        {
            List<Ability> abilities = new List<Ability>
            {
                new Ability{ CharacterId = 1, AbilityTypeId = 1, Name = "Unflinching", Description = "Im strong minded.", IsConditional = true, Active = false },
                new Ability{ CharacterId = 1, AbilityTypeId = 1, Name = "Fast Movement", Description = "+10 move speed.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 1, Name = "Rage", Description = "Get angry. Get strong.", IsConditional = true, Active = false },
                new Ability{ CharacterId = 1, AbilityTypeId = 1, Name = "Uncanny Dodge", Description = "Im always ready.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 1, Name = "Armor Ripper", Description = "You tear through armor while angry.", IsConditional = true, Active = false },
                new Ability{ CharacterId = 1, AbilityTypeId = 1, Name = "Challenge", Description = "Mono e mono.", IsConditional = true, Active = false },
                new Ability{ CharacterId = 1, AbilityTypeId = 1, Name = "Resolve", Description = "Fatigue no more.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 1, Name = "Forest Ghost", Description = "The forest is my home.", IsConditional = true, Active = false },
                new Ability{ CharacterId = 1, AbilityTypeId = 1, Name = "Track", Description = "Master tracker.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 1, Name = "Favored Enemy (human)", Description = "Humans dont like me.", IsConditional = true, Active = false },

                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Greater Defensive Training", Description = "Bonus to Armor Class.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Lesser Defensive Training", Description = "Bonus to Armor Class against humans.", IsConditional = true, Active = false },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Natural Armor", Description = "Bonus to Natural Armor.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Improved Natural Armor", Description = "Bonus to Natural Armor.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Hardy", Description = "Bonus to Saves against poison, spells, and spell-like abilities.", IsConditional = true, Active = false },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Greater Lucky", Description = "Bonus to Saves.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Lesser Lucky", Description = "Bonus to Saves.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Greater Spell Resistance", Description = "Gain spell resistance.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Fast Healing", Description = "Regain hit points each round.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Damage Reduction", Description = "Ignore physical damage on each hit.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Mishapened", Description = "Armor costs double.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Scent", Description = "Track with scent.", IsConditional = false, Active = true },

                new Ability{ CharacterId = 1, AbilityTypeId = 3, Name = "Diehard", Description = "Making you die... its hard.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 3, Name = "Endurance", Description = "You dont know the meaning of tired.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 3, Name = "Power Attack", Description = "All I need is damage.", IsConditional = true, Active = false },
                new Ability{ CharacterId = 1, AbilityTypeId = 3, Name = "Weapon Focus (Claws)", Description = "My claws are lethal.", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 3, Name = "Improved Sunder", Description = "Armor, whats armor?", IsConditional = true, Active = false },
                new Ability{ CharacterId = 1, AbilityTypeId = 3, Name = "Snow Tiger Berserker", Description = "Pounce bitches.", IsConditional = false, Active = true },

                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Base Movement Speed", Description = "Base Movement Speed", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 3, Name = "Greater Sunder", Description = "Now with less armor.", IsConditional = true, Active = false },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "+2 Full Plate", Description = "Armor goes here for now...", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Masterwork Claws", Description = "Masterwork Claws", IsConditional = false, Active = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Celerity", Description = "Like Haste", IsConditional = true, Active = false }
            };

            return abilities;
        }
    }
}