using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pathfinder.App_Start
{
    public class RedirectingAction : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var characterId = filterContext.HttpContext.Session["CharacterId"];

            if (characterId == null && 
                (filterContext.RouteData.Values["controller"].ToString().ToLower() != "character" ||
                filterContext.RouteData.Values["action"].ToString().ToLower() != "index"))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "Character",
                    area = ""
                }));
            }
        }
    }
}