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

        public ActionResult PreparedKnownSpells(int id)
        {
            return View(new PreparedKnownSpellsView(id));
        }

        public ActionResult SpontaneousKnownSpells(int id)
        {
            return View(new SpontaneousKnownSpellsView(id));
        }

        public ActionResult PointsKnownSpells(int id)
        {
            return View(new PointsKnownSpellsView(id));
        }

        public ActionResult PreparedCastSpells(int id)
        {
            return View(new PreparedCastSpellsView(id));
        }

        public ActionResult SpontaneousCastSpells(int id)
        {
            return View(new SpontaneousCastSpellsView(id));
        }

        public ActionResult PointsCastSpells(int id)
        {
            return View(new PointsCastSpellsView(id));
        }

        public ActionResult CreatePreparedSpellLevel(int id)
        {
            PreparedSpellLevel preparedSpellLevel = new PreparedSpellLevel();
            preparedSpellLevel.SpellbookId = id;
            return View(preparedSpellLevel);
        }

        [HttpPost]
        public ActionResult CreatePreparedSpellLevel(PreparedSpellLevel preparedSpellLevel)
        {
            if (ModelState.IsValid)
            {
                db.SpellLevels.Add(preparedSpellLevel);
                db.SaveChanges();

                return RedirectToAction("PreparedKnownSpells", "Spell", new { Id = preparedSpellLevel.SpellbookId });
            }
            else
            {
                return View(preparedSpellLevel);
            }
        }

        public ActionResult CreatePointsSpellLevel(int id)
        {
            PointsSpellLevel pointsSpellLevel = new PointsSpellLevel();
            pointsSpellLevel.SpellbookId = id;
            return View(pointsSpellLevel);
        }

        [HttpPost]
        public ActionResult CreatePointsSpellLevel(PointsSpellLevel pointsSpellLevel)
        {
            if (ModelState.IsValid)
            {
                db.SpellLevels.Add(pointsSpellLevel);
                db.SaveChanges();

                return RedirectToAction("PointsKnownSpells", "Spell", new { Id = pointsSpellLevel.SpellbookId });
            }
            else
            {
                return View(pointsSpellLevel);
            }
        }

        public ActionResult CreateSpontaneousSpellLevel(int id)
        {
            SpontaneousSpellLevel spontaneousSpellLevel = new SpontaneousSpellLevel();
            spontaneousSpellLevel.SpellbookId = id;
            return View(spontaneousSpellLevel);
        }

        [HttpPost]
        public ActionResult CreateSpontaneousSpellLevel(SpontaneousSpellLevel spontaneousSpellLevel)
        {
            if (ModelState.IsValid)
            {
                db.SpellLevels.Add(spontaneousSpellLevel);
                db.SaveChanges();

                return RedirectToAction("SpontaneousKnownSpells", "Spell", new { Id = spontaneousSpellLevel.SpellbookId });
            }
            else
            {
                return View(spontaneousSpellLevel);
            }
        }

        public ActionResult EditPreparedSpellLevel(int id)
        {
            return View(db.PreparedSpellLevels.Find(id));
        }

        [HttpPost]
        public ActionResult EditPreparedSpellLevel(PreparedSpellLevel preparedSpellLevel)
        {
            if (ModelState.IsValid)
            {
                db.PreparedSpellLevels.Attach(preparedSpellLevel);
                db.Entry(preparedSpellLevel).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("PreparedKnownSpells", "Spell", new { Id = preparedSpellLevel.SpellbookId });
            }
            else
            {
                return View(preparedSpellLevel);
            }
        }

        public ActionResult EditSpontaneousSpellLevel(int id)
        {
            return View(db.SpontaneousSpellLevels.Find(id));
        }

        [HttpPost]
        public ActionResult EditSpontaneousSpellLevel(SpontaneousSpellLevel spontaneousSpellLevel)
        {
            if (ModelState.IsValid)
            {
                db.SpontaneousSpellLevels.Attach(spontaneousSpellLevel);
                db.Entry(spontaneousSpellLevel).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("SpontaneousKnownSpells", "Spell", new { Id = spontaneousSpellLevel.SpellbookId });
            }
            else
            {
                return View(spontaneousSpellLevel);
            }
        }

        public ActionResult EditPointsSpellLevel(int id)
        {
            return View(db.PointsSpellLevels.Find(id));
        }

        [HttpPost]
        public ActionResult EditPointsSpellLevel(PointsSpellLevel pointsSpellLevel)
        {
            if (ModelState.IsValid)
            {
                db.PointsSpellLevels.Attach(pointsSpellLevel);
                db.Entry(pointsSpellLevel).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("PointsKnownSpells", "Spell", new { Id = pointsSpellLevel.SpellbookId });
            }
            else
            {
                return View(pointsSpellLevel);
            }
        }

        public ActionResult CreateSpell(int id)
        {
            Spell spell = new Spell();
            return View(spell);
        }

        [HttpPost]
        public ActionResult CreateSpell(Spell spell)
        {
            if (ModelState.IsValid)
            {
                SpellLevel spellLevel = db.SpellLevels.Find(spell.SpellLevelId);
                Spellbook spellbook = db.Spellbooks.Find(spellLevel.SpellbookId);

                if (spellbook.Type == "Points")
                {
                    spell.Prepared = 1;
                    db.Spells.Add(spell);
                    db.SaveChanges();

                    return RedirectToAction("PointsKnownSpells", "Spell", new { Id = spellbook.SpellbookId });
                }
                else if (spellbook.Type == "Prepared")
                {
                    spell.Prepared = 0;
                    db.Spells.Add(spell);
                    db.SaveChanges();
                    
                    return RedirectToAction("PreparedKnownSpells", "Spell", new { Id = spellbook.SpellbookId });
                }
                else if (spellbook.Type == "Spontaneous")
                {
                    spell.Prepared = 1;
                    db.Spells.Add(spell);
                    db.SaveChanges();
                    
                    return RedirectToAction("SpontaneousKnownSpells", "Spell", new { Id = spellbook.SpellbookId });
                }
                else
                {
                    return RedirectToAction("Index", "Spell", new { Id = Session["CharacterId"] });
                }
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

                SpellLevel spellLevel = db.SpellLevels.Find(spell.SpellLevelId);

                return RedirectToAction("KnownSpells", "Spell", new { Id = spellLevel.SpellbookId });
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

        public ActionResult CastPointsSpell(int id)
        {
            Spell spell = db.Spells.Find(id);
            PointsSpellLevel spellLevel = db.PointsSpellLevels.Find(spell.SpellLevelId);
            Spellbook spellbook = db.Spellbooks.Find(spellLevel.SpellbookId);
            Counter counter = db.Counters.Find(spellbook.PointsCounterId);

            counter.Count += spellLevel.SpellCost;
            db.SaveChanges();

            return RedirectToAction("View", "Spell", new { Id = spell.SpellId });
        }

        public ActionResult CastPreparedSpell(int id)
        {
            Spell spell = db.Spells.Find(id);
            spell.Prepared -= 1;
            db.SaveChanges();

            return RedirectToAction("View", "Spell", new { Id = spell.SpellId });
        }
    }
}