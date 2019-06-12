using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Core.Models
{
	public class Emoticon
	{
		public int EmoticonId { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string SourceLink { get; set; }
	}

}