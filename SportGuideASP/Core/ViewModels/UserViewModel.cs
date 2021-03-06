﻿
using SportGuideASP.Properties;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;

namespace SportGuideASP.Core.ViewModels
{
    public class UserViewModel
    {
        [MetadataType(typeof(ValidationLogin))]
        public class SignIn
        {
            //public int Id { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }
        }
        [MetadataType(typeof(ValidationLogin))]
        public class Register
        {
            public string Email { get; set; }
            public string Password { get; set; }

            [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
            public string Name { get; set; }
        }

        public class UserVK
        {
            public int uid { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string gender { get; set; }
            public string photo_scr { get; set; }
            public DateTime? bdate { get; set; }
            //public string href { get; set; }
        }

        private class ValidationLogin
        {
            [Display(Name = "Email")]
            [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]

            // comment this one attribute that to send to server
            //[EmailAddress(ErrorMessageResourceName = "WrongEmail", ErrorMessageResourceType = typeof(Resource))]
            [EmailAddress(ErrorMessage = "Неверный Email")]
            public string Email { get; set; }

            [Display(Name = "Пароль")]
            [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
            [StringLength(maximumLength: 32, MinimumLength = 6, ErrorMessageResourceName = "WrongPassword", ErrorMessageResourceType = typeof(Resource))]
            public string Password { get; set; }
        }
    }
}