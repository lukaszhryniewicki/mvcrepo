using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Models
{

	public class ForbiddenWordViewModel
	{
		public IEnumerable<ForbiddenWord> forbiddenWords { get; set; }
		[Required]
		public string word { get; set; }
	}
}