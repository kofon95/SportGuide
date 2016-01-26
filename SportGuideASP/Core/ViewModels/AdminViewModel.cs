using SportGuideASP.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportGuideASP.Core.ViewModels
{
    public class AdminViewModel
    {
        public class Hall
        {
            [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
            [StringLength(20, MinimumLength =2, ErrorMessageResourceName =nameof(Resource.MinimumSymbols), ErrorMessageResourceType =typeof(Resource))]
            public string Name { get; set; }

            [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType =typeof(Resource))]
            public string Address { get; set; }
            public int CityId { get; set; }
            public string Description { get; set; }
            
            public IEnumerable<string> Images { get; set; }
        }

        public class Workout
        {
            public int Id;
            [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
            [StringLength(20, MinimumLength = 5, ErrorMessageResourceName = nameof(Resource.MinimumSymbols), ErrorMessageResourceType = typeof(Resource))]
            public string Info { get; set; }
            [Required(ErrorMessageResourceName = nameof(Resource.ChooseSomething), ErrorMessageResourceType = typeof(Resource))]
            public int KindOfSportId { get; set; }
            [Required(ErrorMessageResourceName = nameof(Resource.ChooseSomething), ErrorMessageResourceType = typeof(Resource))]
            public int HallId { get; set; }
            [Required(ErrorMessageResourceName = nameof(Resource.ChooseSomething), ErrorMessageResourceType = typeof(Resource))]
            public int TrainerId { get; set; }
            [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
            public int PaymentForMonth { get; set; }

            [Required(ErrorMessageResourceName = nameof(Resource.RequiredNumber), ErrorMessageResourceType = typeof(Resource))]
            public short MinAge { get; set; }
            [Required(ErrorMessageResourceName = nameof(Resource.RequiredNumber), ErrorMessageResourceType = typeof(Resource))]
            public short MaxAge { get; set; }
            //[Required(ErrorMessage ="Wrong time")]
            public TimeSpan? Time { get; set; }

            public bool Mon { get; set; }
            public bool Tue { get; set; }
            public bool Wed { get; set; }
            public bool Thu { get; set; }
            public bool Fri { get; set; }
            public bool Sat { get; set; }
            public bool Sun { get; set; }
        }

        public class Trainer
        {
            public int Id { get; set; }
            [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
            [StringLength(20, MinimumLength = 3, ErrorMessageResourceName = nameof(Resource.MinimumSymbols), ErrorMessageResourceType = typeof(Resource))]
            public string Name { get; set; }
            [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
            public DateTime Birthday { get; set; }
            //public string PhotoSrc { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}