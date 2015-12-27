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
                name: "Search",
                url: "Search/{s}/{category_id}/{kind_id}",
                defaults: new
                {
                    controller = "Search",
                    action = "Index",
                    s = UrlParameter.Optional,
                    category_id = UrlParameter.Optional,
                    kind_id = UrlParameter.Optional,
                }
            );

            routes.MapRoute(
                name: "Workout",
                url: "Workout/{id}",
                defaults: new
                {
                    controller = "Search",
                    action = "Workout",
                    id = UrlParameter.Optional,
                }
            );

            routes.MapRoute(
                name: "Common",
                url: "{action}",
                defaults: new
                {
                    controller = "Common",
                    action = "Index",
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Common", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
