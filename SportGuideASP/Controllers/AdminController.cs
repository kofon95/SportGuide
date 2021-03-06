﻿using Dal;
using SportGuideASP.Core;
using SportGuideASP.Core.Util;
using SportGuideASP.Core.ViewModels;
using SportGuideASP.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static SportGuideASP.StaticData;
using static Utils.Consts;

namespace SportGuideASP.Controllers
{
    //[Authorize]
    [Authorize(Roles = "Moder,Admin")]
    public class AdminController : Controller
    {
        DataManager _dm = new DataManager();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region Add

        [HttpGet]
        public ActionResult AddHall()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddHall(AdminViewModel.HallAddUpdate hallvm)
        {
            if (!ModelState.IsValid)
            {
                Log.Error("ModelState is not valid. IP - " + Request.ServerVariables["REMOTE_ADDR"]);
                return View();
            }

            string[] fileNames;
            FileUploader fileUploader = new FileUploader(this);
            try
            {
                if (Request.Files[0].ContentLength > 0)
                    fileNames = fileUploader.SaveImagesWithResizingGetFileNames(
                        FilePaths.HallImageSource,
                        ContentLengths.Hall_MaxWidthImage,
                        ContentLengths.Hall_MaxHeightImage);
                else
                    fileNames = new string[0];
            }
            catch (BadImageFormatException e)
            {
                ModelState.AddModelError("", string.Format(Resource.ImageNotValid, e.FileName));
                return View();
            }

            try
            {
                Hall hall = new Hall
                {
                    hall_name = hallvm.Name,
                    address = hallvm.Address,
                    city_id = hallvm.CityId,
                    description = hallvm.Description,
                };

                hall = _dm.Hall.Save(hall);

                HallYandexMapLocation yaLocation = new HallYandexMapLocation { id = hall.id };
                if (GetMapLocation(yaLocation, hallvm.LocationLatitude, hallvm.LocationLongitude))
                    _dm.HallYandexMapLocation.Save(yaLocation);

                HallImages[] imgs = new HallImages[fileNames.Length];
                for (int i = 0; i < imgs.Length; i++)
                {
                    imgs[i] = new HallImages { hall_id = hall.id, src = fileNames[i], };
                }
                _dm.HallImages.Save(imgs);
            }
            catch (Exception e)
            {
                Log.Error(e, "Cannot added a new hall");
                fileUploader.DeleteFiles(fileNames);
                ModelState.AddModelError("", "Серверная ошибка");
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        private static bool GetMapLocation(HallYandexMapLocation yaLocation, string latitudeStr, string longitudeStr)
        {
            double longitude, latitude;
            if (double.TryParse(latitudeStr?.Replace(".", ","), out latitude) &&
                double.TryParse(longitudeStr?.Replace(".", ","), out longitude))
            {
                yaLocation.latitude = latitude;
                yaLocation.longitude = longitude;
                return true;
            }
            else
                return false;
        }

        [HttpGet]
        public ActionResult AddWorkout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddWorkout(AdminViewModel.WorkoutAddUpdate workout)
        {
            if (!ModelState.IsValid)
            {
                Log.Warn("ModelState is not valid " + Request.ServerVariables["REMOTE_ADDR"], workout);
                ModelState.AddModelError("", "ModelState is not valid");
                return View();
            }

            Workout addition = new Workout
            {
                hall_id = workout.HallId,
                trainer_id = workout.TrainerId,
                gender_of_athlete = workout.GenderOfAthlete,
                info = workout.Info,
                kind_of_sport_id = workout.KindOfSportId,
                min_age = workout.MinAge,
                max_age = workout.MaxAge,

                mon = workout.Mon,
                tue = workout.Thu,
                wed = workout.Wed,
                thu = workout.Thu,
                fri = workout.Fri,
                sat = workout.Sat,
                sun = workout.Sun,
            };
            var addedId = _dm.Workout.Save(addition).id;
            return RedirectToRoute("Workout", new { id = addedId });
        }

        [HttpGet]
        public ActionResult AddTrainer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTrainer(AdminViewModel.TrainerAddUpdate trainer)
        {
            if (!ModelState.IsValid)
            {
                Log.Warn("ModelState is not valid " + Request.ServerVariables["REMOTE_ADDR"], trainer);
                ModelState.AddModelError("", "ModelState is not valid");
                return View();
            }

            bool fileExists = Request.Files[0].ContentLength > 0;
            string images = null;
            FileUploader uploader = null;
            if (fileExists)
            {
                uploader = new FileUploader(this);
                images = uploader.SaveImagesWithResizingGetFileNames(FilePaths.TrainerImageSource, ContentLengths.Trainer_MaxWidthImage, ContentLengths.Trainer_MaxHeightImage)[0];
            }
            Trainer addition = new Trainer
            {
                name = trainer.Name,
                birthday = trainer.Birthday,
                phone_number = trainer.PhoneNumber,
                photo_src = images,
                register_date = DateTime.Now,
            };
            try
            {
                _dm.Trainer.Save(addition);
            }
            catch (Exception e)
            {
                Log.Error(e, "Could not save trainer", trainer);
                if (fileExists)
                    uploader.DeleteFiles(images);
                throw;
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Update

        [HttpGet]
        public ActionResult UpdateHall(int id, string returnUrl)
        {
            var h = _dm.Hall.GetAll().Include(t=>t.HallImages).First(t=>t.id ==id);
            var hallVM = new AdminViewModel.HallAddUpdate
            {
                Address=h.address,
                CityId=h.city_id,
                Description=h.description,
                Images= h.HallImages.Select(t=>t.src),
                Name=h.hall_name,
            };
            if (h.HallYandexMapLocation != null)
            {
                hallVM.LocationLatitude = h.HallYandexMapLocation.latitude.ToString().Replace(",", ".");
                hallVM.LocationLongitude = h.HallYandexMapLocation.longitude.ToString().Replace(",", ".");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(hallVM);
        }
        [HttpPost]
        public ActionResult UpdateHall(int id, AdminViewModel.HallAddUpdate hallVM, string returnUrl)
        {
            Hall hall = _dm.Hall.GetAll().Include(t => t.HallImages).First(t => t.id == id);
            hall.address = hallVM.Address;
            hall.city_id = hallVM.CityId;
            hall.description = hallVM.Description;
            hall.hall_name = hallVM.Name;

            if (hallVM.LocationLatitude != null && hallVM.LocationLongitude != null)
            {
                if (hall.HallYandexMapLocation == null)
                    hall.HallYandexMapLocation = new HallYandexMapLocation {id=id };
                GetMapLocation(hall.HallYandexMapLocation, hallVM.LocationLatitude, hallVM.LocationLongitude);
            }
            _dm.Hall.Update(hall);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateWorkout(int id, string returnUrl)
        {
            var w = _dm.Workout.GetById(id);
            var workoutVM = new AdminViewModel.WorkoutAddUpdate
            {
                Info = w.info,
                HallId = w.hall_id,
                GenderOfAthlete = w.gender_of_athlete,
                KindOfSportId = w.kind_of_sport_id,
                PaymentForMonth = w.paiment_for_month,
                TrainerId = w.trainer_id,
                MaxAge = w.max_age,
                MinAge = w.min_age,
                Time = w.time,
                Mon = w.mon,
                Tue = w.tue,
                Wed = w.wed,
                Thu = w.thu,
                Fri = w.fri,
                Sat = w.sat,
                Sun = w.sun,
            };
            ViewBag.ReturnUrl = returnUrl;
            return View(workoutVM);
        }
        [HttpPost]
        public ActionResult UpdateWorkout(int id, AdminViewModel.WorkoutAddUpdate workoutVM, string returnUrl)
        {
            var w = workoutVM;
            Workout workout = _dm.Workout.GetById(id);
            workout.info = w.Info;
            workout.hall_id = w.HallId;
            workout.gender_of_athlete = w.GenderOfAthlete;
            workout.kind_of_sport_id = w.KindOfSportId;
            workout.paiment_for_month = w.PaymentForMonth;
            workout.trainer_id = w.TrainerId;
            workout.max_age = w.MaxAge;
            workout.min_age = w.MinAge;
            workout.time = w.Time;
            workout.mon = w.Mon;
            workout.tue = w.Tue;
            workout.wed = w.Wed;
            workout.thu = w.Thu;
            workout.fri = w.Fri;
            workout.sat = w.Sat;
            workout.sun = w.Sun;

            _dm.Workout.Update(workout);
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult UpdateTrainer(int id, string returnUrl)
        {
            var trainer = _dm.Trainer.GetById(id);
            var trainerVM = new AdminViewModel.TrainerAddUpdate
            {
                Name = trainer.name,
                Birthday = trainer.birthday,
                PhoneNumber = trainer.phone_number,
            };

            ViewBag.ReturnUrl = returnUrl;
            return View(trainerVM);
        }
        public ActionResult UpdateTrainer(int id, AdminViewModel.TrainerAddUpdate trainer, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                Log.Warn("ModelState is not valid " + Request.ServerVariables["REMOTE_ADDR"], trainer);
                ModelState.AddModelError("", "ModelState is not valid");
                return View();
            }

            var oldTrainer = _dm.Trainer.GetById(id);
            oldTrainer.name = trainer.Name;
            oldTrainer.phone_number = trainer.PhoneNumber;
            oldTrainer.birthday = trainer.Birthday;
            _dm.Trainer.Update(oldTrainer);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        public ActionResult DeleteHall(int id)
        {
            var hall = _dm.Hall.GetAll().AsNoTracking()
                .Include(t => t.HallImages)
                .Include(t => t.Workout)
                .First(t => t.id == id);

            _dm.Workout.Delete(hall.Workout);
            _dm.HallImages.Delete(hall.HallImages);
            new FileUploader(this)
                .DeleteFiles(hall.HallImages.Select(t => t.src).ToArray());
            _dm.Hall.Delete(hall);

            return Redirect(Request.UrlReferrer?.AbsoluteUri ?? "/");
        }
        public ActionResult DeleteWorkout(int id)
        {
            _dm.Workout.DeleteById(id);
            return Redirect(Request.UrlReferrer?.AbsoluteUri ?? "/");
        }

        public ActionResult DeleteTrainer(int id)
        {
            var trainer = _dm.Trainer.GetAll().AsNoTracking()
                .Include(t => t.Workout)
                .Where(t=>t.id == id)
                .First();
            
            _dm.Workout.Delete(trainer.Workout);
            _dm.Trainer.Delete(trainer);
            if (System.IO.File.Exists(FilePaths.TrainerImageSource + trainer.photo_src))
                System.IO.File.Delete(FilePaths.TrainerImageSource + trainer.photo_src);
            return Redirect(Request.UrlReferrer?.AbsoluteUri ?? "/");
        }

        #endregion
        

        [HttpGet]
        public ActionResult Multi()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Trainers()
        {
            return View();
        }
    }
}