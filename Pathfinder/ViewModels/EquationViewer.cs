using Pathfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pathfinder.ViewModels
{
    public class EquationViewer
    {
        public int CharacterId { get; set; }
        public int CategoryId { get; set; }
        public List<EquationCategory> Categories { get; set; }
        public List<Equation> Equations { get; set; }

        private PathfinderContext db = new PathfinderContext();

        public EquationViewer()
        {
            this.Categories = LoadCategories(0);
            this.Equations = new List<Equation>();
        }

        public EquationViewer(int characterId) : this(characterId, 0) { }

        public EquationViewer(int characterId, int categoryId)
        {
            this.CharacterId = characterId;
            this.CategoryId = categoryId;
            
            this.Categories = LoadCategories(characterId);
            this.Equations = LoadEquations(characterId, categoryId);
        }

        public List<EquationCategory> LoadCategories(int characterId)
        {
            List<EquationCategory> categories = db.EquationCategories
                .Where(m => m.CharacterId == characterId)
                .OrderBy(m => m.Name)
                .ToList<EquationCategory>();

            EquationCategory allCategory = new EquationCategory();
            allCategory.EquationCategoryId = 0;
            allCategory.CharacterId = characterId;
            allCategory.Name = "All";
            categories.Insert(0, allCategory);
            
            return categories;
        }

        public List<Equation> LoadEquations(int characterId, int categoryId)
        {
            List<Equation> equations;

            if (categoryId == 0)
            {
                equations = (from equation in db.Equations
                        where equation.CharacterId == characterId
                        select equation).ToList<Equation>();
            }
            else
            {
                equations = (from equation in db.Equations
                        where equation.CharacterId == characterId
                        && equation.EquationCategoryId == categoryId
                             select equation).ToList<Equation>();
            }

            return equations;
        }
    }
}