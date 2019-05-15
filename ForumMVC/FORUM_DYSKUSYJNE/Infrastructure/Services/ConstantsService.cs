using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Infrastructure.Services
{
	public class ConstantsService : IConstantsService
	{
		private readonly IDataService _dataService;

		public ConstantsService( IDataService dataService)
		{
			_dataService = dataService;
		}
		public IEnumerable<Rank> GetAllRanks()
		{
			return _dataService.GetDbSet<Rank>().ToList();
		}

		public IEnumerable<Role> GetAllRoles()
		{
			return _dataService.GetDbSet<Role>().ToList();
		}

		public Rank GetRankById(int selectedRank)
		{
			return _dataService.GetDbSet<Rank>()
				.Where(x => x.Id == selectedRank)
				.SingleOrDefault();
		}

	}
}