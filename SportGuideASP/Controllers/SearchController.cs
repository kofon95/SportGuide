using Dal;
using SportGuideASP.Core.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using static SportGuideASP.StaticData;

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

            var model = (from w in _dm.Workout.GetAll()
                        join k in _dm.KindOfSport.GetAll() on w.kind_of_sport_id equals k.id
                        join c in _dm.Category.GetAll() on k.category_id equals c.id
                        join t in _dm.Trainer.GetAll() on w.trainer_id equals t.id
                        join h in _dm.Hall.GetAll() on w.hall_id equals h.id
                        join m in _dm.HallYandexMapLocation.GetAll() on h.id equals m.id
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
                            TrainerImageSource = t.photo_src,
                            TrainersPhoneNumber = t.phone_number,

                            HallName = h.hall_name,
                            Address = h.address,
                            Longitude = m.longitude,
                            Latitude = m.latitude,
                        }).FirstOrDefault(t => t.Id == id);

            if (model == null)
            {
                Log.Debug("Workout's id not exists. Id = " + id);
                return RedirectToAction("Index"); // or 404
            }

            return View(model);
        }
    }
}