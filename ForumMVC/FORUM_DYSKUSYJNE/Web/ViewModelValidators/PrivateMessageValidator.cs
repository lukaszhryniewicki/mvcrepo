using FluentValidation;
using FORUM_DYSKUSYJNE.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Web.ViewModelValidators
{
	public class PrivateMessageValidator:AbstractValidator<MessageView>
	{
		public PrivateMessageValidator()
		{
			RuleFor(x => x.MessageContent)
				.NotNull()
				.WithMessage("Content required");
			RuleFor(x => x.MessageTitle)
				.NotNull()
				.WithMessage("Title required");
			RuleFor(x => x.Username)
				.NotNull()
				.WithMessage("Receiver required");
		}
	}
}