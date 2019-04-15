using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORUM_DYSKUSYJNE.Contracts
{
	public interface ISectionsService
	{
		bool IsUserMod(int? id,User user);
	}
}
