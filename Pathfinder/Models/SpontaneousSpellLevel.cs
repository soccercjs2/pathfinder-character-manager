using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    [Table("SpontaneousSpellLevels")]
    public class SpontaneousSpellLevel : SpellLevel
    {
        public int SpellLevelId { get; set; }
        public int SpellsKnown { get; set; }
        public int SpellsPerDay { get; set; }
    }
}