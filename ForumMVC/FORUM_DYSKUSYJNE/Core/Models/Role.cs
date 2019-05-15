using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Core.Models
{
	public class Role
	{

		public int RoleId { get; set; }
		[Required]
		public string RoleName { get; set; }
		public virtual ICollection<User> User { get; set; }
	}
}