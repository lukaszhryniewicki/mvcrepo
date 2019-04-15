using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Models
{

	public class PanelView
	{
		public IEnumerable<Group> Groups { get; set; }
		[Required(ErrorMessage ="Name required")]
		[MaxLength(15)]
		public string Name { get; set; }
		[Required(ErrorMessage = "Description required")]
		[MaxLength(140)]
		public string Description { get; set; }
		[Display(Name = "Add before")]
		[Required]
		public string SelectedSection { get; set; }
		[Required]
		[Display(Name ="Group")]
		public string SelectedGroup { get; set; }
        public int editedSectionId { get; set; }
	}
}