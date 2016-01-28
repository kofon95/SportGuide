using System.Collections.Generic;
using System.Data.Entity;
using Dal;
using SportGuideASP.Core.ViewModels;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using SportGuideASP.Core;
using static SportGuideASP.StaticData;
using static Utils.Consts;

namespace SportGuideASP.Controllers
{
    public class SearchController : Controller
    {
        DataManager _dm = new DataManager();
        // GET: Home
        public async Task<ActionResult> Index(string s, int? categoryId, int? kindOfSportId)
        {
            var workouts = _dm.Workout.GetAll();

            var model = await Task.Run(() =>
            {
                #region WHERE

                if (kindOfSportId != null)
                    workouts = workouts.Where(t => t.kind_of_sport_id == kindOfSportId);
                else if (categoryId != null)
                    workouts = workouts.Where(t => t.KindOfSport.category_id == categoryId);
                if (s != null && s.Length > 0)
                    workouts = workouts.Where(t =>
                        t.Hall.hall_name.Contains(s) ||
                        t.info.Contains(s) ||
                        t.Hall.address.Contains(s) ||
                        t.Trainer.name.Contains(s) ||
                        t.Hall.description.Contains(s));

                #endregion

                #region JOIN

                return from w in workouts
                       join k in _dm.KindOfSport.GetAll() on w.kind_of_sport_id equals k.id
                       join c in _dm.Category.GetAll() on k.category_id equals c.id
                       select new SearchViewModel.Index
                       {
                           Id = w.id,
                           CategoryName = c.category_name,
                           KindOfSport = k.sport_name,
                           TimeForWorkout = w.time,
                           OtherInformation = w.info,
                       };

                #endregion
            });
            return View(model);
        }

        public ActionResult Workout(int? id)
        {
            if (id == null)
            {
                Log.Debug("Workout's id is null");
                return RedirectToAction("Index");
            }

            IEnumerable<SearchViewModel.Workout> workouts =
                    from w in _dm.Workout.GetAll().Where(t => t.id == id)
                    join k in _dm.KindOfSport.GetAll() on w.kind_of_sport_id equals k.id
                    join c in _dm.Category.GetAll() on k.category_id equals c.id
                    join t in _dm.Trainer.GetAll() on w.trainer_id equals t.id
                    join h in DalExtensions.Include(_dm.Hall.GetAll(), nameof(_dm.HallImages)) on w.hall_id equals h.id
                    join m in _dm.HallYandexMapLocation.GetAll() on h.id equals m.id
                    //join i in _dm.HallImages.GetAll() on h.id equals i.hall_id
                    select new SearchViewModel.Workout
                    {
                        Id = w.id,
                        TimeForWorkout = w.time,

                        DateMonday = w.mon,
                        DateTuesday = w.tue,
                        DateWednesday = w.wed,
                        DateThursday = w.thu,
                        DateFriday = w.fri,
                        DateSaturday = w.sat,
                        DateSunday = w.sun,

                        KindOfSport = k.sport_name,
                        CategoryName = c.category_name,

                        TrainerName = t.name,
                        TrainerImageSource = UrlPaths.TrainerImageSource + t.photo_src,
                        TrainersPhoneNumber = t.phone_number,

                        HallName = h.hall_name,
                        HallImages = h.HallImages.Select(t => UrlPaths.HallImageSource + t.src),
                        Address = h.address,
                        Longitude = m.longitude,
                        Latitude = m.latitude,
                    };

            SearchViewModel.Workout model = workouts.FirstOrDefault();

            if (model == null)
            {
                Log.Debug("Workout's id not exists. Id = " + id);
                return RedirectToAction("Index"); // or 404
            }

            return View(model);
        }
    }
}