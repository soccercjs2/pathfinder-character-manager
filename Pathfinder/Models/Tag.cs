using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public int CharacterId { get; set; }
        public int TagCategoryId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}