using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    [Table("Gears")]
    public class Gear : Equipment
    {
        public int GearId { get; set; }
    }
}