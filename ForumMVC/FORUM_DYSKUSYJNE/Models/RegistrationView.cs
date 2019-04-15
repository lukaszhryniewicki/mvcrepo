using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Models
{

    public class RegistrationView
    {
        [Required(ErrorMessage = "User Name required")]
        [Display(Name = "User Name")]
		[MaxLength(10)]
        public string Username { get; set; }

        [Required(ErrorMessage = "First Name required")]
        [Display(Name = "First Name")]
		[MaxLength(10)]
		public string Name { get; set; }

        [Required(ErrorMessage = "Email required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Location required")]
        [Display(Name = "Location")]
		[MaxLength(15)]
		public string Location { get; set; }

        [Required(ErrorMessage = "Birthday date required")]
        [Display(Name = "Birthday date")]
        public DateTime BirthdayDate { get; set; }

        [Required]
        public Guid ActivationCode { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Error : Confirm password does not match with password")]
        public string ConfirmPassword { get; set; }
    }
}