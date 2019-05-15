using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Core.Models;
using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using FORUM_DYSKUSYJNE.Web.ViewModels;

namespace FORUM_DYSKUSYJNE.Controllers
{

	public class PostController : Controller
	{
		private readonly ITopicsService _topicsService;
		private readonly IPostsService _postsService;
		private readonly IForbiddenWordsService _forbiddenWordsService;
		private readonly ISectionsService _sectionsService;
		private readonly IUsersService _usersService;

		public PostController(
			IUsersService usersService,
			ITopicsService topicsService,
			IPostsService postsService,
			IForbiddenWordsService forbiddenWordsService,
			ISectionsService sectionsService)
		{
			_topicsService = topicsService;
			_postsService = postsService;
			_forbiddenWordsService = forbiddenWordsService;
			_sectionsService = sectionsService;
			_usersService = usersService;
		}


		public ActionResult Index()
		{
			return View();
		}
		public ActionResult Create(int id = -1)
		{
			if (id == -1) return HttpNotFound();

			User user = null;
			if ((User as CustomPrincipal) != null)
			{
				user = _usersService.GetUserById((User as CustomPrincipal).UserId);
			}

			var topic = _topicsService.GetTopicById(id);
			var isUserMod = _usersService.IsUserMod(topic.SectionId, user);
			

			_topicsService.IncreaseTopicViewCount(topic.TopicId);

			var model = new PostView(isUserMod, topic);
			return View(model);
		}

		[HttpPost]
		[CustomAuthorize(Roles = "User")]
		public ActionResult Create(PostView viewmodel)
		{
			var user = _usersService.GetUserById((User as CustomPrincipal).UserId);
			var forbiddenWords = _forbiddenWordsService.CheckForForbiddenWords(viewmodel.Content);
			var topic = _topicsService.GetTopicById(viewmodel.TopicId);

			if (ModelState.IsValid && !forbiddenWords.Any())
			{
				ModelState.Clear();

				var attachment = _postsService.GetAttachment(viewmodel.UserAttachment);
				_postsService.CreatePost(viewmodel.Content,topic, user, attachment);

				return RedirectToAction("Create", "Post", new { id = viewmodel.TopicId });
			}
			else
			{
				
				var isUserMod = _usersService.IsUserMod(topic.SectionId, user);
				var model = new PostView(isUserMod, topic,forbiddenWords);

				return View(model);
			}
		}
		[HttpDelete]
		public ActionResult Delete(int id, int topicId)
		{
			var topic = _topicsService.GetTopicById(topicId);

			_postsService.DeletePost(id);

			
			if (topic.PostsCount == 0)
			{
				return RedirectToAction("Section", "Home", new { id = topic.SectionId });
			}
			else
			{

				var user = _usersService.GetUserById((User as CustomPrincipal).UserId);
				var isUserMod = _usersService.IsUserMod(topic.SectionId, user);
				var model = new PostView(isUserMod, topic);

				return View("Create", model);
			}
		}

		public ActionResult Edit(int id)
		{

			var post = _postsService.GetPostbyId(id);
			var user = _usersService.GetUserById((User as CustomPrincipal).UserId);
			bool isMod = _usersService.IsUserMod(post.Topic.SectionId, user);

			if (isMod || user.UserId == post.AuthorId)
			{
				var model = new EditViewModel(post);
				return View(model);
			}
			else
			{
				return HttpNotFound();
			}

		}

		[HttpPost]
		public ActionResult Edit(EditViewModel editPost)
		{
			var forbiddenWords = _forbiddenWordsService.CheckForForbiddenWords(editPost.Post.Content);

			if (forbiddenWords.Any() )
			{
				editPost.ForbiddenWords = forbiddenWords;
				return View(editPost);

			}
			else
			{
				_postsService.EditPost(editPost.Post.PostId, editPost.Post.Content);
				return RedirectToAction("Create", "Post", new { id = editPost.Post.TopicId });

			}

		}
		[CustomAuthorize(Roles ="User")]
		public ActionResult Report(int? id=null )
		{
			if (id == null) return HttpNotFound();

			var post = _postsService.GetPostbyId(id);

			var moderators = _sectionsService.GetSectionModerators(post.Topic.SectionId);

			_postsService.ReportPost(post,moderators, (User as CustomPrincipal).UserId);
			Session["Cooldown"] = DateTime.Now;

			return RedirectToAction("Create", "Post", new { @id = post.TopicId });
			
		}

		public FileResult Download (int id)
		{

			var post = _postsService.GetPostbyId(id);
			return File(post.Attachment.AttachmentData, System.Net.Mime.MediaTypeNames.Application.Octet, post.Attachment.AttachmentName);
		}

	}
}