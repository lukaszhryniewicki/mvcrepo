using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FORUM_DYSKUSYJNE.Core.Models;
using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Web.ViewModels;
using System.Net;

namespace FORUM_DYSKUSYJNE.Controllers
{
	[CustomAuthorize(Roles ="User")]
	public class PrivateMessageController : Controller
	{
		private readonly IPostsService _postsService;
		private readonly IUsersService _usersService;
		private readonly IPrivateMessagesService _privateMessagesService;

		public PrivateMessageController( 
			 IPostsService postsService,
			IUsersService usersService,
			IPrivateMessagesService privateMessagesService)
		{
			_postsService = postsService;
			_usersService = usersService;
			_privateMessagesService = privateMessagesService;
		}

		public ActionResult Create()
		{
			return View();
		}

		public ActionResult RecievedMessages(int id)
		{

			var messages = _privateMessagesService.GetUsersReceivedMessages(id);
			var model = new MessageListViewModel("Recieved Messages", "From",messages);

			return View("Messages", model);
		}

		public ActionResult SentMessages(int id)
		{
			var messages = _privateMessagesService.GetUsersSentMessages(id);
			var model = new MessageListViewModel("My sent messages","To",messages);

			return View("Messages", model);
		}
		//[HttpDelete]
        public ActionResult DeleteMessage(int id)
        {


			if ((User as CustomPrincipal) != null)
			{

				_privateMessagesService.DeleteMessage(id, (User as CustomPrincipal).UserId);
				return new HttpStatusCodeResult(HttpStatusCode.OK);

			}
			else
			{
				return HttpNotFound();
			}

        }

		public ActionResult SendMessage(int id	= -1)
		{
			
			
			if (id != -1)
			{
				var receiver = _usersService.GetUserById(id);
				var model = new MessageView(receiver.Name);

				return View(model);
			}
			return View();
		}

		[HttpPost]
		public ActionResult SendMessage(MessageView message)
		{
			if(ModelState.IsValid)
			{

				var user = _usersService.GetUserByName(message.Username);
				if (user == null)
				{
					ModelState.AddModelError("Warning Username", "User with that username does not exist");
					return View("sendMessage");
				}

				message.MessageContent = _postsService.FindEmotes(message.MessageContent);
				_privateMessagesService.SendMessage((User as CustomPrincipal).UserId, user.UserId, message.MessageTitle, message.MessageContent);

				return RedirectToAction("sentMessages", new { id = (User as CustomPrincipal).UserId });
			}
			else
			{
				return View(message);
			}
		}

		public ActionResult MessageDetails(int id)
		{

			var message = _privateMessagesService.GetMessageById(id);
			return View(message);
		}
	}
}