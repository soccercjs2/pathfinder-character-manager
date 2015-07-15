using Pathfinder.App_Start;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pathfinder
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RedirectingAction());
        }
    }
}
