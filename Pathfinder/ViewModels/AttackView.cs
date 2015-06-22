using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            if (Convert.ToInt32(this.AttackBonus) >= 0) { return '+' + this.AttackBonus.ToString(); }
            else { return this.AttackBonus.ToString(); }
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