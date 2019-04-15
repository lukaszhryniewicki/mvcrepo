using FORUM_DYSKUSYJNE.Database;
using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using FORUM_DYSKUSYJNE.DataAccess;
using FORUM_DYSKUSYJNE.SharedMethods;
using FORUM_DYSKUSYJNE.Contracts;

namespace FORUM_DYSKUSYJNE.Controllers
{

	public class PrivateMessageController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		PostController controller;

		public PrivateMessageController( IUnitOfWork unitOfWork, ITopicsService topicsService,IPostsService postsService,IForbiddenWordsService forbiddenWordsService
			,IEmotesService emotesService,
			ISectionsService sectionsService)
		{
			_unitOfWork=unitOfWork;
			controller = new PostController( unitOfWork,topicsService,postsService,forbiddenWordsService,emotesService, sectionsService);
		}

		public ActionResult Create()
		{
			return View();
		}

		public ActionResult RecievedMessages(int id)
		{
			
			var allMessages = _unitOfWork.Repository<Message>().Find(m => m.ReceiverId == id && m.BelongsTo == id);
			var userId=(User as CustomPrincipal).UserId;
			ViewBag.Heading = "Recieved Messages";
			ViewBag.Person = "From";
			return View("Messages", allMessages);
		}

		public ActionResult SentMessages(int id)
		{
			var allMessages = _unitOfWork.Repository<Message>().Find(i => i.SenderId == id && i.BelongsTo == id);
			var userId = (User as CustomPrincipal).UserId;
			ViewBag.Heading = "My sent messages";
			ViewBag.Person = "To";
			return View("Messages", allMessages);
		}

        public ActionResult DeleteMessage(int id)
        {
           
			var message = _unitOfWork.Repository<Message>().Get(id);
			if((User as CustomPrincipal) != null)
			{

			
			if ((User as CustomPrincipal).UserId == message.SenderId || (User as CustomPrincipal).UserId == message.ReceiverId)
			{
					_unitOfWork.Repository<Message>().Remove(message);
					_unitOfWork.Complete();
			}
			}


			return RedirectToAction("AccountDetail", "Account", new { id = (User as CustomPrincipal).UserId });
        }

		public ActionResult SendMessage(int? id	= null)
		{
			
			
			if (id != null)
			{
				var receiver = _unitOfWork.Repository<User>().Get((int)id);
				var msg = new Message()
				{
					Receiver = receiver
				};
				return View(msg);
			}
			return View();
		}

		[HttpPost]
		public ActionResult SendMessage(Message message)
		{

			var user = _unitOfWork.Repository<User>().Find(x=>x.Username == message.Receiver.Username).FirstOrDefault();
			if(user ==null)
			{
				ModelState.AddModelError("Warning Username", "User with that username does not exist");
				return View("sendMessage");
			}

			var mSender = new Message()
			{
				SenderId = (User as CustomPrincipal).UserId,
				ReceiverId = user.UserId,
				Title = message.Title,
				Content = message.Content,
				SendDate = DateTime.Now,
				BelongsTo = (User as CustomPrincipal).UserId
			};
			var mReceiver = new Message()
			{
				SenderId = (User as CustomPrincipal).UserId,
				ReceiverId = user.UserId,
				Title = message.Title,
				Content = message.Content,
				SendDate = DateTime.Now,
				BelongsTo = user.UserId
			};

			if(message.Content!= null)
			{ 
			mSender.Content = FindEmote.FindEmotes(mSender.Content, _unitOfWork);
			mReceiver.Content = FindEmote.FindEmotes(mReceiver.Content, _unitOfWork);
			}
			_unitOfWork.Repository<Message>().Add(mSender);
			_unitOfWork.Repository<Message>().Add(mReceiver);
			_unitOfWork.Complete();
			return RedirectToAction("sentMessages",new { id= (User as CustomPrincipal).UserId });
		}

		public ActionResult MessageDetails(int id)
		{

			var message = _unitOfWork.Repository<Message>().Get(id);
			return View(message);
		}
	}
}