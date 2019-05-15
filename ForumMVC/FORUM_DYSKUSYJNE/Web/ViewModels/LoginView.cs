using FluentValidation.Attributes;
using FORUM_DYSKUSYJNE.Web.ViewModelValidators;
using System.ComponentModel.DataAnnotations;

namespace FORUM_DYSKUSYJNE.Web.ViewModels
{
	[Validator(typeof(LoginValidator))]
	public class LoginView
    {

		[Display(Name = "User Name")]
        public string Username { get; set; }

		[DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}