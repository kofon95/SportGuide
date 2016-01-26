using Dal;
using Dal.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;

namespace SportGuideWebAPI.Controllers.v1
{
    [EnableQuery]
    public class GetController : ApiController
    {
        DataManager _dm = new DataManager { ProxyCreationEnabled = false };

        [HttpGet]
        public IQueryable<object> Cities()
        {
            return _dm.City.GetAll().Select(t => new { t.id, t.name, Republic = new { t.Republic.name, id = t.republic_id } });
        }

        [HttpGet]
        public IQueryable<object> KindsOfSports()
        {
            return _dm.KindOfSport.GetAll().AsNoTracking()
                .Select(t => new { t.id, t.sport_name, t.min_age, Category = new { t.Category.id, t.Category.category_name, } });
        }

        [HttpGet]
        public IQueryable<object> Categories()
        {
            return _dm.Category.GetAll().AsNoTracking()
                .Select(t => new { t.id, t.category_name, });
        }

        [HttpGet]
        public IQueryable<object> Trainers()
        {
            return _dm.Trainer.GetAll().AsNoTracking()
                .Select(t => new { t.id, t.name, t.birthday, t.phone_number, t.photo_src, t.register_date });
        }

        [HttpGet]
        public IQueryable<object> Users()
        {
            return _dm.User.GetAll().AsNoTracking()
                .Include(t => t.Login)
                .Include(t => t.LoginSocialNetwork)
                .Include(t => t.City)
                .Select(t => new
                {
                    t.id,
                    t.name,
                    t.gender,
                    t.birthday,
                    t.photo_is_local,
                    t.photo_src,
                    t.first_ip,
                    t.role,

                    Login = t.Login == null ? null : new { t.Login.email },
                    LoginSocialNetwork = t.LoginSocialNetwork == null ? null : new
                    {
                        t.LoginSocialNetwork.network_name,
                        t.LoginSocialNetwork.uid_sn
                    },
                    City = t.City == null ? null : new
                    {
                        t.City.id,
                        t.City.name
                    },
                });
        }

        [HttpGet]
        public IQueryable<object> Halls()
        {
            return _dm.Hall.GetAll().AsNoTracking()
                .Select(t => new
                {
                    t.address,
                    t.description,
                    t.hall_name,
                    t.id,
                    City = new { t.City.id, t.City.name },
                    HallImages = t.HallImages.Select(i => new { i.id, i.src }),
                    PhoneOfHall = t.PhoneOfHall.Select(p => new { p.id, p.phone_number }),
                    HallYandexMapLocation = new
                    {
                        t.HallYandexMapLocation.latitude,
                        t.HallYandexMapLocation.longitude
                    },
                    Workout = t.Workout.Select(w => new
                    {
                        w.id,
                        w.info,
                        KindOfSport = new { w.KindOfSport.id, w.KindOfSport.sport_name }
                    }),
                });
        }

        [HttpGet]
        public async Task<IQueryable<object>> Workouts()
        {
            return await Task.Run(() =>
                _dm.Workout.GetAll()
                    .AsNoTracking()
                    .Include(t => t.KindOfSport.Category)
                    .Include(t => t.Hall.City)
                    .Select(t => new
                    {
                        t.id,
                        t.paiment_for_month,
                        t.min_age,
                        t.max_age,
                        t.info,
                        t.time,
                        t.mon,
                        t.tue,
                        t.wed,
                        t.thu,
                        t.fri,
                        t.sat,
                        t.sun,

                        Hall = new
                        {
                            t.Hall.id,
                            t.Hall.hall_name,
                            t.Hall.address,
                            t.Hall.description,
                            City = new { t.Hall.City.id, t.Hall.City.name }
                        },
                        KindOfSport = new
                        {
                            t.KindOfSport.id,
                            t.KindOfSport.sport_name,
                            t.KindOfSport.min_age,
                            Category = new { t.KindOfSport.Category.id, t.KindOfSport.Category.category_name }
                        },
                        Trainer = new
                        {
                            t.Trainer.id,
                            t.Trainer.name,
                            t.Trainer.phone_number,
                            t.Trainer.photo_src,
                            t.Trainer.birthday
                        },
                    }));
        }

        [HttpGet]
        public IQueryable<Workout> WorkoutsNoDetail()
        {
            return _dm.Workout.GetAll().AsNoTracking();
        }

        //        [HttpGet]
        public string GetData()
        {
            return "This is a test string";
        }
    }
}
