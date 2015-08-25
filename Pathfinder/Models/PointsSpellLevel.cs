using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    [Table("PointsSpellLevels")]
    public class PointsSpellLevel : SpellLevel
    {
        public int PointsSpellLevelId { get; set; }
        public int SpellsKnown { get; set; }
        public int SpellCost { get; set; }
    }
}