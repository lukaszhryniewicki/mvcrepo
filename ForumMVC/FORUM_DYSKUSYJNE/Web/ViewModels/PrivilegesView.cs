using FluentValidation.Attributes;
using FORUM_DYSKUSYJNE.Core.Models;
using FORUM_DYSKUSYJNE.Web.ViewModelValidators;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FORUM_DYSKUSYJNE.Web.ViewModels
{
	[Validator(typeof(PrivilegesValidator))]
	public class PrivilegesView
	{
		
		public User User { get; set; }
		public IEnumerable<Section> Sections { get; set; }
		public IEnumerable <string> UserSections { get; set; }
		public List<SelectListItem> AvailableRoles = new List<SelectListItem>(new SelectListItem[]
				{
				new SelectListItem { Text = "Admin", Value = "Admin" },
				new SelectListItem { Text = "User", Value = "User" },
				});

		public PrivilegesView(User user, IEnumerable<Section> sections, IEnumerable<Rank> ranks)
		{
			User = user;
			Sections = sections;
			Ranks = ranks;
			UserSections = new List<string>();
			SelectedRank = user.RankId;
		}

		public PrivilegesView(User user, IEnumerable<Section>  sections, IEnumerable<string> userSections)
		{
			User = user;
			Sections = sections;
			UserSections = userSections;
		}
		public PrivilegesView()
		{

		}

		public IEnumerable<String> SelectedRoles { get; set; }
		public IEnumerable<Rank> Ranks { get; set; }
		public int? SelectedRank { get; set; }
		public int UserId { get; set; }



	}
}