using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORUM_DYSKUSYJNE.Core.Contracts
{
	public interface IAnnouncementsService
	{
		void RemoveExpiredAnnouncements();
		IEnumerable<Announcement> GetAllAnnouncements();
		void DeleteAnnouncement(int id);
		void CreateAnnouncement(string content, DateTime expirationDate);
	}
}
