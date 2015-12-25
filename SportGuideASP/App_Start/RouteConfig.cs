using System.Web.Mvc;
using System.Web.Routing;

namespace SportGuideASP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Search",
                url: "{controller}/{action}/{s}/{category_id}/{kind_id}",
                defaults: new { controller = "Search", action = "Index",
                    s = UrlParameter.Optional,
                    category_id = UrlParameter.Optional,
                    kind_id = UrlParameter.Optional,
                }
            );
        }
    }
}
