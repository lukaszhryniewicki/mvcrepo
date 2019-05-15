using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Infrastructure.Services
{
	public class PrivateMessagesService : IPrivateMessagesService
	{
		private readonly IDataService _dataService;

		public PrivateMessagesService(IDataService dataService)
		{
			_dataService = dataService;
		}
		public void DeleteMessage(int id,int userId)
		{
			var message = _dataService.GetDbSet<Message>()
				.Where(x => x.MessageId == id && (x.SenderId == userId || x.ReceiverId == userId))
				.SingleOrDefault();

			_dataService.GetDbSet<Message>().Remove(message);
			_dataService.SaveChanges();
		}
		public Message GetMessageById(int id)
		{
			return _dataService.GetDbSet<Message>()
				.Where(x => x.MessageId == id)
				.SingleOrDefault();
		}
		public IEnumerable<Message> GetUsersSentMessages(int id)
		{
			return _dataService.GetDbSet<Message>()
				.Where(x => x.BelongsTo == id && x.SenderId == id)
				.ToList();
		}
		public IEnumerable<Message> GetUsersReceivedMessages(int id)
		{
			return _dataService.GetDbSet<Message>()
				.Where(x => x.BelongsTo == id && x.ReceiverId == id)
				.ToList();
		}

		public void SendMessage(int senderId, int receiverId, string title, string content)
		{
			

			var messageSender = new Message()
			{
				SenderId=senderId,
				ReceiverId=receiverId,
				Title=title,
				Content= content,
				SendDate= DateTime.Now,
				BelongsTo=senderId,
			};

			var messageReceiver = new Message()
			{
				SenderId = senderId,
				ReceiverId = receiverId,
				Title = title,
				Content = content,
				SendDate = DateTime.Now,
				BelongsTo = receiverId,
			};

			_dataService.GetDbSet<Message>().Add(messageSender);
			_dataService.GetDbSet<Message>().Add(messageReceiver);

			_dataService.SaveChanges();

		}
	}
}