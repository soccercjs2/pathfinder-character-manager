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
        //public List<AttackView> Attacks { get; set; }
        //public int CharacterId { get; set; }
        //public string WeaponName { get; set; }
        //public string AttackBonuses { get; set; }
        //public string Damage { get; set; }
        //public string Critical { get; set; }
        //public string Range { get; set; }
        //public string Type { get; set; }

        //public CharacterView MyCharacter { get; set; }
        //public AttackGroup Attack { get; set; }
        //public Weapon Weapon { get; set; }
        //public List<Attack> SubAttacks { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public AttackGroupView() { }
        public AttackGroupView(int id) 
        {
            //Initialize(id, new CharacterView(db.AttackGroups.Find(id).CharacterId));
        }

        public AttackGroupView(int id, CharacterView character)
        {
            //Initialize(id, character);
        }

        //private void Initialize(int id, CharacterView character)
        //{
        //    this.Attack = db.Attacks.Find(id);
        //    this.AttackId = this.Attack.AttackId;
        //    this.CharacterId = this.Attack.CharacterId;

        //    this.Weapon = db.Weapons.Find(this.Attack.WeaponId);
        //    this.SubAttacks = db.SubAttacks.Where(m => m.AttackId == id).ToList<SubAttack>();
        //    this.MyCharacter = character;

        //    this.WeaponName = this.Weapon.Name;
        //    this.AttackBonuses = LoadAttackBonuses(this.SubAttacks);
        //    this.Damage = LoadDamage(this.Weapon, this.Attack);
        //    this.Critical = LoadCritical(this.Weapon);
        //    this.Range = this.Weapon.Range + "ft";
        //    this.Type = this.Weapon.Type;
        //}

        //private string LoadAttackBonuses(List<SubAttack> attacks)
        //{
        //    string attack = "";

        //    foreach (SubAttack subAttack in attacks)
        //    {
        //        Equation equation = db.Equations.Find(subAttack.AttackEquationId);

        //        if (attack != "") { attack += "/"; }
        //        attack += "+" + this.MyCharacter.EquationResults[equation.Name];
        //    }
            
        //    return attack;
        //}

        //private string LoadDamage(Weapon weapon, Attack attack)
        //{
        //    Equation damageEquation = db.Equations.Find(attack.DamageEquationId);
        //    int damageBonus = this.MyCharacter.EquationResults[damageEquation.Name];

        //    if (damageBonus < 0) { return weapon.Damage + " - " + Math.Abs(damageBonus); }
        //    else { return weapon.Damage + " + " + damageBonus;  }
        //}

        //private string LoadCritical(Weapon weapon)
        //{
        //    string critical = "";

        //    if (weapon.CriticalMinimum < 20) { critical = weapon.CriticalMinimum + "-"; }
        //    critical += "20";
        //    critical += "/x" + weapon.CriticalModifier;
        //    return critical;
        //}
    }
}