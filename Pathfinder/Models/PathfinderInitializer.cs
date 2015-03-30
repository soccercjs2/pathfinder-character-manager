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
            List<Attack> attacks = GetAttackList();
            List<SubAttack> subAttacks = GetSubAttackList();
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

            foreach (Attack attack in attacks)
            {
                context.Attacks.Add(attack);
            }

            foreach (SubAttack subAttack in subAttacks)
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

        private List<Attack> GetAttackList()
        {
            List<Attack> attacks = new List<Attack>
            {
                new Attack{ CharacterId = 1, WeaponId = 1, DamageEquationId = 18 }
            };

            return attacks;
        }

        private List<SubAttack> GetSubAttackList()
        {
            List<SubAttack> subAttacks = new List<SubAttack>
            {
                new SubAttack{ AttackId = 1, AttackEquationId = 16 },
                new SubAttack{ AttackId = 1, AttackEquationId = 16 }
            };

            return subAttacks;
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
                new Equation{ CharacterId = 1, Name = "RANGED_ATTACK", Formula = "BAB + DEX", EquationCategoryId = 3 },
                new Equation{ CharacterId = 1, Name = "One-Handed Damage", Formula = "STR", EquationCategoryId = 4 }
            };

            return equations;
        }

        public List<EquationCategory> GetEquationCategoryList()
        {
            List<EquationCategory> categories = new List<EquationCategory>
            {
                new EquationCategory{ CharacterId = 1, Name = "Ability Modifier" },
                new EquationCategory{ CharacterId = 1, Name = "Base Stats" },
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
                new Ability{ CharacterId = 1, AbilityTypeId = 1, Name = "Fast Movement", Description = "+10 move speed.", IsConditional = false },
                new Ability{ CharacterId = 1, AbilityTypeId = 1, Name = "Rage", Description = "Get angry. Get strong.", IsConditional = true },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Skilled", Description = "Gain bonus skill points.", IsConditional = false },
                new Ability{ CharacterId = 1, AbilityTypeId = 2, Name = "Stealthy", Description = "+2 to stealth.", IsConditional = false },
                new Ability{ CharacterId = 1, AbilityTypeId = 3, Name = "Diehard", Description = "Making you die... it's hard", IsConditional = false },
                new Ability{ CharacterId = 1, AbilityTypeId = 3, Name = "Endurance", Description = "You don't know the meaning of tired.", IsConditional = false }
            };

            return abilities;
        }
    }
}