using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pathfinder
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Character", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Roller",
                url: "Character/Roller/{name}/{value}",
                defaults: new { controller = "Character", action = "Roller", name = "", value = 0 }
            );

            routes.MapRoute(
                name: "Ability",
                url: "Ability/{action}/{id}/{type}",
                defaults: new { controller = "Ability", action = "Index", id = UrlParameter.Optional, type = UrlParameter.Optional }
            );
        }
    }
}
