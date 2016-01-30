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
                name: "SearchTrainers",
                url: "GetTrainers/{s}",
                defaults: new
                {
                    controller = "Search",
                    action = "GetTrainers",
                    s = UrlParameter.Optional,
                }
            );

            routes.MapRoute(
                name: "SearchHalls",
                url: "GetHalls/{s}",
                defaults: new
                {
                    controller = "Search",
                    action = "GetHalls",
                    s = UrlParameter.Optional,
                }
            );

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
                name: "Trainer",
                url: "Trainer/{id}",
                defaults: new
                {
                    controller = "Search",
                    action = "Trainer",
                }
            );
            routes.MapRoute(
                name: "Hall",
                url: "Hall/{id}",
                defaults: new
                {
                    controller = "Search",
                    action = "Hall",
                }
            );

            routes.MapRoute(
                name: "Workout",
                url: "Workout/{id}",
                defaults: new
                {
                    controller = "Search",
                    action = "Workout",
                }
            );

            routes.MapRoute(
                name: "Common_about",
                url: "About",
                defaults: new
                {
                    controller = "Common",
                    action = "About",
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
