using System.Web.Mvc;

namespace SportGuideASP.Controllers
{
    public class CommonController : Controller
    {
        // Start page
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Reset()
        {
            StaticData.Reset();
            return View();
        }
    }
}