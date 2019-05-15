using FluentValidation.Attributes;
using FORUM_DYSKUSYJNE.Web.ViewModelValidators;
using System;
using System.ComponentModel.DataAnnotations;

namespace FORUM_DYSKUSYJNE.Web.ViewModels
{
	[Validator(typeof(RegistrationValidator))]
	public class RegistrationView
    {

        [Display(Name = "User Name")]
        public string Username { get; set; }


        [Display(Name = "First Name")]
		public string Name { get; set; }


        public string Email { get; set; }


		public string Location { get; set; }


        [Display(Name = "Birthday date")]
        public DateTime BirthdayDate { get; set; }



        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}