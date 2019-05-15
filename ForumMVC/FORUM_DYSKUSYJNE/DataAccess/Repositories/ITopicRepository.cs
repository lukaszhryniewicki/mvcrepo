using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.DataAccess.Repositories
{		
	public interface ITopicRepository : IRepository<Topic>
	{
		int? GetTopicsSection(int id);
		IEnumerable<Topic> GetTopicsWithUser(int sectionId);


	}
}