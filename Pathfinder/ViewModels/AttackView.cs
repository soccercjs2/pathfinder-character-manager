using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pathfinder.Extensions;

namespace Pathfinder.ViewModels
{
    public class AttackView
    {
        public string Weapon { get; set; }
        public string AttackBonus { get; set; }
        public string Damage { get; set; }
        public int CriticalMinimum { get; set; }
        public int CriticalModifier { get; set; }

        public string AttackBonusString()
        {
            string attackBonus = this.AttackBonus.Beautify();
            if (!attackBonus.Contains("d"))
            {
                if (Convert.ToInt32(attackBonus) >= 0) { return "+" + attackBonus.ToString(); }
                else { return attackBonus.ToString(); }
            }
            else { return attackBonus.ToString(); }
        }
        
        public string CriticalString()
        {
            string critical = this.CriticalMinimum.ToString();
            if (this.CriticalMinimum < 20) { critical += "-20"; }
            critical += "/x" + this.CriticalModifier;

            return critical;
        }
    }
}