using Pathfinder.Models;
using Pathfinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pathfinder.Controllers
{
    public class TagController : Controller
    {
        private PathfinderContext db = new PathfinderContext();

        //// GET: Tag
        //public ActionResult Index(int id)
        //{
        //    return View(new TagViewer(id));
        //}

        //[HttpPost]
        //public ActionResult Index(TagViewer viewer)
        //{
        //    return View(new TagViewer(viewer.CharacterId, viewer.CategoryId));
        //}

        //public ActionResult Create(int id)
        //{
        //    ViewBag.Categories = db.TagCategories.Where(m => m.CharacterId == id).ToList<TagCategory>();
            
        //    Tag tag = new Tag();
        //    tag.CharacterId = id;
        //    return View(tag);
        //}

        //[HttpPost]
        //public ActionResult Create(Tag tag)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        tag.Value = "[" + tag.Value.ToUpper() + "]";
        //        db.Tags.Add(tag);
        //        db.SaveChanges();
        //        return RedirectToAction("Index", new { Id = tag.CharacterId });
        //    }
        //    else
        //    {
        //        return View(tag);
        //    }
        //}

        //public ActionResult CreateCategory(int id)
        //{
        //    TagCategory category = new TagCategory();
        //    category.CharacterId = id;
        //    return View(category);
        //}

        //[HttpPost]
        //public ActionResult CreateCategory(TagCategory category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TagCategories.Add(category);
        //        db.SaveChanges();
        //        return RedirectToAction("Index", new { Id = category.CharacterId });
        //    }
        //    else
        //    {
        //        return View(category);
        //    }
        //}
    }
}