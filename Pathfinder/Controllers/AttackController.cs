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
    public class AttackController : Controller
    {
        private PathfinderContext db = new PathfinderContext();

        // GET: Attack
        public ActionResult Index(int id)
        {
            return View(db.AttackGroups.Where(m => m.CharacterId == id));
        }

        public ActionResult CreateAttackGroup(int id)
        {
            return View(new AttackGroup(id));
        }

        [HttpPost]
        public ActionResult CreateAttackGroup(AttackGroup attackGroup)
        {
            if (ModelState.IsValid)
            {
                db.AttackGroups.Add(attackGroup);
                db.SaveChanges();

                return RedirectToAction("Index", "Attack", new { Id = attackGroup.CharacterId });
            }
            else
            {
                return View(attackGroup);
            }
        }

        public ActionResult View(int id)
        {
            return View(new AttackGroupView(id));
        }

        public ActionResult CreateAttack(int id)
        {
            AttackGroup attackGroup = db.AttackGroups.Find(id);
            List<Weapon> weapons = db.Weapons.Where(m => m.CharacterId == attackGroup.CharacterId).ToList<Weapon>();

            EquationCategory attackCategory = db.EquationCategories
                .Where(m => m.CharacterId == attackGroup.CharacterId && m.Name == "Attacks").FirstOrDefault<EquationCategory>();

            List<Equation> attackEquations = db.Equations
                .Where(m => m.CharacterId == attackGroup.CharacterId && m.EquationCategoryId == attackCategory.EquationCategoryId).ToList<Equation>();
            
            EquationCategory damageCategory = db.EquationCategories
                .Where(m => m.CharacterId == attackGroup.CharacterId && m.Name == "Damage").FirstOrDefault<EquationCategory>();

            List<Equation> damageEquations = db.Equations
                .Where(m => m.CharacterId == attackGroup.CharacterId && m.EquationCategoryId == damageCategory.EquationCategoryId).ToList<Equation>();

            ViewBag.Weapons = new SelectList(weapons, "WeaponId", "Name");
            ViewBag.AttackEquations = new SelectList(attackEquations, "EquationId", "Name");
            ViewBag.DamageEquations = new SelectList(damageEquations, "EquationId", "Name");
            
            return View(new Attack(id));
        }

        [HttpPost]
        public ActionResult CreateAttack(Attack attack)
        {
            if (ModelState.IsValid)
            {
                db.Attacks.Add(attack);
                db.SaveChanges();

                return RedirectToAction("View", "Attack", new { Id = attack.AttackGroupId });
            }
            else
            {
                return View(attack);
            }
        }

        public ActionResult EditAttackGroup(int id)
        {
            return View(db.AttackGroups.Find(id));
        }

        [HttpPost]
        public ActionResult EditAttackGroup(AttackGroup attack)
        {
            if (ModelState.IsValid)
            {
                db.AttackGroups.Attach(attack);
                db.Entry(attack).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("View", "Attack", new { Id = attack.AttackGroupId });
            }
            else
            {
                return View(attack);
            }
        }

        public ActionResult EditAttack(int id)
        {
            Attack attack = db.Attacks.Find(id);
            AttackGroup attackGroup = db.AttackGroups.Find(attack.AttackGroupId);
            List<Weapon> weapons = db.Weapons.Where(m => m.CharacterId == attackGroup.CharacterId).ToList<Weapon>();

            EquationCategory damageCategory = db.EquationCategories
                .Where(m => m.CharacterId == attackGroup.CharacterId && m.Name == "Damage").FirstOrDefault<EquationCategory>();

            List<Equation> damageEquations = db.Equations
                .Where(m => m.CharacterId == attackGroup.CharacterId && m.EquationCategoryId == damageCategory.EquationCategoryId).ToList<Equation>();

            EquationCategory attackCategory = db.EquationCategories
                .Where(m => m.CharacterId == attackGroup.CharacterId && m.Name == "Attacks").FirstOrDefault<EquationCategory>();

            List<Equation> attackEquations = db.Equations
                .Where(m => m.CharacterId == attackGroup.CharacterId && m.EquationCategoryId == attackCategory.EquationCategoryId).ToList<Equation>();

            ViewBag.Weapons = new SelectList(weapons, "WeaponId", "Name");
            ViewBag.AttackEquations = new SelectList(attackEquations, "EquationId", "Name");
            ViewBag.DamageEquations = new SelectList(damageEquations, "EquationId", "Name");

            return View(attack);
        }

        [HttpPost]
        public ActionResult EditAttack(Attack attack)
        {
            if (ModelState.IsValid)
            {
                db.Attacks.Attach(attack);
                db.Entry(attack).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("View", "Attack", new { Id = attack.AttackGroupId });
            }
            else
            {
                return View(attack);
            }
        }
    }
}