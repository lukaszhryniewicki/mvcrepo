using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Web.ViewModels
{
	public class MessageListViewModel
	{
		public IEnumerable<Message> Messages { get; set; }
		public string Header { get; set; }
		public string Direction { get; set; }

		public MessageListViewModel(string header, string direction, IEnumerable<Message> messages)
		{
			Messages = messages;
			Header= header;
			Direction = direction;
		}

		public MessageListViewModel()
		{

		}
	}
}