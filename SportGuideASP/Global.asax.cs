using System.Web.Mvc;
using System.Web.Routing;

namespace SportGuideASP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            StaticData.Reset();
        }
        protected void Application_BeginRequest()
        {
            StaticData.Log.Debug("Request user");
        }
    }
}
