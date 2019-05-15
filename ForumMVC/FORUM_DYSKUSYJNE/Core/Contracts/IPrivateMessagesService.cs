using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORUM_DYSKUSYJNE.Core.Contracts
{
	public interface IPrivateMessagesService
	{
		Message GetMessageById(int id);
		void DeleteMessage(int id, int userId);
		void SendMessage(int senderId, int receiverId, string title, string content);
		IEnumerable<Message> GetUsersSentMessages(int id);
		IEnumerable<Message> GetUsersReceivedMessages(int id);
	}
}
