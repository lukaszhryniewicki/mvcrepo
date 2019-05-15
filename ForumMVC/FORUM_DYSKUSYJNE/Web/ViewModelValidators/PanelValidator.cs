using FluentValidation;
using FORUM_DYSKUSYJNE.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Web.ViewModelValidators
{
	public class PanelValidator:AbstractValidator<PanelView>
	{
		public PanelValidator()
		{
			RuleFor(x => x.Name)
				.NotNull()
				.WithMessage("Name required.")
				.MaximumLength(15)
				.WithMessage("Name must be less than 15 characters.");

			RuleFor(x => x.Description)
				.NotNull()
				.WithMessage("Description required.")
				.MaximumLength(140).
				WithMessage("Description must be less than 140 characters.");

			RuleFor(x => x.SelectedGroup)
				.NotNull()
				.WithMessage("Group required.");



	}
	}
}