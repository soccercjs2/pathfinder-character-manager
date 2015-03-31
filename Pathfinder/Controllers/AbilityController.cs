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
    public class AbilityController : Controller
    {
        private PathfinderContext db = new PathfinderContext();

        // GET: Ability
        public ActionResult Index(int id)
        {
            return View(new AbilityViewer(id));
        }

        [HttpPost]
        public ActionResult Index(AbilityViewer viewer)
        {
            return View(new AbilityViewer(viewer.CharacterId, viewer.TypeId));
        }

        public ActionResult CreateAbility(int id)
        {
            List<AbilityType> types = db.AbilityTypes.Where(m => m.CharacterId == id).ToList<AbilityType>();
            ViewBag.AbilityTypes = new SelectList(types, "AbilityTypeId", "Name");
            
            Ability ability = new Ability();
            ability.CharacterId = id;
            return View(ability);
        }

        [HttpPost]
        public ActionResult CreateAbility(Ability ability)
        {
            if (ModelState.IsValid)
            {
                db.Abilities.Add(ability);
                db.SaveChanges();

                return RedirectToAction("Index", "Ability", new { Id = ability.CharacterId });
            }
            else
            {
                return View(ability);
            }
        }

        public ActionResult EditAbility(int id)
        {
            Ability ability = db.Abilities.Find(id);
            List<AbilityType> types = db.AbilityTypes.Where(m => m.CharacterId == ability.CharacterId).ToList<AbilityType>();
            ViewBag.AbilityTypes = new SelectList(types, "AbilityTypeId", "Name");

            return View(ability);
        }

        [HttpPost]
        public ActionResult EditAbility(Ability ability)
        {
            if (ModelState.IsValid)
            {
                db.Abilities.Attach(ability);
                db.Entry(ability).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Ability", new { Id = ability.CharacterId });
            }
            else
            {
                return View(ability);
            }
        }

        public ActionResult DeleteAbility(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteAbility(Ability ability)
        {
            return View();
        }

        public ActionResult CreateAbilityType(int id)
        {
            AbilityType type = new AbilityType();
            type.CharacterId = id;
            return View(type);
        }

        [HttpPost]
        public ActionResult CreateAbilityType(AbilityType abilityType)
        {
            if (ModelState.IsValid)
            {
                db.AbilityTypes.Add(abilityType);
                db.SaveChanges();

                return RedirectToAction("Index", "Ability", new { Id = abilityType.CharacterId });
            }
            else
            {
                return View(abilityType);
            }
        }

        public ActionResult AbilityBonuses(int id)
        {
            return View(new AbilityBonusViewer(id));
        }
    }
}