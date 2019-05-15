using FluentValidation.Attributes;
using FORUM_DYSKUSYJNE.Web.ViewModelValidators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FORUM_DYSKUSYJNE.Web.ViewModels
{
	[Validator(typeof(TopicCreateValidator))]
	public class TopicCreateView
	{

		[Display(Name ="Name")]
		public string TopicName { get; set; }

		[Display(Name ="Post")]
		public string PostContent { get; set; }

		public int SectionId { get; set; }
		public IEnumerable<string> ForbiddenWords { get; set; }
		public HttpPostedFileBase UserAttachment { get; set; }

	}
}