using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Models
{

	public class ChangePasswordViewModel
	{
		public string Code { get; set; }
		[Required(ErrorMessage = "Password required")]
		[DataType(DataType.Password)]
		[Display(Name = " New Password")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password required")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm New Password")]
		[Compare("Password", ErrorMessage = "Error : Confirm password does not match with password")]
		public string ConfirmPassword { get; set; }
	}
}