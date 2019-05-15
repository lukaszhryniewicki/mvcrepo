using FluentValidation;
using FORUM_DYSKUSYJNE.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Web.ViewModelValidators
{
	public class ChangePasswordValidator:AbstractValidator<ChangePasswordViewModel>
	{
		public ChangePasswordValidator()
		{

			RuleFor(x => x.Password)
				.NotNull()
				.WithMessage("Password required.");
			RuleFor(x => x.ConfirmPassword)
				.NotNull()
				.WithMessage("Comfirm password required.")
				.Equal(x => x.Password)
				.WithMessage("Passwords must match.");
	}
	}
}