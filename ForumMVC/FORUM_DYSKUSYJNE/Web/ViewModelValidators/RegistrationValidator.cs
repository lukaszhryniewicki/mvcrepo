using FluentValidation;
using FORUM_DYSKUSYJNE.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Web.ViewModelValidators
{
	public class RegistrationValidator:AbstractValidator<RegistrationView>
	{
		public RegistrationValidator()
		{
			RuleFor(x => x.Username)
				.NotNull()
				.WithMessage("Username required.")
				.MaximumLength(10)
				.WithMessage("Username must not be longer than 10 characters.");

			RuleFor(x => x.Name)
				.NotNull()
				.WithMessage("First name required.")
				.MaximumLength(10)
				.WithMessage("First name must not be longer than 10 characters.");

			RuleFor(x => x.Email)
				.NotNull()
				.WithMessage("Email required.")
				.EmailAddress()
				.WithMessage("Insert a valid email adress.");

			RuleFor(x => x.Location)
			.NotNull()
			.WithMessage("Location required.")
			.MaximumLength(15)
			.WithMessage("First name must not be longer than 15 characters.");


			RuleFor(x => x.Password)
			.NotNull()
			.WithMessage("Password required.")
			.MinimumLength(6)
			.WithMessage("Password must be longer than 5 characters.");

			RuleFor(x => x.ConfirmPassword)
				.NotNull()
				.WithMessage("Confirm your password.")
				.Equal(x => x.Password)
				.WithMessage("Passwords must match.");


	}
	}
}