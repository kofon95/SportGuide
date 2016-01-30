using System;
using System.Collections.Generic;

namespace SportGuideASP.Core.ViewModels
{
    public class SearchViewModel
    {
        public class Index
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
            public string KindOfSport { get; set; }
            public TimeSpan? TimeForWorkout { get; set; }
            public string OtherInformation { get; set; }
            public string GenderOfAthlete { get; set; }
        }

        public class Workout
        {
            public int Id { get; set; }
            public string Info { get; set; }
            public TimeSpan? TimeForWorkout { get; set; }
            public bool DateMonday { get; set; }
            public bool DateTuesday { get; set; }
            public bool DateWednesday { get; set; }
            public bool DateThursday { get; set; }
            public bool DateFriday { get; set; }
            public bool DateSaturday { get; set; }
            public bool DateSunday { get; set; }

            public int MinAge { get; set; }
            public int MaxAge { get; set; }

            public string GenderOfAthlete { get; set; }

            public int KindOfSportId { get; set; }
            public string KindOfSport { get; set; }
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }

            public int? TrainerId { get; set; }
            public string TrainerName { get; set; }
            public string TrainerImageSource { get; set; }
            public string TrainersPhoneNumber { get; set; }

            public int HallId { get; set; }
            public string HallName { get; set; }
            //public string HallImage { get; set; }
            public IEnumerable<string> HallImages { get; set; }

            public string Address { get; set; }
            public double? Longitude { get; set; }
            public double? Latitude { get; set; }
        }

        public class Hall
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Description { get; set; }
            public string CityName { get; set; }

            public IEnumerable<string> PhoneNumbers { get; set; }
            public IEnumerable<string> Images { get; set; }
            public IEnumerable<Workout> Workouts { get; set; }

            public Dal.HallYandexMapLocation MapLocation{ get; set; }
        }

        public class Trainer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Birthday { get; set; }
            public string PhotoSrc { get; set; }
            public string PhoneNumber { get; set; }
            public IEnumerable<Workout> Workouts { get; set; }
        }
        public class GetHalls
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Description { get; set; }

            public int CityId { get; set; }
            public string CityName { get; set; }

            public IEnumerable<string> HallImages { get; set; }
            public double? LocationLatitude { get; set; }
            public double? LocationLongitude { get; set; }
        }
    }
}