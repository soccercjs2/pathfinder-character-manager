using Pathfinder.Models;
using Pathfinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pathfinder.Controllers
{
    public class EquationController : Controller
    {
        private PathfinderContext db = new PathfinderContext();
        
        // GET: Equation
        public ActionResult Index(int id)
        {
            return View(new EquationViewer(id));
        }

        [HttpPost]
        public ActionResult Index(EquationViewer viewer)
        {
            return View(new EquationViewer(viewer.CharacterId, viewer.CategoryId));
        }

        public ActionResult Create(int id)
        {
            List<EquationCategory> categories = db.EquationCategories.Where(m => m.CharacterId == id).ToList<EquationCategory>();
            ViewBag.Categories = new SelectList(categories, "EquationCategoryId", "Name");
            
            Equation equation = new Equation();
            equation.CharacterId = id;
            equation.AbilityId = 0;
            equation.ShowFormula = true;

            return View(equation);
        }

        [HttpPost]
        public ActionResult Create(Equation equation)
        {
            if (ModelState.IsValid)
            {
                equation.EvaluationOrder = db.Equations.Max(e => e.EvaluationOrder);
                db.Equations.Add(equation);
                db.SaveChanges();

                //if equations are invalid and can't be sorted, remove the equation, and go back to editing it
                if (!SortEquations(equation.CharacterId))
                {
                    db.Equations.Remove(db.Equations.Find(equation.EquationId));
                    db.SaveChanges();

                    return View(equation);
                }
                else
                {
                    return RedirectToAction("Index", "Equation", new { Id = equation.CharacterId });
                }
            }
            else
            {
                return View(equation);
            }
        }

        public ActionResult CreateAbilityEquation(int id)
        {
            Ability ability = db.Abilities.Find(id);
            
            Equation equation = new Equation();
            equation.CharacterId = ability.CharacterId;
            equation.EquationCategoryId = 0;
            equation.AbilityId = id;
            equation.ShowFormula = false;

            return View(equation);
        }

        [HttpPost]
        public ActionResult CreateAbilityEquation(Equation equation)
        {
            if (ModelState.IsValid)
            {
                equation.Name = equation.BonusType;
                equation.EvaluationOrder = db.Equations.Max(e => e.EvaluationOrder);
                db.Equations.Add(equation);
                db.SaveChanges();

                //if equations are invalid and can't be sorted, remove the equation, and go back to editing it
                if (!SortEquations(equation.CharacterId))
                {
                    db.Equations.Remove(db.Equations.Find(equation.EquationId));
                    db.SaveChanges();

                    return View(equation);
                }
                else
                {
                    return RedirectToAction("AbilityBonuses", "Ability", new { Id = equation.AbilityId });
                }
            }
            else
            {
                return View(equation);
            }
        }

        public ActionResult EditAbilityEquation(int id)
        {
            return View(db.Equations.Find(id));
        }

        [HttpPost]
        public ActionResult EditAbilityEquation(Equation equation)
        {
            if (ModelState.IsValid)
            {
                //keep equation to revert back to if new equation isn't valid
                Equation oldSavedEquation = db.Equations.Find(equation.EquationId);

                equation.Name = equation.BonusType;
                db.Equations.Attach(equation);
                db.Entry(equation).State = EntityState.Modified;
                db.SaveChanges();

                //if equations are invalid and can't be sorted, reattach the old equation, and go back to editing the new equation
                if (!SortEquations(equation.CharacterId))
                {
                    db.Equations.Attach(oldSavedEquation);
                    db.Entry(oldSavedEquation).State = EntityState.Modified;
                    db.SaveChanges();

                    return View(equation);
                } 
                else
                {
                    return RedirectToAction("AbilityBonuses", "Ability", new { Id = equation.AbilityId });
                }                
            }
            else
            {
                return View(equation);
            }
        }

        public ActionResult CreateCategory(int id)
        {
            EquationCategory category = new EquationCategory();
            category.CharacterId = id;
            return View(category);
        }

        [HttpPost]
        public ActionResult CreateCategory(EquationCategory category)
        {
            if (ModelState.IsValid)
            {
                db.EquationCategories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = category.CharacterId });
            }
            else
            {
                return View(category);
            }
        }

        public ActionResult Edit(int id)
        {
            return View(db.Equations.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Equation equation)
        {
            if (ModelState.IsValid)
            {
                //keep equation to revert back to if new equation isn't valid
                Equation oldSavedEquation = db.Equations.Find(equation.EquationId);

                db.Equations.Attach(equation);
                db.Entry(equation).State = EntityState.Modified;
                db.SaveChanges();

                //if equations are invalid and can't be sorted, reattach the old equation, and go back to editing the new equation
                if (!SortEquations(equation.CharacterId))
                {
                    db.Equations.Attach(oldSavedEquation);
                    db.Entry(oldSavedEquation).State = EntityState.Modified;
                    db.SaveChanges();

                    return View(equation);
                }
                else
                {
                    return RedirectToAction("AbilityBonuses", "Ability", new { Id = equation.AbilityId });
                }    
            }
            else
            {
                return View(equation);
            }
        }

        private bool SortEquations(int characterId)
        {
            List<Equation> equations = db.Equations.Where(m => m.CharacterId == characterId && m.ShowFormula == true).ToList<Equation>();
            List<string> equationNames = equations.Select(e => e.Name).ToList<string>();
            List<string> validatedEquationNames = new List<string>();
            int validationAttempts = 0;
            int validationAttemptsMax = equations.Count * 4;
            
            for (int i = 0; i < equations.Count; i++ )
            {
                if (validationAttempts < validationAttemptsMax) {
                    bool hasInvalidEquations = false;
                    validationAttempts++;

                    foreach (string name in equationNames)
                    {
                        string formula = equations[i].Formula;
                        int startIndex = formula.IndexOf(name);

                        //if you find the equation finds a matched equation result
                        if (startIndex >= 0)
                        {
                            //the end of the sub equation
                            int endIndex = startIndex + name.Length - 1;

                            //check if the characters before and after the sub equation are symbols or whitespace
                            bool validPrefix = startIndex == 0 || Char.IsWhiteSpace(formula[startIndex - 1]) || Char.IsSymbol(formula[startIndex - 1]);
                            bool validSuffix = endIndex == formula.Length - 1 || Char.IsWhiteSpace(formula[endIndex + 1]) || Char.IsSymbol(formula[endIndex + 1]);

                            //if both of the above conditions are true, you know you found a sub equation by itself, and not just part of a word
                            if (validPrefix && validSuffix && !validatedEquationNames.Contains(name))
                            {
                                hasInvalidEquations = true;
                                break;
                            }
                        }
                    }

                    if (hasInvalidEquations)
                    {
                        //update the evaluation order
                        int nextOrderIndex = equations.Max(e => e.EvaluationOrder) + 1;
                        equations[i].EvaluationOrder = nextOrderIndex;
                        equations.Add(equations[i]);
                        equations.RemoveAt(i);
                        i--;

                        //save the equation
                        db.Equations.Attach(equations[i]);
                        db.Entry(equations[i]).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        //mark the equation for 
                        validatedEquationNames.Add(equations[i].Name);
                    }
                }
                else
                {
                    //if you have reached here, you have probably found some circular logic, so tell the person to fix their equations
                    return false;
                }
            }

            return true;
        }
    }
}