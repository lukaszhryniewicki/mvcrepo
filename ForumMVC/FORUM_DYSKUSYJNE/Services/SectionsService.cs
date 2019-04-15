using FORUM_DYSKUSYJNE.Contracts;
using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Services
{
	public class SectionsService : ISectionsService
	{
		public bool IsUserMod(int? id, User user)
		{
			bool isMod=false;
			foreach (var section in user.Section)
			{
				if (section.SectionId == id) isMod = true;
			}
			return isMod;
		}
	}
}