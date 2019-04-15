using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace FORUM_DYSKUSYJNE.DataAccess.Repositories
{
	
	public class SectionRepository : Repository<Section>, ISectionRepository
	{
		public SectionRepository(ForumDatabase context) : base(context) { }

		public string GetSelectedSectionName(Section edit)
		{
			return context.Sections.Where(g => g.GroupId == edit.GroupId && g.Order > edit.Order)
				.OrderBy(o => o.Order).Select(s => s.Name).FirstOrDefault();
		}

		public int GetTopOrder()
		{
			return context.Sections.OrderByDescending(o => o.Order).Select(o => o.Order).First();
		}

		public IEnumerable<User> GetSectionModerators(int id)
		{

			return context.Sections.Where(s => s.SectionId == id).Select(u => u.User).FirstOrDefault().ToList();
		}

		public IEnumerable<Section> OrderBy<T>(Expression<Func<Section, T>> order)
		{
			return context.Sections.OrderBy(order).ToList();
		}

		public IEnumerable<Section> GetOrderedSections(int id)
		{
			return context.Sections.Where(s => s.GroupId == id).OrderBy(o => o.Order).ToList();
		}

	}
}