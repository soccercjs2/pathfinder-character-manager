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
            Ability ability = new Ability();
            ability.CharacterId = id;
            return View();
        }

        [HttpPost]
        public ActionResult CreateAbility(Ability ability)
        {
            if (ModelState.IsValid)
            {
                db.Abilities.Add(ability);
                db.SaveChanges();

                return RedirectToAction("Index", "Abilities", new { Id = ability.CharacterId });
            }
            else
            {
                return View(ability);
            }
        }

        public ActionResult EditAbility(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditAbility(Ability ability)
        {
            if (ModelState.IsValid)
            {
                db.Abilities.Attach(ability);
                db.Entry(ability).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Abilities", new { Id = ability.CharacterId });
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
    }
}