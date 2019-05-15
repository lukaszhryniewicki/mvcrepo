using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Core.Models;
using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using FORUM_DYSKUSYJNE.Web.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace FORUM_DYSKUSYJNE.Controllers
{
	public class TopicController : Controller
	{
		private IUnitOfWork _unitOfWork;
		private readonly IPostsService _postsService;
		private readonly IForbiddenWordsService _forbiddenWordsService;
		private readonly ITopicsService _topicsService;
		private readonly IUsersService _usersService;

		public TopicController(IUnitOfWork unitOfWork,
			IPostsService postsService,
			IForbiddenWordsService forbiddenWordsService,
			ITopicsService topicsService,
			IUsersService usersService)
		{
			_unitOfWork=unitOfWork;
			_postsService = postsService;
			_forbiddenWordsService = forbiddenWordsService;
			_topicsService = topicsService;
			_usersService = usersService;
		}

		// GET: Topic
		public ActionResult Index()
		{
			return View();
		}

		[CustomAuthorize(Roles ="User")]
		public ActionResult Create(int? id=null)
		{
			if (id == null) return HttpNotFound();
			var model = new TopicCreateView()
			{
				SectionId = (int)id
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult Create(TopicCreateView viewmodel)
		{

			var forbiddenWords = _forbiddenWordsService.CheckForForbiddenWords(viewmodel.TopicName + " " + viewmodel.PostContent);

			if (ModelState.IsValid || !forbiddenWords.Any())
			{
				var topic=_topicsService.CreateTopic(viewmodel.TopicName, (User as CustomPrincipal).UserId, viewmodel.SectionId);

				var user = _usersService.GetUserById((User as CustomPrincipal).UserId);

				var attachment = _postsService.GetAttachment(viewmodel.UserAttachment);
				_postsService.CreatePost(viewmodel.PostContent, topic, user, attachment);
	
				return RedirectToAction("Create", "Post", new { id = topic.TopicId });
			}
			else
			{
				var model = new TopicCreateView()
				{

					TopicName = viewmodel.TopicName,
					PostContent = viewmodel.PostContent,
					ForbiddenWords = forbiddenWords,
				};


				return View("Create", model);

			}
			
		}
		[HttpDelete]
		[CustomAuthorize(Roles ="Admin")]
		public ActionResult Delete(int id,int sectionId)
		{

			_topicsService.DeleteTopic(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}
		[CustomAuthorize(Roles = "Admin")]
		
		public ActionResult ToggleStick(int id, int sectionId)
		{
			_topicsService.ToggleStickTopic(id);

			return RedirectToAction("Section", "Home", new { id = sectionId });
		}
		[HttpPost]
		public ActionResult Search(SectionView model)
		{
			return RedirectToAction("Section", "Home", new { id = model.SectionId , query = model.SearchTerm});
		}


	}
}