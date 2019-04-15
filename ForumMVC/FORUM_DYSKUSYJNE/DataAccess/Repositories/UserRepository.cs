using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FORUM_DYSKUSYJNE.DataAccess.Repositories
{
	/// <summary>
	/// Klasa implementujaca definicje interfejsu IUserRepository.
	/// </summary>
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(ForumDatabase context) : base(context){ }

		public User GetUserWithRank(int id)
		{
			return context.Users.Include(x => x.Rank).Where(x => x.UserId == id).FirstOrDefault();
		}

		public User GetUserByEmail(string email)
		{
			return context.Users.Where(x => x.Email == email).FirstOrDefault();
		}

		public User GetUserByCode(string code)
		{
			return context.Users.Where(x => x.ForgottenPasswordCode == code).FirstOrDefault();
		}
	}
}