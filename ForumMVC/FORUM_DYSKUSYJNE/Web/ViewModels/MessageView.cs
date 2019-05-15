using FluentValidation.Attributes;
using FORUM_DYSKUSYJNE.Web.ViewModelValidators;

namespace FORUM_DYSKUSYJNE.Web.ViewModels
{
	[Validator(typeof(PrivateMessageValidator))]
    public class MessageView
    {
		

		public MessageView(string name)
		{
			Username = name;
		}

		public MessageView()
		{
			
		}

		public string Username { get; set; }
        public string MessageTitle { get; set; }
        public string MessageContent { get; set; }
    }
}