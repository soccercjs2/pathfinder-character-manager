using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    [Table("MagicItems")]
    public class MagicItem : Equipment
    {
        public int MagicItemId { get; set; }
        public int Charges { get; set; }
        public int CasterLevel { get; set; }
        public string Type { get; set; }
        public string Slot { get; set; }
    }
}