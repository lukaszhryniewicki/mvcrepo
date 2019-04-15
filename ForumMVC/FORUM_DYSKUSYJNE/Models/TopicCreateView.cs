using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Models
{

	public class TopicCreateView
	{
		[Required(ErrorMessage ="Name required")]
		[Display(Name ="Name")]
		public string TopicName { get; set; }
		[Required(ErrorMessage ="Post required")]
		[Display(Name ="Post")]
		public string PostContent { get; set; }
		public int SectionId { get; set; }
		public List<string> ForbiddenWords { get; set; }


	}
}