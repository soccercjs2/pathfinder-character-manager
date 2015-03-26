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
            return View(equation);
        }

        [HttpPost]
        public ActionResult Create(Equation equation)
        {
            if (ModelState.IsValid)
            {
                db.Equations.Add(equation);
                db.SaveChanges();

                return RedirectToAction("Index", "Equation", new { Id = equation.CharacterId });
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
                db.Equations.Attach(equation);
                db.Entry(equation).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Equation", new { Id = equation.CharacterId });
            }
            else
            {
                return View(equation);
            }
        }
    }
}