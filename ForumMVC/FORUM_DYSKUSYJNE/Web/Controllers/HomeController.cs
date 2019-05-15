using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Collections.ObjectModel;
using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using FORUM_DYSKUSYJNE.Core.Models;
using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Web.ViewModels;

namespace FORUM_DYSKUSYJNE.Controllers
{

	public class HomeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUsersService _usersService;
		private readonly IAnnouncementsService _announcementsService;
		private readonly IGroupsService _groupsService;
		private readonly ISectionsService _sectionsService;
		private readonly ITopicsService _topicsService;

		public HomeController(IUnitOfWork unitOfWork,
			IUsersService usersService,
			IAnnouncementsService announcementsService,
			IGroupsService groupsService,
			ISectionsService sectionsService,
			ITopicsService topicsService)
		{
			_unitOfWork = unitOfWork;
			_usersService = usersService;
			_announcementsService = announcementsService;
			_groupsService = groupsService;
			_sectionsService = sectionsService;
			_topicsService = topicsService;
		}

		public ActionResult Index()
		{
			_announcementsService.RemoveExpiredAnnouncements();
			var announcements = _announcementsService.GetAllAnnouncements();

			var registeredUsersInfo = _usersService.GetAllUsersNumber();

			var groups = _groupsService.GetAllGroups();

			var sections = _sectionsService.GetAllOrderedSections();

			var viewModel = new HomeView(groups, sections, announcements, registeredUsersInfo);

			return View(viewModel);
		}

		public ActionResult Section(int id = -1, string query = null)
		{


			if (id == -1) return HttpNotFound();

			User user = null;
			if ((User as CustomPrincipal) != null)
			{
				user = _usersService.GetUserById((User as CustomPrincipal).UserId);
			}

			var sectionName = _sectionsService.GetSectionById(id).Name;

			var isMod = _usersService.IsUserMod(id, user);

			if (string.IsNullOrEmpty(query))
			{
				var topics = _topicsService.GetTopicsInSection(id);
				var viewModel = new SectionView(topics, id, isMod, sectionName);
				return View(viewModel);
			}
			else
			{
				var topics = _topicsService.GetSectionTopicsByQuery(query, id);
				var viewModel = new SectionView(topics, id, isMod, sectionName, query);
				return View(viewModel);
			}


		}
		//[HttpDelete]
		[CustomAuthorize(Roles = "Admin")]
		public ActionResult Delete(int id)
		{

			_announcementsService.DeleteAnnouncement(id);

			return RedirectToAction("Index");
		}
	}
}