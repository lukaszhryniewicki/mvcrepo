using FORUM_DYSKUSYJNE.Core.Models;
using System.Collections.Generic;

namespace FORUM_DYSKUSYJNE.Web.ViewModels
{

	public class SectionView
	{

		public bool isMod { get; set; }
		public IEnumerable<Topic> Topics { get; set; }
		public string SearchTerm { get; set; }
		public int SectionId { get; set; }
		public string SectionName { get; set; }

		public SectionView(IEnumerable<Topic> topics, int id, bool isModx, string sectionName)
		{
			Topics = topics;
			SectionId = id;
			isMod = isModx;
			SectionName = sectionName;
		}

		public SectionView(IEnumerable<Topic> topics, int id, bool isModx, string sectionName, string query)
		{
			Topics = topics;
			SectionId = id;
			isMod = isModx;
			SectionName = sectionName;
			SearchTerm = query;
		}
		public SectionView()
		{

		}
	}
}