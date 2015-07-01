using Pathfinder.Models;
using Pathfinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pathfinder.Extensions;

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
            CharacterView characterView = new CharacterView(id);
            HealthUpdater updater = new HealthUpdater();
            updater.CharacterId = id;
            updater.CurrentHealth = characterView.CurrentHealth;
            updater.MaxHealth = Convert.ToInt32(characterView.MaximumHealth.Beautify());

            return View(updater);
        }

        [HttpPost]
        public ActionResult UpdateCurrentHealth(HealthUpdater updater, string submit)
        {
            if (ModelState.IsValid && submit != "Max Health")
            {
                Character character = db.Characters.Find(updater.CharacterId);

                switch (submit)
                {
                    case "Damage":
                        character.CurrentHealth -= updater.Health;
                        break;
                    case "Health":
                        character.CurrentHealth += updater.Health;
                        break;
                    default:
                        throw new Exception();
                }

                db.SaveChanges();

                return RedirectToAction("View", "Character", new { Id = character.CharacterId });
            }
            else if (submit == "Max Health")
            {
                Character character = db.Characters.Find(updater.CharacterId);
                character.CurrentHealth = updater.MaxHealth;
                db.SaveChanges();

                return RedirectToAction("View", "Character", new { Id = character.CharacterId });
            }
            else
            {
                return View(updater);
            }
        }

        public ActionResult UpdateExperience(int id)
        {
            CharacterView characterView = new CharacterView(id);
            ExperienceUpdater updater = new ExperienceUpdater();
            updater.CharacterId = id;
            updater.CurrentExperience = characterView.Experience;

            return View(updater);
        }

        [HttpPost]
        public ActionResult UpdateExperience(ExperienceUpdater updater)
        {
            if (ModelState.IsValid)
            {
                Character character = db.Characters.Find(updater.CharacterId);
                character.Experience += updater.Experience;
                db.SaveChanges();

                return RedirectToAction("View", "Character", new { Id = character.CharacterId });
            }
            else
            {
                return View(updater);
            }
        }

        public ActionResult CreateCounter(int id)
        {
            Counter counter = new Counter();
            counter.CharacterId = id;
            
            return View(counter);
        }

        [HttpPost]
        public ActionResult CreateCounter(Counter counter)
        {
            if (ModelState.IsValid)
            {
                db.Counters.Add(counter);
                db.SaveChanges();

                return RedirectToAction("View", "Character", new { Id = counter.CharacterId });
            }
            else
            {
                return View(counter);
            }
        }

        public ActionResult UpdateCounter(int id)
        {
            return View(db.Counters.Find(id));
        }
        
        [HttpPost]
        public ActionResult UpdateCounter(Counter counter)
        {
            if (ModelState.IsValid)
            {
                db.Counters.Attach(counter);
                db.Entry(counter).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("View", "Character", new { Id = counter.CharacterId });
            }
            else
            {
                return RedirectToAction("View", "Character", new { Id = counter.CharacterId });
            }
        }
    }
}