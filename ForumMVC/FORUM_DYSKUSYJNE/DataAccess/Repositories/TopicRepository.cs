using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FORUM_DYSKUSYJNE.Core.Models;
using FORUM_DYSKUSYJNE.Infrastructure.Data;

namespace FORUM_DYSKUSYJNE.DataAccess.Repositories
{

	public class TopicRepository : Repository<Topic> , ITopicRepository
	{
		public TopicRepository(ForumDatabase context) : base(context) { }

		public Topic GetTopicById(int id)
		{
			var topic = context.Topics.Where(x => x.TopicId == id).FirstOrDefault();
			return topic;
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