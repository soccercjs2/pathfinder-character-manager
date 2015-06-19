using Pathfinder.Models;
using Pathfinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pathfinder.Controllers
{
    public class CharacterController : Controller
    {
        private PathfinderContext db = new PathfinderContext();

        // GET: Character
        public ActionResult Index()
        {
            List<Character> characters = db.Characters.ToList();
            Session.Clear();
            return View(characters);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Character newCharacter)
        {
            if (ModelState.IsValid)
            {
                db.Characters.Add(newCharacter);
                db.SaveChanges();

                newCharacter.InitializeCharacter();

                return RedirectToAction("View", "Character", new { Id = newCharacter.CharacterId });
            }
            else
            {
                return View(newCharacter);
            }
        }

        public ActionResult View(int id)
        {
            CharacterView character = new CharacterView(id);
            Session["CharacterId"] = character.CharacterId;
            return View(character);
        }

        public ActionResult UpdateCurrentHealth(int id)
        {
            return View(db.Characters.Find(id));
        }

        [HttpPost]
        public ActionResult TakeDamage(Character character)
        {
            if (ModelState.IsValid)
            {
                db.Characters.Attach(character);
                db.Entry(character).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("View", "Character", new { Id = character.CharacterId });
            }
            else
            {
                return View(character);
            }
        }
    }
}