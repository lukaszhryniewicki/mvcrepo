using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Core.Models
{
	public class Rank
	{
		public int Id { get; set; }
		[Required]
		public int Requirement { get; set; }
		[Required]
		public string RankName { get; set; }
	}


}