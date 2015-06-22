using Pathfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.ViewModels
{
    public class AttackGroupView
    {
        public int AttackGroupId { get; set; }
        public string Name { get; set; }
        public List<AttackView> AttackViews { get; set; }
        public CharacterView Character { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public AttackGroupView() { }
        public AttackGroupView(int id) 
        {
            AttackGroup attackGroup = db.AttackGroups.Find(id);
            this.AttackGroupId = attackGroup.AttackGroupId;
            this.Name = attackGroup.Name;
            this.Character = new CharacterView(attackGroup.CharacterId);
            this.AttackViews = LoadAttackViews(attackGroup);
        }

        private List<AttackView> LoadAttackViews(AttackGroup attackGroup)
        {
            List<Attack> attacks = db.Attacks.Where(m => m.AttackGroupId == attackGroup.AttackGroupId).ToList<Attack>();
            List<AttackView> attackViews = new List<AttackView>();

            foreach (Attack attack in attacks)
            {
                Weapon weapon = db.Weapons.Where(m => m.WeaponId == attack.WeaponId).FirstOrDefault<Weapon>();
                Equation attackEquation = db.Equations.Find(attack.AttackEquationId);
                Equation damageEquation = db.Equations.Find(attack.DamageEquationId);
                
                AttackView attackView = new AttackView();
                attackView.Weapon = weapon.Name;
                attackView.AttackBonus = GetAttackBonus(attackEquation.Name, weapon);
                attackView.Damage = GetDamage(damageEquation.Name, weapon);
                attackView.CriticalMinimum = weapon.CriticalMinimum;
                attackView.CriticalModifier = weapon.CriticalModifier;
                attackViews.Add(attackView);
            }

            return attackViews;
        }

        private string GetAttackBonus(string equationName, Weapon weapon)
        {
            string bonus = this.Character.EquationResults[equationName];
            
            if (weapon.EnhancementBonus != 0) { bonus += weapon.EnhancementBonus; }
            else if (weapon.Masterwork) { bonus += 1; }

            return bonus;
        }

        private string GetDamage(string equationName, Weapon weapon)
        {
            string damage = weapon.Damage;
            string damageBonus = this.Character.EquationResults[equationName];

            if (weapon.EnhancementBonus != 0) { damageBonus += weapon.EnhancementBonus; }

            return damage + " + " + damageBonus;
        }
    }
}