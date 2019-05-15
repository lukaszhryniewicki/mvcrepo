using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Core.Models
{

	public class Section
	{

		public int SectionId { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		public string IconName { get; set; }
		public int GroupId { get; set; }
		public int Order { get; set; }
		public Group Group { get; set; }
		public virtual ICollection<Topic> Topic { get; set; }
		public virtual ICollection<User> User { get; set; }

		
	}
}