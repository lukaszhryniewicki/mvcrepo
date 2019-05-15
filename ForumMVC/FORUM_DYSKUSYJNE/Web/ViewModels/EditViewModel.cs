using FORUM_DYSKUSYJNE.Core.Models;
using System.Collections.Generic;

namespace FORUM_DYSKUSYJNE.Web.ViewModels
{

	public class EditViewModel
	{
		public Post Post { get; set; }
		public IEnumerable<string> ForbiddenWords { get; set; }

		public EditViewModel(Post post)
		{
			Post = post;
		}
		public EditViewModel()
		{

		}
	}
}