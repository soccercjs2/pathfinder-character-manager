﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Spell
    {
        public int SpellId { get; set; }
        public int SpellbookId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool Prepared { get; set; }
    }
}