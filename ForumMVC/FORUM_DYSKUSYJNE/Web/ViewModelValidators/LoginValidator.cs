using FluentValidation;
using FORUM_DYSKUSYJNE.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FORUM_DYSKUSYJNE.Web.ViewModelValidators
{
	public class LoginValidator:AbstractValidator<LoginView>
	{
		public LoginValidator()
		{
			RuleFor(x => x.Username)
				.NotNull()
				.WithMessage("Username required.");

			RuleFor(x => x.Password)
				.NotNull()
				.WithMessage("Password required.");


		}
	}
}