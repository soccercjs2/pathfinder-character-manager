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
        
        //
        // GET: /Inventory/
        public ActionResult Index(int id)
        {
            return View(new InventoryViewer(id));
        }
	}
}