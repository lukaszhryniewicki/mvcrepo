using FORUM_DYSKUSYJNE.Contracts;
using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Services
{
	public class TopicsServices : ITopicsService
	{
		private readonly IUnitOfWork _unitOfWork;
		public TopicsServices(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public Topic GetTopicById(int id)
		{
			var topic = _unitOfWork.Topics.Get(id);
			return topic;
		}
	}
}