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

        public ActionResult Index(int id)
        {
            //load all abilities and display them
            return View(new AbilityViewer(id));
        }

        [HttpPost]
        public ActionResult Index(AbilityViewer viewer)
        {
            //take selected category and filter abilities
            return View(new AbilityViewer(viewer.CharacterId, viewer.TypeId));
        }

        public ActionResult CreateAbility(int id)
        {
            //load ability categories for dropdown
            List<AbilityType> types = db.AbilityTypes.Where(m => m.CharacterId == id).ToList<AbilityType>();
            ViewBag.AbilityTypes = new SelectList(types, "AbilityTypeId", "Name");
            
            //initialize ability
            Ability ability = new Ability();
            ability.CharacterId = id;

            //go to ability creater
            return View(ability);
        }

        [HttpPost]
        public ActionResult CreateAbility(Ability ability)
        {
            if (ModelState.IsValid)
            {
                //if the ability is conditional, default active status to false
                //otherwise ability is active
                ability.Active = !ability.IsConditional;

                //add ability
                db.Abilities.Add(ability);
                db.SaveChanges();

                //go to ability index
                return RedirectToAction("Index", "Ability", new { Id = ability.CharacterId });
            }
            else
            {
                //something was missing, go back to editing
                return View(ability);
            }
        }

        public ActionResult EditAbility(int id)
        {
            //load ability categories for dropdown
            Ability ability = db.Abilities.Find(id);
            List<AbilityType> types = db.AbilityTypes.Where(m => m.CharacterId == ability.CharacterId).ToList<AbilityType>();
            ViewBag.AbilityTypes = new SelectList(types, "AbilityTypeId", "Name");

            //go to editor
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

        [HttpPost]
        public ActionResult UpdateAbility(Ability ability)
        {
            if (ModelState.IsValid)
            {
                db.Abilities.Attach(ability);
                db.Entry(ability).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                return Json(new { });
            }
            else
            {
                return Json(new { });
            }
        }
    }
}