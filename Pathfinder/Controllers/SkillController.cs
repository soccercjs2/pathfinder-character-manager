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
    public class SkillController : Controller
    {
        private PathfinderContext db = new PathfinderContext();
        
        // GET: Skill
        public ActionResult Index(int id)
        {
            return View(new SkillManager(id));
        }

        [HttpPost]
        public ActionResult Index(SkillManager manager)
        {
            if (ModelState.IsValid)
            {
                foreach (Skill skill in manager.Skills)
                {
                    db.Skills.Attach(skill);
                    db.Entry(skill).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("ViewCharacter", "Character", new { Id = manager.CharacterId });
            }
            else
            {
                return View(manager);
            }
        }

        public ActionResult Create(int id)
        {
            EquationCategory abilityCategory = db.EquationCategories
                .Where(m => m.Name == "Ability Modifier" && m.CharacterId == id).FirstOrDefault<EquationCategory>();
            ViewBag.Abilities = db.Equations.Where(m => m.EquationCategoryId == abilityCategory.EquationCategoryId);
            
            Skill skill = new Skill();
            skill.CharacterId = id;
            return View(skill);
        }

        [HttpPost]
        public ActionResult Create(Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Skills.Add(skill);
                db.SaveChanges();

                return RedirectToAction("Index", "Skill", new { Id = skill.CharacterId });
            }
            else
            {
                return View(skill);
            }
        }

        public ActionResult Edit(int id)
        {
            EquationCategory abilityCategory = db.EquationCategories
                .Where(m => m.Name == "Ability Modifier" && m.CharacterId == id).FirstOrDefault<EquationCategory>();
            ViewBag.Abilities = db.Equations.Where(m => m.EquationCategoryId == abilityCategory.EquationCategoryId);

            return View(db.Skills.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Skills.Attach(skill);
                db.Entry(skill).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Skill", new { Id = skill.CharacterId });
            }
            else
            {
                return View(skill);
            }
        }
    }
}