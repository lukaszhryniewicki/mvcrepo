using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Models
{

	public class SectionView
	{
		public bool isMod { get; set; }
		public IEnumerable<Topic> Topics { get; set; }
		public string SearchTerm { get; set; }
		public int SectionId { get; set; }
		public string SectionName { get; set; }
	}
}