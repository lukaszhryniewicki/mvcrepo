using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Infrastructure.Services
{
	public class UsersService:IUsersService
	{
		private readonly IDataService _dataService;
		public UsersService(IDataService dataService)
		{
			_dataService = dataService;
		}
		public User GetUserWithRank(int id)
		{
			return _dataService.GetDbSet<User>()
				.Include(x => x.Rank)
				.Where(x => x.UserId == id)
				.SingleOrDefault();
		}

		public User GetUserByEmail(string email)
		{
			return _dataService.GetDbSet<User>()
				.Where(x => x.Email == email)
				.SingleOrDefault();
		}

		public User GetUserByCode(string code)
		{
			return _dataService.GetDbSet<User>()
				.Where(x => x.ForgottenPasswordCode == code)
				.SingleOrDefault();
		}

		public User GetUserById(int id)
		{
			return _dataService.GetDbSet<User>()
				.Where(x => x.UserId == id)
				.SingleOrDefault();
		}

		public User GetUserByName(string username)
		{
			return _dataService.GetDbSet<User>()
				.Where(x => x.Username == username)
				.SingleOrDefault();
		}

		public string GetAllUsersNumber()
		{
			return $"Number of registered users: { _dataService.GetDbSet<User>().ToList().Count }";
		}

		public bool IsUserMod(int? id, User user)
		{
			bool isMod = false;
			if (user != null)
			{
				foreach (var section in user.Section)
				{
					if (section.SectionId == id) isMod = true;
				}
			}
			return isMod;
		}

		public IEnumerable<User> GetAllUsers()
		{
			return _dataService.GetDbSet<User>().ToList();
		}

		public void ManageUserStatus(User user, Rank rankForUser, IEnumerable<string> selectedRoles, IEnumerable<string> userSections, IEnumerable<Rank> allRanks, IEnumerable<Section> allSections, IEnumerable<Role> allRoles)
		{

			if (rankForUser != null)
			{
				user.Rank = rankForUser;
			}

			var userRoles = allRoles
				.Where(x => selectedRoles.Contains(x.RoleName))
				.ToList();

			var newUserSections = allSections
				.Where(x => userSections.Contains(x.SectionId.ToString()))
				.ToList();


			user.Role.Clear();
			user.Role = userRoles;
			user.Section = newUserSections;

			_dataService.SaveChanges();

		}
	}
}