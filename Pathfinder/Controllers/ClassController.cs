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
            List<Class> classes = db.Classes.Where(m => m.CharacterId == id).ToList<Class>();
            return View(classes);
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
    }
}