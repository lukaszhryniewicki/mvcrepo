using FluentValidation;
using FORUM_DYSKUSYJNE.Web.ViewModels;


namespace FORUM_DYSKUSYJNE.Web.ViewModelValidators
{
	public class PostValidator:AbstractValidator<PostView>
	{
		public PostValidator()
		{
			RuleFor(x => x.Content)
				.NotNull()
				.WithMessage("Post content is required.")
				.MaximumLength(255)
				.WithMessage("Post contains more than 255 characters.");

			int size = 1024*100;

			RuleFor(x => x.UserAttachment.ContentLength)
				.LessThan(size)
				.WithMessage($"Post attachment size must be less than {size/100} kB")
				.When(x=>x.UserAttachment != null);
		}
	}
}