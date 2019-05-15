using Ext.FluentValidation;
using FORUM_DYSKUSYJNE.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Web.ViewModelValidators
{
	public class ForbiddenWordsValidator:AbstractValidator<ForbiddenWordViewModel>
	{
		public ForbiddenWordsValidator()
		{
			RuleFor(x => x.word)
				.NotNull()
				.WithMessage("Word required.");

			
		}
	}
}