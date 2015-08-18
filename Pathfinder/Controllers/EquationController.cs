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
        
        public ActionResult Index(int id)
        {
            //to see all equations for a character
            return View(new EquationViewer(id));
        }

        [HttpPost]
        public ActionResult Index(EquationViewer viewer)
        {
            //selected a category to filter equation results
            return View(new EquationViewer(viewer.CharacterId, viewer.CategoryId));
        }

        public ActionResult Create(int id)
        {
            //load categories for create equation dropdown
            List<EquationCategory> categories = db.EquationCategories.Where(m => m.CharacterId == id).ToList<EquationCategory>();
            ViewBag.Categories = new SelectList(categories, "EquationCategoryId", "Name");
            
            //initialize equation variables
            Equation equation = new Equation();
            equation.CharacterId = id;
            equation.AbilityId = 0; //AbilityId is 0 because the equation is not tied to an ability
            equation.ShowFormula = true; //this is a base equation, so show it

            return View(equation);
        }

        [HttpPost]
        public ActionResult Create(Equation equation)
        {
            if (ModelState.IsValid)
            {
                //add equation to db so that it can be sorted with all of the other shown equations
                equation.EvaluationOrder = db.Equations.Max(e => e.EvaluationOrder);
                db.Equations.Add(equation);
                db.SaveChanges();

                //if equations are invalid and can't be sorted, remove the equation, and go back to editing it
                if (!SortEquations(equation.CharacterId))
                {
                    //remove the equation
                    db.Equations.Remove(db.Equations.Find(equation.EquationId));
                    db.SaveChanges();

                    //go back to editing
                    return View(equation);
                }
                else
                {
                    //valid equation saved, so go back to equation index
                    return RedirectToAction("Index", "Equation", new { Id = equation.CharacterId });
                }
            }
            else
            {
                //equation was missing something, go back to editing
                return View(equation);
            }
        }

        public ActionResult CreateAbilityEquation(int id)
        {
            //find the abilty to add an equation to
            Ability ability = db.Abilities.Find(id);
            
            //initialize the equation
            Equation equation = new Equation();
            equation.CharacterId = ability.CharacterId;
            equation.EquationCategoryId = 0; //equation doesn't have a category because it's tied to an ability
            equation.AbilityId = id;
            equation.ShowFormula = false; //equation is not a base equation because it's tied to an ability, and shouldn't be shown

            return View(equation);
        }

        [HttpPost]
        public ActionResult CreateAbilityEquation(Equation equation)
        {
            if (ModelState.IsValid)
            {
                //add the equation and set the name
                equation.Name = equation.BonusType;
                equation.EvaluationOrder = db.Equations.Max(e => e.EvaluationOrder);
                db.Equations.Add(equation);
                db.SaveChanges();

                //if equations are invalid and can't be sorted, remove the equation, and go back to editing it
                if (!SortEquations(equation.CharacterId))
                {
                    //remove the equation
                    db.Equations.Remove(db.Equations.Find(equation.EquationId));
                    db.SaveChanges();

                    //go back to editing
                    return View(equation);
                }
                else
                {
                    //valid equation added, go back to ability page
                    return RedirectToAction("AbilityBonuses", "Ability", new { Id = equation.AbilityId });
                }
            }
            else
            {
                //something is missing, go back to editing
                return View(equation);
            }
        }

        public ActionResult EditAbilityEquation(int id)
        {
            //find ability equation, and go to edit page
            return View(db.Equations.Find(id));
        }

        [HttpPost]
        public ActionResult EditAbilityEquation(Equation equation)
        {
            if (ModelState.IsValid)
            {
                //keep equation to revert back to if new equation isn't valid
                Equation oldSavedEquation = db.Equations.Find(equation.EquationId);
                db.Entry(oldSavedEquation).State = EntityState.Detached;

                //add the equation and set the equation name
                equation.Name = equation.BonusType;
                db.Equations.Attach(equation);
                db.Entry(equation).State = EntityState.Modified;
                db.SaveChanges();

                //if equations are invalid and can't be sorted, reattach the old equation, and go back to editing the new equation
                if (!SortEquations(equation.CharacterId))
                {
                    //attach the old equation to overwrite changes
                    db.Equations.Attach(oldSavedEquation);
                    db.Entry(oldSavedEquation).State = EntityState.Modified;
                    db.SaveChanges();

                    //return to editing
                    return View(equation);
                } 
                else
                {
                    //valid equation added, return to ability page
                    return RedirectToAction("AbilityBonuses", "Ability", new { Id = equation.AbilityId });
                }                
            }
            else
            {
                //something is missing, go back to editing
                return View(equation);
            }
        }

        public ActionResult CreateSpellEquation(int id)
        {
            //find the abilty to add an equation to
            Spell spell = db.Spells.Find(id);

            //initialize the equation
            Equation equation = new Equation();
            equation.CharacterId = spell.SpellbookId; //CHANGE THIS
            equation.EquationCategoryId = 0; //equation doesn't have a category because it's tied to an spell
            equation.AbilityId = id;
            equation.ShowFormula = false; //equation is not a base equation because it's tied to an spell, and shouldn't be shown

            return View(equation);
        }

        [HttpPost]
        public ActionResult CreateSpellEquation(Equation equation)
        {
            if (ModelState.IsValid)
            {
                //add the equation and set the name
                equation.Name = equation.BonusType;
                equation.EvaluationOrder = db.Equations.Max(e => e.EvaluationOrder);
                db.Equations.Add(equation);
                db.SaveChanges();

                //if equations are invalid and can't be sorted, remove the equation, and go back to editing it
                if (!SortEquations(equation.CharacterId))
                {
                    //remove the equation
                    db.Equations.Remove(db.Equations.Find(equation.EquationId));
                    db.SaveChanges();

                    //go back to editing
                    return View(equation);
                }
                else
                {
                    //valid equation added, go back to ability page
                    return RedirectToAction("View", "Spell", new { Id = equation.SpellId });
                }
            }
            else
            {
                //something is missing, go back to editing
                return View(equation);
            }
        }

        public ActionResult EditSpellEquation(int id)
        {
            //find ability equation, and go to edit page
            return View(db.Equations.Find(id));
        }

        [HttpPost]
        public ActionResult EditSpellEquation(Equation equation)
        {
            if (ModelState.IsValid)
            {
                //keep equation to revert back to if new equation isn't valid
                Equation oldSavedEquation = db.Equations.Find(equation.EquationId);
                db.Entry(oldSavedEquation).State = EntityState.Detached;

                //add the equation and set the equation name
                equation.Name = equation.BonusType;
                db.Equations.Attach(equation);
                db.Entry(equation).State = EntityState.Modified;
                db.SaveChanges();

                //if equations are invalid and can't be sorted, reattach the old equation, and go back to editing the new equation
                if (!SortEquations(equation.CharacterId))
                {
                    //attach the old equation to overwrite changes
                    db.Equations.Attach(oldSavedEquation);
                    db.Entry(oldSavedEquation).State = EntityState.Modified;
                    db.SaveChanges();

                    //return to editing
                    return View(equation);
                }
                else
                {
                    //valid equation added, return to ability page
                    return RedirectToAction("View", "Spell", new { Id = equation.SpellId });
                }
            }
            else
            {
                //something is missing, go back to editing
                return View(equation);
            }
        }

        public ActionResult CreateCategory(int id)
        {
            //initialize category
            EquationCategory category = new EquationCategory();
            category.CharacterId = id;

            //go to creationg page
            return View(category);
        }

        [HttpPost]
        public ActionResult CreateCategory(EquationCategory category)
        {
            if (ModelState.IsValid)
            {
                //add equation category
                db.EquationCategories.Add(category);
                db.SaveChanges();

                //continue to equation index
                return RedirectToAction("Index", new { Id = category.CharacterId });
            }
            else
            {
                //something is missing, go back to editing
                return View(category);
            }
        }

        public ActionResult Edit(int id)
        {
            //find equation and go to edit page
            return View(db.Equations.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Equation equation)
        {
            if (ModelState.IsValid)
            {
                //keep equation to revert back to if new equation isn't valid
                Equation oldSavedEquation = db.Equations.Find(equation.EquationId);
                db.Entry(oldSavedEquation).State = EntityState.Detached;

                //update the equation
                db.Equations.Attach(equation);
                db.Entry(equation).State = EntityState.Modified;
                db.SaveChanges();

                //if equations are invalid and can't be sorted, reattach the old equation, and go back to editing the new equation
                if (!SortEquations(equation.CharacterId))
                {
                    //overwrite equation changes with the original equation
                    db.Equations.Attach(oldSavedEquation);
                    db.Entry(oldSavedEquation).State = EntityState.Modified;
                    db.SaveChanges();

                    //go back to editing
                    return View(equation);
                }
                else
                {
                    //valid equation added, go back to equation index
                    return RedirectToAction("Index", "Equation", new { Id = equation.CharacterId });
                }    
            }
            else
            {
                //something is missing, go back to editing
                return View(equation);
            }
        }

        private bool SortEquations(int characterId)
        {
            //load shown equations and their names seperately
            List<Equation> equations = db.Equations.Where(m => m.CharacterId == characterId && m.ShowFormula == true).ToList<Equation>();
            List<string> equationNames = equations.Select(e => e.Name).ToList<string>();

            //prepare to track when you find a valid equation
            List<string> validatedEquationNames = new List<string>();
            int validationAttempts = 0;

            //this is the limit of tries the sorter has to sort the equations
            int validationAttemptsMax = equations.Count * 4;
            
            //loop through equations to validate each of them
            for (int i = 0; i < equations.Count; i++ )
            {
                //continue as long as the sorter hasn't reached it's validation limit
                if (validationAttempts < validationAttemptsMax) {
                    //track when you have found an invalid equation in the formula
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

            //you've found nothing wrong, continue!
            return true;
        }
    }
}