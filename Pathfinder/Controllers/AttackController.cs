using Pathfinder.Models;
using Pathfinder.ViewModels;
using System;
using System.Collections.Generic;
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
            return View(new PlayerCharacter(id));
        }

        public ActionResult Create(int id)
        {
            List<Weapon> weapons = db.Weapons.Where(m => m.CharacterId == id).ToList<Weapon>();
            ViewBag.Weapons = new SelectList(weapons, "WeaponId", "Name");

            EquationCategory damageCategory = db.EquationCategories
                .Where(m => m.CharacterId == id && m.Name == "Damage").FirstOrDefault<EquationCategory>();

            List<EquationCategory> damageCategories = db.EquationCategories
                .Where(m => m.CharacterId == id && m.EquationCategoryId == damageCategory.EquationCategoryId).ToList<EquationCategory>();

            ViewBag.DamageEquations = new Sele
            
            Attack attack = new Attack();
            attack.CharacterId = id;

            return View(attack);
        }

        [HttpPost]
        public ActionResult Create(Attack attack)
        {
            if (ModelState.IsValid)
            {
                db.Attacks.Add(attack);
                db.SaveChanges();

                return RedirectToAction("Index", "Attack", new { Id = attack.CharacterId });
            }
            else
            {
                return View(attack);
            }
        }

        public ActionResult SubAttack(int id)
        {
            return View(new AttackView(id));
        }

        public ActionResult CreateSubAttack(int id)
        {
            Attack attack = db.Attacks.Where(m => m.AttackId == id).FirstOrDefault<Attack>();
            EquationCategory attackCategory = db.EquationCategories
                .Where(m => m.CharacterId == attack.CharacterId 
                    && m.Name == "Attacks").FirstOrDefault<EquationCategory>();

            List<Equation> equations = db.Equations
                .Where(m => m.CharacterId == attack.CharacterId 
                    && m.EquationCategoryId == attackCategory.EquationCategoryId).ToList<Equation>();

            ViewBag.Equations = new SelectList(equations, "EquationId", "Name");
            
            SubAttack subAttack = new SubAttack();
            subAttack.AttackId = id;
            return View(subAttack);
        }

        [HttpPost]
        public ActionResult CreateSubAttack(SubAttack subAttack)
        {
            if (ModelState.IsValid)
            {
                db.SubAttacks.Add(subAttack);
                db.SaveChanges();

                return RedirectToAction("SubAttack", "Attack", new { Id = subAttack.AttackId });
            }
            else
            {
                return View(subAttack);
            }
        }
    }
}