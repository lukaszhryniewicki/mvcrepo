using Ext.FluentValidation.Attributes;
using FORUM_DYSKUSYJNE.Core.Models;
using FORUM_DYSKUSYJNE.Web.ViewModelValidators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FORUM_DYSKUSYJNE.Web.ViewModels
{
	[Validator(typeof(PanelValidator))]
	public class PanelView
	{
		

		public PanelView(IEnumerable<Group> groups)
		{
			Groups = groups;
		}

		public PanelView(Section edit, IEnumerable<Group> groups, IEnumerable<Section> sections, string name)
		{

			Name = edit.Name;
			Description = edit.Description;
			Groups = groups;
			SelectedGroup = edit.Group.GroupName;
			SelectedSection = name;
			editedSectionId = edit.SectionId;
			Sections = sections;
		}
		public PanelView()
		{

		}

		public IEnumerable<Group> Groups { get; set; }
		public IEnumerable<Section> Sections { get; set; }


		public string Name { get; set; }


		public string Description { get; set; }


		[Display(Name = "Add before")]
		public string SelectedSection { get; set; }


		[Display(Name ="Group")]
		public string SelectedGroup { get; set; }


        public int editedSectionId { get; set; }
	}
}