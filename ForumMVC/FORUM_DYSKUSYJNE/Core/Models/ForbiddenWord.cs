using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Core.Models
{
	public class ForbiddenWord
	{
		public int ForbiddenWordId { get; set; }
		[Required]
		public string Word { get; set; }
	}

}