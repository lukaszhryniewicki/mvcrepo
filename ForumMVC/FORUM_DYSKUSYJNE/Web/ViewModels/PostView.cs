using FluentValidation.Attributes;
using FORUM_DYSKUSYJNE.Core.Models;
using FORUM_DYSKUSYJNE.Web.ViewModelValidators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FORUM_DYSKUSYJNE.Web.ViewModels
{
	[Validator(typeof(PostValidator))]
	public class PostView
	{
		public Topic Topic { get; set; }
		public HttpPostedFileBase UserAttachment { get; set; }
		public string Content { get; set; }
		public int TopicId { get; set; }
		public bool isMod { get; set; }
		public IEnumerable<string> ForbiddenWords { get; set; }

		public PostView()
		{

		}
		public PostView(bool isUserMod, Topic topic, IEnumerable<string> forbiddenWords)
		{
			isMod = isUserMod;
			Topic = topic;
			TopicId = topic.TopicId;
			ForbiddenWords = forbiddenWords;

		}

		public PostView(bool isUserMod, Topic topic)
		{
			ForbiddenWords = new List<string>();
			isMod = isUserMod;
			Topic = topic;
			TopicId = topic.TopicId;
		}
	}
}