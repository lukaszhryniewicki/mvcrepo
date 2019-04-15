using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Models
{

	public class PostView
	{
		public Topic Topic { get; set; }
		[Display(Name ="Post")]
		public Post Post { get; set; }
		[Required(ErrorMessage ="Content required")]
		public string Content { get; set; }
		public int TopicId { get; set; }
		public bool isMod { get; set; }
		public List<string> ForbiddenWords { get; set; }

	}
}