using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Models
{

	public class EditViewModel
	{
		public Post Post { get; set; }
		public List<string> ForbiddenWords { get; set; }
	}
}