using FORUM_DYSKUSYJNE.Core.Models;
using System.Collections.Generic;

namespace FORUM_DYSKUSYJNE.Core.Contracts
{
	public interface IUsersService
	{
		User GetUserWithRank(int id);
		User GetUserByEmail(string email);
		User GetUserByCode(string code);
		User GetUserById(int id);
		string GetAllUsersNumber();
		bool IsUserMod(int? id, User user);
		User GetUserByName(string username);
		IEnumerable<User> GetAllUsers();
		void ManageUserStatus(User user, Rank rankForUser, IEnumerable<string> selectedRoles, IEnumerable<string> userSections, IEnumerable<Rank> allRanks, IEnumerable<Section> allSections, IEnumerable<Role> allRoles);
	}
}
