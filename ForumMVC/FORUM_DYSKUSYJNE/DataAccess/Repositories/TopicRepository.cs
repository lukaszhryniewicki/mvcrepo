using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace FORUM_DYSKUSYJNE.DataAccess.Repositories
{

	public class TopicRepository : Repository<Topic> , ITopicRepository
	{
		public TopicRepository(ForumDatabase context) : base(context) { }

		public Topic GetTopicById(int id)
		{
			throw new NotImplementedException();
		}

		public int? GetTopicsSection(int id)
		{
			return context.Topics.Where(t => t.TopicId == id).FirstOrDefault().SectionId;
		}

		public IEnumerable<Topic> GetTopicsWithUser(int sectionId)
		{
			return context.Topics.Include(u => u.User).Where(i => i.SectionId == sectionId).ToList();
		}
	}
}