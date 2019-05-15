using FORUM_DYSKUSYJNE.Core.Models;
using System.Collections.Generic;

namespace FORUM_DYSKUSYJNE.Web.ViewModels
{

	public class HomeView
	{
		public IEnumerable<Group> Groups;
		public IEnumerable<Section> Sections;
		public IEnumerable<Announcement> Announcements;
		public string RegisteredUsersInfo { get; set; }

		public HomeView(IEnumerable<Group> groups, IEnumerable<Section> sections, IEnumerable<Announcement> announcements, string registeredUsersInfo)
		{
			Groups = groups;
			Sections = sections;
			Announcements = announcements;
			RegisteredUsersInfo = registeredUsersInfo;
		}

		public HomeView()
		{

		}

	}
}