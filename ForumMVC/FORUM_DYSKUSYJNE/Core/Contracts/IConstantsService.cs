using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORUM_DYSKUSYJNE.Core.Contracts
{
	public interface IConstantsService
	{
		IEnumerable<Rank> GetAllRanks();
		Rank GetRankById(int selectedRank);
		IEnumerable<Role> GetAllRoles();
	}
}
