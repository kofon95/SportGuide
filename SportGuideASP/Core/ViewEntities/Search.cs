using System;

namespace SportGuideASP.Core.ViewEntities
{
    public class Search
    {
        public class Index
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
            public string KindOfSport { get; set; }
            public TimeSpan? TimeForWorkout { get; set; }
            public string OtherInformation { get; set; }
        }
        public class Workout
        {
            public int Id { get; set; }
            public TimeSpan? TimeForWorkout { get; set; }
            public bool DateMonday { get; set; }
            public bool DateTuesday { get; set; }
            public bool DateWednesday { get; set; }
            public bool DateThursday { get; set; }
            public bool DateFriday { get; set; }
            public bool DateSaturday { get; set; }
            public bool DateSunday { get; set; }

            public string KindOfSport { get; set; }
            public string CategoryName { get; set; }

            public string TrainerName { get; set; }
            public string TrainerImageSource { get; set; }
            public string TrainersPhoneNumber { get; set; }
            
            public string HallName { get; set; }

            public string Address { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }
    }
}