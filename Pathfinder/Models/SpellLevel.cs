﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class SpellLevel
    {
        public int SpellLevelId { get; set; }
        public int SpellbookId { get; set; }
        public int Level { get; set; }
    }
}