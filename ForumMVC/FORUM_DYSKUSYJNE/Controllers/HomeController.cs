using FORUM_DYSKUSYJNE.Database;
using FORUM_DYSKUSYJNE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.ObjectModel;
using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using FORUM_DYSKUSYJNE.Contracts;

namespace FORUM_DYSKUSYJNE.Controllers
{

	public class HomeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public HomeController(IUnitOfWork unitOfWork)
		{
			_unitOfWork=unitOfWork;
		}
		
		public ActionResult Index()
		{
			var announcements = _unitOfWork.Repository<Announcement>().GetAll();

			foreach (var ann in announcements)
			{
				if (ann.ExpirationDate <= DateTime.Now) _unitOfWork.Repository<Announcement>().Remove(ann);
			}
			_unitOfWork.Complete();
			
			var registeredUsers = "Number of registered users:  "+ _unitOfWork.Users.GetAll().Count();
			
			var viewModel = new HomeView()
			{
				Groups = _unitOfWork.Repository<Group>().GetAll(),
				Sections = _unitOfWork.Sections.OrderBy(x=>x.Order),
				Announcements = announcements,
				RegisteredUsersInfo = registeredUsers,	
			};


			return View(viewModel);
		}

		public ActionResult Section(int id=-1,string query=null)
		{
			
		
			if (id == -1) return HttpNotFound();
			ICollection<Topic> topicsModel = new Collection<Topic>();
			var sectionName = _unitOfWork.Sections.Get(id).Name;
			bool isModx = false;

			if((User as CustomPrincipal) !=null)
			{
				var userId = (User as CustomPrincipal).UserId;
				var userMod = _unitOfWork.Repository<User>().Get(userId);

				foreach (var section in userMod.Section)
				{
					if (section.SectionId == id) isModx = true;
				}
			}
			
			SectionView viewModel = new SectionView();
			if(string.IsNullOrEmpty(query))
			{
				viewModel = new SectionView()
				{
					Topics = _unitOfWork.Topics.GetTopicsWithUser(id),
					SectionId = id,
					isMod = isModx,
					SectionName=sectionName
				};

			}
			else
			{
				var topics = _unitOfWork.Topics.Find(x => x.SectionId == id);
			
				foreach (var topic in topics)
				{
					foreach (var post in topic.Post)
					{
						if(post.Content.Contains(query) || post.Topic.Name.Contains(query))
						{
							if (!topicsModel.Contains(topic))
							{
								topicsModel.Add(topic);
							}
						}
					}
				}
					
					viewModel = new SectionView()
					{

						isMod = isModx,
						SearchTerm =query,
						SectionId = id,
						Topics=topicsModel,
						SectionName = sectionName

					};
			}

			return View(viewModel);
		}

		public ActionResult Delete(int id)
		{

			var announcement = _unitOfWork.Repository<Announcement>().Get(id);
			_unitOfWork.Repository<Announcement>().Remove(announcement);
			_unitOfWork.Complete();

			return RedirectToAction("Index");
		}
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}