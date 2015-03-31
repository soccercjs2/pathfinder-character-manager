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

                return RedirectToAction("ViewCharacter", "Character", new { Id = newCharacter.CharacterId });
            }
            else
            {
                return View(newCharacter);
            }
        }

        public ActionResult ViewCharacter(int id)
        {
            PlayerCharacter playerCharacter = new PlayerCharacter(id);
            return View(playerCharacter);
        }

        [HttpPost]
        public ActionResult UpdateAbility(Ability ability)
        {
            if (ModelState.IsValid)
            {
                db.Abilities.Attach(ability);
                db.Entry(ability).State = System.Data.Entity.EntityState.Modified;
                
                db.SaveChanges();

                return RedirectToAction("ViewCharacter", "Character", new { Id = ability.CharacterId });
            }
            else
            {
                return RedirectToAction("ViewCharacter", "Character", new { Id = ability.CharacterId });
            }
        }

        public ActionResult Roller(string name, string value)
        {
            Roller roller = new Roller(name, value);
            return View(roller);
        }

        [HttpPost]
        public ActionResult Roller(Roller roller)
        {
            ModelState.Remove("RollResult");
            roller.Roll();
            return View(roller);
        }
    }
}