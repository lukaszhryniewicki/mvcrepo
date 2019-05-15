using System;
using System.Linq;
using System.Web.Mvc;
using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using FORUM_DYSKUSYJNE.Web.ViewModels;
using FORUM_DYSKUSYJNE.Core.Models;
using FORUM_DYSKUSYJNE.Core.Contracts;

namespace FORUM_DYSKUSYJNE.Controllers
{	
	[CustomAuthorize(Roles ="Admin")]
	public class AdminController : Controller
	{
		private readonly IUsersService _usersService;
		private readonly IGroupsService _groupsService;
		private readonly ISectionsService _sectionsService;
		private readonly IForbiddenWordsService _forbiddenWordsService;
		private readonly IAnnouncementsService _announcementsService;
		private readonly IConstantsService _constantsService;

		public AdminController(
			IUsersService usersService,
			IGroupsService groupsService,
			ISectionsService sectionsService,
			IForbiddenWordsService forbiddenWordsService,
			IAnnouncementsService announcementsService,
			IConstantsService constantsService)
		{
			_usersService = usersService;
			_groupsService = groupsService;
			_sectionsService = sectionsService;
			_forbiddenWordsService = forbiddenWordsService;
			_announcementsService = announcementsService;
			_constantsService = constantsService;
		}
		
		public ActionResult Panel()
		{
			return View();
		}
		
		public ActionResult CreateSection()
		{
			var groups = _groupsService.GetAllGroups();
			var model = new PanelView(groups);
			return View(model);
		}

		[HttpPost]
		public ActionResult CreateSection(PanelView panelView)
		{
			if(ModelState.IsValid)
			{
				_sectionsService.CreateSection(panelView.Name, panelView.Description, panelView.SelectedSection, panelView.SelectedGroup);
				return RedirectToAction("Index", "Home");
			}

			return View(panelView);
			
		}

		public ActionResult EditDeleteForum()
		{
			var groups = _groupsService.GetAllGroups();
			var sections = _sectionsService.GetAllOrderedSections();

			var viewModel = new EditForumViewModel(groups, sections);

			return View(viewModel);	

		}
		//[HttpDelete]
		public ActionResult DeleteSection(int id=-1)
		{
			if(id != -1)
			{
				_sectionsService.DeleteSection(id);
				return RedirectToAction("EditDeleteForum", "Admin");
			}
			else
			{
				return HttpNotFound();
			}
			
		}

		public ActionResult EditSection(int id)
		{
			var edit = _sectionsService.GetSectionById(id);

			var groups = _groupsService.GetAllGroups();

			var sectionName = _sectionsService.GetSelectedSectionName(edit);


			var sections = _sectionsService.GetSectionsInGroup(edit.GroupId);

			var model = new PanelView(edit, groups, sections, sectionName);


			return View(model);
		}

        [HttpPost]
        public ActionResult EditSection(PanelView panelView)
        {

			if(ModelState.IsValid)
			{

				var group = _groupsService.GetGroupById(Convert.ToInt32(panelView.SelectedGroup));

				_sectionsService.EditSection(panelView.editedSectionId, panelView.SelectedSection, panelView.Description, panelView.Name, group);
				return RedirectToAction("Index", "Home");

			}
			else
			{
				return View(panelView);
			}
			
        }
		
		public ActionResult Privileges()
		{
			var users = _usersService.GetAllUsers();
			return View(users);
		}

		public ActionResult PrivilegesDetail(int id)
		{
			var user = _usersService.GetUserById(id);
			var sections = _sectionsService.GetAllOrderedSections();
			var ranks = _constantsService.GetAllRanks();
			var model = new PrivilegesView(user,sections,ranks);
			return View(model);
		}

		
		[HttpPost]
		public ActionResult PrivilegesDetail(PrivilegesView privilegesView,int id)
		{
			var user = _usersService.GetUserById(id);

			var allSections = _sectionsService.GetAllOrderedSections();

			if (ModelState.IsValid)
			{
				var rankForUser = _constantsService.GetRankById((int)privilegesView.SelectedRank);
				var allRanks = _constantsService.GetAllRanks();
				var allRoles = _constantsService.GetAllRoles();

				_usersService.ManageUserStatus(user, rankForUser, privilegesView.SelectedRoles, privilegesView.UserSections, allRanks, allSections, allRoles);
			}

			var model = new	PrivilegesView(user,allSections,privilegesView.UserSections);

			return RedirectToAction("PrivilegesDetail","Admin",model);
		}

		public ActionResult Announcement()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Announcement(Announcement model)
		{

			if (ModelState.IsValid)
			{
				_announcementsService.CreateAnnouncement(model.Content,model.ExpirationDate);
				
				return RedirectToAction("Index", "Home");
			}

			return View("Announcement");

		}

		public ActionResult ForbiddenWords()
		{
			var words = _forbiddenWordsService.GetAllWords();
			var model = new ForbiddenWordViewModel(words);
			return View(model);
		}

		[HttpPost]
		public ActionResult ForbiddenWords(ForbiddenWordViewModel model)
		{


			
			if (ModelState.IsValid)
			{
				if (!_forbiddenWordsService.IsWordUnique(model.word))
				{
					ModelState.AddModelError("duplicate", "Given word is already in the list");
					model.forbiddenWords = _forbiddenWordsService.GetAllWords();
					return View("ForbiddenWords", model);
				}

				_forbiddenWordsService.CreateWord(model.word);
			}
			return RedirectToAction("ForbiddenWords");


		}
		//[HttpDelete]
		public ActionResult DeleteWord(int id=-1)
		{
			if (id == -1) return HttpNotFound();

			 _forbiddenWordsService.DeleteWord(id);

			return RedirectToAction("ForbiddenWords");
		}

		public JsonResult GetSection(int group)
		{
			var sections = _sectionsService.GetOrderedSections(group);
			
			return Json(sections, JsonRequestBehavior.AllowGet);
		}

	}
}