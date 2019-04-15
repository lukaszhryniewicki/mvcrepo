using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORUM_DYSKUSYJNE.DataAccess.Repositories
{

	public interface IUserRepository:IRepository<User>
	{
		User GetUserWithRank(int id);
		User GetUserByEmail(string email);
		User GetUserByCode(string code);
	}
}
