using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Web.ViewModels
{
	public class EditForumViewModel
	{
		public IEnumerable<Group> Groups;
		public IEnumerable<Section> Sections;

		public EditForumViewModel(IEnumerable<Group> groups, IEnumerable<Section> sections)
		{
			Groups = groups;
			Sections = sections;
		}
		public EditForumViewModel()
		{

		}
	}
}