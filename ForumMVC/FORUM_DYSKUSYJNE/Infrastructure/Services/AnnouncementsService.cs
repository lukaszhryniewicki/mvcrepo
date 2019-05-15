using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Infrastructure.Services
{
	public class AnnouncementsService : IAnnouncementsService
	{
		private readonly IDataService _dataService;

		public AnnouncementsService(IDataService dataService)
		{
			_dataService = dataService;
		}
		public void RemoveExpiredAnnouncements()
		{
			var announcementsToRemove = _dataService.GetDbSet<Announcement>()
				.Where(x => x.ExpirationDate <= DateTime.Now)
				.ToList();

			_dataService.GetDbSet<Announcement>().RemoveRange(announcementsToRemove);

			_dataService.SaveChanges();
		}
		public IEnumerable<Announcement> GetAllAnnouncements()
		{
			return _dataService.GetDbSet<Announcement>().ToList();
		}

		public void DeleteAnnouncement(int id)
		{
			var announcement = _dataService.GetDbSet<Announcement>()
				.Where(x => x.Id == id)
				.SingleOrDefault();
			_dataService.GetDbSet<Announcement>().Remove(announcement);
			_dataService.SaveChanges();
		}

		public void CreateAnnouncement(string content, DateTime expirationDate)
		{
			var announcement = new Announcement()
			{
				Content = content,
				CreateDate = DateTime.Now,
				ExpirationDate = expirationDate,
			};

			_dataService.GetDbSet<Announcement>().Add(announcement);
			_dataService.SaveChanges();
		}
	}
}