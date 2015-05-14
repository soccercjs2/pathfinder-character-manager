using Pathfinder.Models;
using Pathfinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pathfinder.Controllers
{
    public class InventoryController : Controller
    {
        private PathfinderContext db = new PathfinderContext();
        
        public ActionResult Index(int id)
        {
            return View(new InventoryViewer(id));
        }

        //weapons
        public ActionResult CreateWeapon(int id)
        {
            Weapon weapon = new Weapon();
            weapon.CharacterId = id;
            return View(weapon);
        }

        [HttpPost]
        public ActionResult CreateWeapon(Weapon weapon)
        {
            if (ModelState.IsValid)
            {
                db.Weapons.Add(weapon);
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = weapon.CharacterId });
            }
            else
            {
                return View(weapon);
            }
        }

        public ActionResult EditWeapon(int id)
        {
            return View(db.Weapons.Find(id));
        }

        [HttpPost]
        public ActionResult EditWeapon(Weapon weapon)
        {
            if (ModelState.IsValid)
            {
                db.Weapons.Attach(weapon);
                db.Entry(weapon).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = weapon.CharacterId });
            }
            else
            {
                return View(weapon);
            }
        }

        //armors
        public ActionResult CreateArmor(int id)
        {
            Armor armor = new Armor();
            armor.CharacterId = id;
            return View(armor);
        }

        [HttpPost]
        public ActionResult CreateArmor(Armor armor)
        {
            if (ModelState.IsValid)
            {
                db.Armors.Add(armor);
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = armor.CharacterId });
            }
            else
            {
                return View(armor);
            }
        }

        public ActionResult EditArmor(int id)
        {
            return View(db.Armors.Find(id));
        }

        [HttpPost]
        public ActionResult EditArmor(Armor armor)
        {
            if (ModelState.IsValid)
            {
                db.Armors.Attach(armor);
                db.Entry(armor).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = armor.CharacterId });
            }
            else
            {
                return View(armor);
            }
        }

        //magic items
        public ActionResult CreateMagicItem(int id)
        {
            MagicItem magicItem = new MagicItem();
            magicItem.CharacterId = id;
            return View(magicItem);
        }

        [HttpPost]
        public ActionResult CreateMagicItem(MagicItem magicItem)
        {
            if (ModelState.IsValid)
            {
                db.MagicItems.Add(magicItem);
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = magicItem.CharacterId });
            }
            else
            {
                return View(magicItem);
            }
        }

        public ActionResult EditMagicItem(int id)
        {
            return View(db.MagicItems.Find(id));
        }

        [HttpPost]
        public ActionResult EditMagicItem(MagicItem magicItem)
        {
            if (ModelState.IsValid)
            {
                db.MagicItems.Attach(magicItem);
                db.Entry(magicItem).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = magicItem.CharacterId });
            }
            else
            {
                return View(magicItem);
            }
        }

        //gears
        public ActionResult CreateGear(int id)
        {
            Gear gear = new Gear();
            gear.CharacterId = id;
            return View(gear);
        }

        [HttpPost]
        public ActionResult CreateGear(Gear gear)
        {
            if (ModelState.IsValid)
            {
                db.Gears.Add(gear);
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = gear.CharacterId });
            }
            else
            {
                return View(gear);
            }
        }

        public ActionResult EditGear(int id)
        {
            return View(db.Gears.Find(id));
        }

        [HttpPost]
        public ActionResult EditGear(Gear gear)
        {
            if (ModelState.IsValid)
            {
                db.Gears.Attach(gear);
                db.Entry(gear).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = gear.CharacterId });
            }
            else
            {
                return View(gear);
            }
        }
	}
}