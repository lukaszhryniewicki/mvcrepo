using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace FORUM_DYSKUSYJNE.Core.Models
{
	public class Group
	{
		public int GroupId { get; set; }
		[Required]
		public string GroupName { get; set; }
		public virtual ICollection<Section> Section { get; set; }
	}

}