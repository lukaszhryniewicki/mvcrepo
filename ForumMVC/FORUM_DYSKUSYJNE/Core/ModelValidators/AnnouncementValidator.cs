using FluentValidation;
using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Core.ModelValidators
{
	public class AnnouncementValidator:AbstractValidator<Announcement>
	{
		public AnnouncementValidator()
		{
			RuleFor(x => x.Content)
				.NotNull()
				.WithMessage("Content required")
				.MaximumLength(30)
				.WithMessage("Announcement too long");

			RuleFor(x => x.ExpirationDate)
				.NotNull()
				.WithMessage("Expiration date is required")
				.Must(x => x.Date >= DateTime.Now && x.Date < DateTime.MaxValue )
				.WithMessage("Expiration date is invalid");
		}
	}
}