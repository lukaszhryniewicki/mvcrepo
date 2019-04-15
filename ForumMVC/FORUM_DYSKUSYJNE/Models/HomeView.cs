using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Models
{

	public class HomeView
	{
		public IEnumerable<Group> Groups;
		public IEnumerable<Section> Sections;
		public IEnumerable<Announcement> Announcements;
		public string RegisteredUsersInfo { get; set; }

	}
}