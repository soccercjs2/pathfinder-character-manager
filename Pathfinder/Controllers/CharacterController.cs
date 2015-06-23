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

        [HttpPost]
        public ActionResult UpdateAbility(Ability ability)
        {
            string redirectUrl = new UrlHelper(Request.RequestContext).Action("View", "Character", new { Id = ability.CharacterId });

            if (ModelState.IsValid)
            {
                db.Abilities.Attach(ability);
                db.Entry(ability).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                return Json(new { Url = redirectUrl });
            }
            else
            {
                return Json(new { Url = redirectUrl });
            }
        }
    }
}