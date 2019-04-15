using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace FORUM_DYSKUSYJNE.DataAccess.Repositories
{
		
		public interface ISectionRepository : IRepository<Section>
		{
			IEnumerable<Section> OrderBy<T>(Expression<Func<Section,T>> order);
			IEnumerable<User> GetSectionModerators(int id);
			int GetTopOrder();
			IEnumerable<Section> GetOrderedSections(int id);
			string GetSelectedSectionName(Section edit);
		}
	
}