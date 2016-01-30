using Dal;
using SportGuideASP.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static SportGuideASP.StaticData;
using static Utils.Consts;

namespace SportGuideASP.Controllers
{
    public class SearchController : Controller
    {
        DataManager _dm = new DataManager();
        // GET: Home
        public async Task<ActionResult> Index(string s, int? categoryId, int? kindOfSportId, string gender)
        {
            var workouts = _dm.Workout.GetAll();

            var model = await Task.Run(() =>
            {
                #region WHERE

                if (kindOfSportId != null)
                    workouts = workouts.Where(t => t.kind_of_sport_id == kindOfSportId);
                else if (categoryId != null)
                    workouts = workouts.Where(t => t.KindOfSport.category_id == categoryId);
                if (gender == "m" || gender == "f")
                    workouts = workouts.Where(t => t.gender_of_athlete == gender);
                if (s != null && s.Length > 0)
                    workouts = workouts.Where(t =>
                        t.info.Contains(s) ||
                        t.Hall.hall_name.Contains(s) ||
                        //t.Hall.address.Contains(s) ||
                        t.Hall.description.Contains(s) ||
                        t.KindOfSport.sport_name.Contains(s) ||
                        t.KindOfSport.Category.category_name.Contains(s));

                #endregion

                #region JOIN

                return from w in workouts
                       join k in _dm.KindOfSport.GetAll() on w.kind_of_sport_id equals k.id
                       join c in _dm.Category.GetAll() on k.category_id equals c.id
                       select new SearchViewModel.Index
                       {
                           Id = w.id,
                           GenderOfAthlete = w.gender_of_athlete,
                           CategoryName = c.category_name,
                           KindOfSport = k.sport_name,
                           TimeForWorkout = w.time,
                           OtherInformation = w.info,
                       };

                #endregion
            });
            return View(model);
        }

        public ActionResult Workout(int id)
        {
            IEnumerable<SearchViewModel.Workout> workouts =
                    from w in _dm.Workout.GetAll().Where(t => t.id == id)
                    join k in _dm.KindOfSport.GetAll() on w.kind_of_sport_id equals k.id
                    join c in _dm.Category.GetAll() on k.category_id equals c.id
                    join t in _dm.Trainer.GetAll() on w.trainer_id equals t.id into ts
                    from t in ts.DefaultIfEmpty()
                    join h in DalExtensions.Include(_dm.Hall.GetAll(), nameof(_dm.HallImages)) on w.hall_id equals h.id
                    //join m in _dm.HallYandexMapLocation.GetAll() on h.id equals m.id
                    //join i in _dm.HallImages.GetAll() on h.id equals i.hall_id
                    select new SearchViewModel.Workout
                    {
                        Id = w.id,
                        Info = w.info,
                        TimeForWorkout = w.time,

                        DateMonday = w.mon,
                        DateTuesday = w.tue,
                        DateWednesday = w.wed,
                        DateThursday = w.thu,
                        DateFriday = w.fri,
                        DateSaturday = w.sat,
                        DateSunday = w.sun,

                        MinAge = w.min_age,
                        MaxAge = w.max_age,

                        GenderOfAthlete = w.gender_of_athlete,
                        KindOfSport = k.sport_name,
                        CategoryName = c.category_name,

                        TrainerId = t.id,
                        TrainerName = t.name,
                        TrainerImageSource = t.photo_src == null ? null : UrlPaths.TrainerImageSource + t.photo_src,
                        TrainersPhoneNumber = t.phone_number,

                        HallId = h.id,
                        HallName = h.hall_name,
                        HallImages = h.HallImages.Select(t => UrlPaths.HallImageSource + t.src),
                        Address = h.address,
                        Longitude = h.HallYandexMapLocation.longitude,
                        Latitude = h.HallYandexMapLocation.latitude,
                    };

            SearchViewModel.Workout model = workouts.FirstOrDefault();

            if (model == null)
            {
                Log.Debug("Workout's id not exists. Id = " + id);
                throw new System.Web.HttpException(404, "Workout not exists");
            }

            return View(model);
        }

        public ActionResult Trainer(int id)
        {
            var trainer = _dm.Trainer.GetAll().AsNoTracking()
                    .Select(t => new SearchViewModel.Trainer
                    {
                        Id = t.id,
                        Birthday = t.birthday,
                        Name = t.name,
                        PhoneNumber = t.phone_number,
                        PhotoSrc = t.photo_src == null ? null : UrlPaths.TrainerImageSource + t.photo_src,
                        Workouts = t.Workout.Select(w =>
                            new SearchViewModel.Workout
                            {
                                Id = w.id,
                                HallImages = w.Hall.HallImages.Select(i => UrlPaths.HallImageSource + i.src),
                                Info = w.info,
                                GenderOfAthlete = w.gender_of_athlete,
                            })
                    })
                    .First(t => t.Id == id);

            return View(trainer);
        }

        public ActionResult Hall(int id)
        {
            var hall = (
                from h in _dm.Hall.GetAll().AsNoTracking()
                select new SearchViewModel.Hall
                {
                    Id = h.id,
                    Name = h.hall_name,
                    Address = h.address,
                    Description = h.description,
                    CityName = h.City.name,
                    Images = h.HallImages.Select(i => UrlPaths.HallImageSource + i.src),
                    PhoneNumbers = h.PhoneOfHall.Select(p => p.phone_number),
                    MapLocation = h.HallYandexMapLocation,

                    Workouts = h.Workout.Select(w => new SearchViewModel.Workout
                    {
                        Id = w.id,
                        Info = w.info,
                        GenderOfAthlete = w.gender_of_athlete,

                        TrainerId = w.Trainer.id,
                        TrainerName = w.Trainer.name,

                        TimeForWorkout = w.time,

                        KindOfSportId = w.KindOfSport.id,
                        KindOfSport = w.KindOfSport.sport_name,

                        CategoryId = w.KindOfSport.Category.id,
                        CategoryName = w.KindOfSport.Category.category_name,
                    }),
                }
                ).FirstOrDefault(t => t.Id == id);
            
            return View(hall);
        }


        public ActionResult GetTrainers(string s, string gender, int? categoryId, int? kindOfSportId)
        {
            var tw = from t in _dm.Trainer.GetAll().AsNoTracking()
                           join w in _dm.Workout.GetAll().AsNoTracking() on t.id equals w.trainer_id into tw0
                           from w in tw0.DefaultIfEmpty()
                           select new { Trainer=t, Workout=w };

            if (kindOfSportId != null)
                tw = tw.Where(t => t.Workout.kind_of_sport_id == kindOfSportId);
            else if (categoryId != null)
                tw = tw.Where(t => t.Workout.KindOfSport.category_id == categoryId);
            if (gender == "m" || gender == "f")
                tw = tw.Where(t => t.Workout.gender_of_athlete == gender);
            if (!string.IsNullOrWhiteSpace(s))
                tw = tw.Where(t => t.Trainer.name.Contains(s) || t.Trainer.phone_number.Contains(s));

            ViewBag.PrefixTrainerImage = UrlPaths.TrainerImageSource;


            List<Dal.Trainer> list = new List<Dal.Trainer>();
            int lastId = 0;
            foreach (var item in tw.Select(t => t.Trainer).ToList())
            {
                if (lastId == item.id)
                    continue;
                list.Add(item);
                lastId = item.id;
            }

            return View(list);
        }

        public ActionResult GetHalls(string s, string gender, int? categoryId, int? kindOfSportId)
        {
            var a = from w in _dm.Workout.GetAll().AsNoTracking()
                    join k in _dm.KindOfSport.GetAll().AsNoTracking() on w.kind_of_sport_id equals k.id
                    select new { w, k };

            var halls = from h in _dm.Hall.GetAll()
                        join w_k in (
                            from w in _dm.Workout.GetAll().AsNoTracking()
                            join k in _dm.KindOfSport.GetAll().AsNoTracking() on w.kind_of_sport_id equals k.id
                            select new { w.hall_id, w.gender_of_athlete, KindOfSportId=k.id, CategoryId=k.category_id, }
                        ) on h.id equals w_k.hall_id
                        select new { Hall = h, Other = w_k };

            if (kindOfSportId != null)
                halls = halls.Where(t => t.Other.KindOfSportId == kindOfSportId);
            else if (categoryId != null)
                halls = halls.Where(t => t.Other.CategoryId == categoryId);
            if (gender == "m" || gender == "f")
                halls = halls.Where(t => t.Other.gender_of_athlete == gender);
            if (!string.IsNullOrWhiteSpace(s))
                halls = halls.Where(t => t.Hall.hall_name.Contains(s));

            var model = halls.Select(t => new SearchViewModel.GetHalls
            {
                Address = t.Hall.address,
                CityId = t.Hall.City.id,
                CityName = t.Hall.City.name,
                Description = t.Hall.description,
                HallImages = t.Hall.HallImages.Select(i => UrlPaths.HallImageSource + i.src),
                Id = t.Hall.id,
                LocationLatitude = t.Hall.HallYandexMapLocation.latitude,
                LocationLongitude = t.Hall.HallYandexMapLocation.longitude,
                Name = t.Hall.hall_name,
            });

            List<SearchViewModel.GetHalls> list = new List<SearchViewModel.GetHalls>();
            int lastId = 0;
            foreach (var item in model.ToList())
            {
                if (lastId == item.Id)
                    continue;
                list.Add(item);
                lastId = item.Id;
            }

            return View(list);
        }
    }
}