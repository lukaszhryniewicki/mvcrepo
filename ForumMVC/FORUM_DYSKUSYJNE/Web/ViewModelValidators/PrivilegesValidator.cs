using FluentValidation;
using FORUM_DYSKUSYJNE.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Web.ViewModelValidators
{
	public class PrivilegesValidator:AbstractValidator<PrivilegesView>
	{
		public PrivilegesValidator()
		{
			RuleFor(x => x.SelectedRoles)
				.NotNull()
				.WithMessage("At least one role must be chosen");
		}
	}
}