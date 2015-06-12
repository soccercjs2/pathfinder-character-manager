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
    public class ClassController : Controller
    {
        private PathfinderContext db = new PathfinderContext();
        
        // GET: Class
        public ActionResult Index(int id)
        {
            return View(new ClassView(id));
        }

        public ActionResult Create(int id)
        {
            Class playerClass = new Class();
            playerClass.CharacterId = id;
            return View(playerClass);
        }

        [HttpPost]
        public ActionResult Create(Class playerClass)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(playerClass);
                db.SaveChanges();

                return RedirectToAction("Index", "Class", new { Id = playerClass.CharacterId });
            }
            else
            {
                return View(playerClass);
            }
        }

        public ActionResult Edit(int id)
        {
            return View(db.Classes.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Class playerClass)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Attach(playerClass);
                db.Entry(playerClass).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Class", new { Id = playerClass.CharacterId });
            }
            else
            {
                return View(playerClass);
            }
        }

        public ActionResult Delete(int id)
        {
            Class playerClass = db.Classes.Find(id);
            int characterId = playerClass.CharacterId;
            db.Classes.Remove(playerClass);
            db.SaveChanges();

            return RedirectToAction("Index", "Class", new { Id = characterId });
        }

        public ActionResult CreateClassHealth(int id)
        {
            ClassHealth classHealth = new ClassHealth();
            classHealth.CharacterId = id;

            List<Class> classes = db.Classes.Where(m => m.CharacterId == id).ToList<Class>();

            ViewBag.Classes = new SelectList(classes, "ClassId", "Name");

            return View(classHealth);
        }

        [HttpPost]
        public ActionResult CreateClassHealth(ClassHealth classHealth)
        {
            if (ModelState.IsValid)
            {
                db.ClasseHealths.Add(classHealth);
                db.SaveChanges();

                return RedirectToAction("Index", "Class", new { Id = classHealth.CharacterId });
            }
            else
            {
                return View(classHealth);
            }
        }
    }
}