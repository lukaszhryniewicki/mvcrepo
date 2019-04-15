using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FORUM_DYSKUSYJNE.Models
{

	public class PrivilegesView
	{
		
		public User User { get; set; }
		public IEnumerable<Section> Sections { get; set; }
		public ICollection <string> UserSections { get; set; }
		public List<SelectListItem> AvailableRoles = new List<SelectListItem>(new SelectListItem[]
				{
				new SelectListItem { Text = "Admin", Value = "Admin" },
				new SelectListItem { Text = "User", Value = "User" },
				});
		public List<String> SelectedRoles { get; set; }
		public IEnumerable<Rank> Ranks { get; set; }
		public int? SelectedRank { get; set; }
		public int UserId { get; set; }



	}
}