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
    public class SpellController : Controller
    {
        private PathfinderContext db = new PathfinderContext();

        public ActionResult Index(int id)
        {
            List<Spellbook> spellbooks = db.Spellbooks.Where(m => m.CharacterId == id).ToList<Spellbook>();
            return View(spellbooks);
        }

        public ActionResult CreateSpellbook(int id)
        {
            Spellbook spellbook = new Spellbook();
            spellbook.CharacterId = id;
            spellbook.PointsCounterId = -1;
            return View(spellbook);
        }

        [HttpPost]
        public ActionResult CreateSpellbook(Spellbook spellbook)
        {
            if (ModelState.IsValid)
            {
                db.Spellbooks.Add(spellbook);
                db.SaveChanges();

                return RedirectToAction("Index", "Spell", new { Id = spellbook.CharacterId });
            }
            else
            {
                return View(spellbook);
            }
        }

        public ActionResult EditSpellbook(int id)
        {
            return View(db.Spellbooks.Find(id));
        }

        [HttpPost]
        public ActionResult EditSpellbook(Spellbook spellbook)
        {
            if (ModelState.IsValid)
            {
                db.Spellbooks.Attach(spellbook);
                db.Entry(spellbook).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Spell", new { Id = spellbook.CharacterId });
            }
            else
            {
                return View(spellbook);
            }
        }

        public ActionResult KnownSpells(int id)
        {
            return View(new KnownSpellsView(id));
        }

        public ActionResult CastSpells(int id)
        {
            return View(new CastSpellsView(id));
        }

        public ActionResult CreateSpellLevel(int id)
        {
            SpellLevel knownSpellsByLevel = new SpellLevel();
            knownSpellsByLevel.SpellbookId = id;
            return View(knownSpellsByLevel);
        }

        [HttpPost]
        public ActionResult CreateSpellLevel(SpellLevel knownSpellsByLevel)
        {
            if (ModelState.IsValid)
            {
                db.SpellLevels.Add(knownSpellsByLevel);
                db.SaveChanges();

                return RedirectToAction("KnownSpells", "Spell", new { Id = knownSpellsByLevel.SpellbookId });
            }
            else
            {
                return View(knownSpellsByLevel);
            }
        }

        public ActionResult EditSpellLevel(int id)
        {
            return View(db.SpellLevels.Find(id));
        }

        [HttpPost]
        public ActionResult EditSpellLevel(SpellLevel knownSpellsByLevel)
        {
            if (ModelState.IsValid)
            {
                db.SpellLevels.Attach(knownSpellsByLevel);
                db.Entry(knownSpellsByLevel).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("KnownSpells", "Spell", new { Id = knownSpellsByLevel.SpellbookId });
            }
            else
            {
                return View(knownSpellsByLevel);
            }
        }

        public ActionResult CreateSpell(int id)
        {
            Spell spell = new Spell();
            spell.SpellbookId = id;
            return View(spell);
        }

        [HttpPost]
        public ActionResult CreateSpell(Spell spell)
        {
            if (ModelState.IsValid)
            {
                db.Spells.Add(spell);
                db.SaveChanges();

                return RedirectToAction("KnownSpells", "Spell", new { Id = spell.SpellbookId });
            }
            else
            {
                return View(spell);
            }
        }

        public ActionResult EditSpell(int id)
        {
            return View(db.Spells.Find(id));
        }

        [HttpPost]
        public ActionResult EditSpell(Spell spell)
        {
            if (ModelState.IsValid)
            {
                db.Spells.Attach(spell);
                db.Entry(spell).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("KnownSpells", "Spell", new { Id = spell.SpellbookId });
            }
            else
            {
                return View(spell);
            }
        }

        [HttpPost]
        public ActionResult UpdateSpell(Spell spell)
        {
            if (ModelState.IsValid)
            {
                db.Spells.Attach(spell);
                db.Entry(spell).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges(); //asdf

                return Json(new { });
            }
            else
            {
                return Json(new { });
            }
        }
    }
}