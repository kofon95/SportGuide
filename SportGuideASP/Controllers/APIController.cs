using Dal;
using System.Linq;
using System.Web.Mvc;

namespace SportGuideASP.Controllers
{
    public class APIController : Controller
    {
        DataManager _dm = new DataManager();

        public ActionResult GetKindsOfSportByCategoryId(int id)
        {
            var kinds = _dm.KindOfSport.GetAll().Where(t => t.category_id == id).OrderBy(t => t.sport_name);
            return PartialView(kinds);
        }
    }
}