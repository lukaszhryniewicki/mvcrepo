
using FluentValidation.Attributes;
using FORUM_DYSKUSYJNE.Web.ViewModelValidators;
using System.ComponentModel.DataAnnotations;


namespace FORUM_DYSKUSYJNE.Web.ViewModels
{
	[Validator(typeof(ChangePasswordValidator))]
	public class ChangePasswordViewModel
	{
		public string Code { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = " New Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm New Password")]
		public string ConfirmPassword { get; set; }
	}
}