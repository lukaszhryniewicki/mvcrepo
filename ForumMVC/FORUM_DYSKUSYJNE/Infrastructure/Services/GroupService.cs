using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Infrastructure.Services
{
	public class GroupService : IGroupsService
	{
		private readonly IDataService _dataService;

		public GroupService(IDataService dataService)
		{
			_dataService = dataService;
		}

		public IEnumerable<Group> GetAllGroups()
		{
			return _dataService.GetDbSet<Group>().ToList();
		}

		public Group GetGroupById(int id)
		{
			return _dataService.GetDbSet<Group>()
				.Where(x=>x.GroupId == id)
				.SingleOrDefault();
		}
	}
}