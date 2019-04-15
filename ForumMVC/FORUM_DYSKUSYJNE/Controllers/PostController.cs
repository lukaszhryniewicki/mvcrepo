using FORUM_DYSKUSYJNE.Database;
using FORUM_DYSKUSYJNE.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using System.IO;
using FORUM_DYSKUSYJNE.Contracts;

namespace FORUM_DYSKUSYJNE.Controllers
{
	
	public class PostController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ITopicsService _topicsService;
		private readonly IPostsService _postsService;
		private readonly IForbiddenWordsService _forbiddenWordsService;
		private readonly IEmotesService _emotesService;
		private readonly ISectionsService _sectionsService;

		public PostController(
			IUnitOfWork unitOfWork,
			ITopicsService topicsService, 
			IPostsService postsService,
			IForbiddenWordsService forbiddenWordsService,
			IEmotesService emotesService,
			ISectionsService sectionsService)
		{
			_unitOfWork = unitOfWork;
			_topicsService = topicsService;
			_postsService = postsService;
			_forbiddenWordsService = forbiddenWordsService;
			_emotesService = emotesService;
			_sectionsService = sectionsService;
		}


		public ActionResult Index()
		{
			return View();
		}
		public ActionResult Create(int id=-1)
		{
			User user=null;
			if (id == -1) return HttpNotFound();

			var topic = _topicsService.GetTopicById(id);

			if ((User as CustomPrincipal) != null)
			{
				 user = _unitOfWork.Repository<User>().Get((User as CustomPrincipal).UserId);

			}

			var model =_postsService.GetTopicModel(topic,user,null);

			return View(model);
		}

		[HttpPost]
		[CustomAuthorize(Roles ="User")]
		public ActionResult Create(PostView viewmodel)
		{

			
			if (ModelState.IsValid)
			{

				var userId = (User as CustomPrincipal).UserId;
				var user = _unitOfWork.Repository<User>().Get(userId);
				var topic = _unitOfWork.Topics.Get(viewmodel.TopicId);
				
				var forbiddenWords = _forbiddenWordsService.CheckForForbiddenWords(viewmodel.Content);
				
				if (forbiddenWords.Count > 0)
				{
					var _model = _postsService.GetTopicModel(topic,user,forbiddenWords);
					return View("Create", _model);
				}
				else
				{
					
					var ranks = _unitOfWork.Repository<Rank>().GetAll();
	
					Attachment attachmentx = null;
					byte[] attachmentData = null;
					if (Request.Files.Count > 0)
					{
						HttpPostedFileBase attachment = Request.Files["UserAttachment"];

						if (attachment.ContentLength > 0)
						{
							using (var binary = new BinaryReader(attachment.InputStream))
							{
								attachmentData = binary.ReadBytes(attachment.ContentLength);
							}
							if (attachmentData.Length <= 3145728 * 5)
							{

								attachmentx = new Attachment()
								{
									AttachmentData = attachmentData,
									AttachmentName = Path.GetFileName(Request.Files["UserAttachment"].FileName),
								};

							}
							else
							{
								ModelState.AddModelError("AttachmentSizeError", "Attachment size too big");
							}

						}
					}
					attachmentx = _postsService.GetAttachment();
					var post = new Post()
					{
						Content = viewmodel.Content,
						CreateDate = DateTime.Now,
						LastModified = DateTime.Now,
						AuthorId = userId,
						TopicId = viewmodel.TopicId,
						User = user,
						Attachment = attachmentx


					};

					post.Content = _emotesService.FindEmotes(post.Content);
					_unitOfWork.Repository<Post>().Add(post);
					topic.PostsCount++;
					var userPosts = _unitOfWork.Repository<Post>().Find(x => x.AuthorId == userId).Count() + 1;

					foreach (var rank in ranks)
					{
						if (userPosts >= rank.Requirement) user.Rank = rank;
					}


					_unitOfWork.Topics.Get(viewmodel.TopicId).LastModified = DateTime.Now;
					_unitOfWork.Complete();
					ModelState.Clear();

					
					
					return RedirectToAction("Create", "Post", new { id = viewmodel.TopicId });
					
				}
			}
			else
			{
				var model = new PostView()
				{
					TopicId= viewmodel.TopicId,
					Topic= _unitOfWork.Topics.Get(viewmodel.TopicId)
				};
				return View(model);
			}
			
		}
	
		public ActionResult Delete(int id,int topicId)
		{
			var post = _unitOfWork.Repository<Post>().Get(id);
			_unitOfWork.Repository<Post>().Remove(post);
			_unitOfWork.Complete();
			
			if (_unitOfWork.Repository<Post>().Find(t => t.TopicId == topicId).Count() == 0)
			{
				var sectionId = _unitOfWork.Topics.GetTopicsSection(topicId);
				var delTopic = _unitOfWork.Topics.Get(topicId);
				_unitOfWork.Topics.Remove(delTopic);
				_unitOfWork.Complete();
				return RedirectToAction("Section", "Home",new { id = sectionId });	
			}
			var userId = (User as CustomPrincipal).UserId;
			bool isModx = false;
			var userMod = _unitOfWork.Repository<User>().Get(userId);
			var sectionid = _unitOfWork.Topics.GetTopicsSection(topicId);
			foreach (var section in userMod.Section)
			{
				if (section.SectionId == sectionid) isModx = true;
			}

			var model = new PostView()
			{
				isMod=isModx,
				Topic = _unitOfWork.Topics.Get(topicId),

		};
			return View("Create",model);
		}

		public ActionResult Edit(int id)
		{
			
			var post = _unitOfWork.Repository<Post>().Get(id);
			EditPostEmote(post);
			var userId = (User as CustomPrincipal).UserId;
			bool isModx = false;
			var userMod = _unitOfWork.Repository<User>().Get(userId);
			var sectionid = _unitOfWork.Topics.GetTopicsSection(post.TopicId);
			var model = new EditViewModel()
			{
				Post = post,
			};
			foreach (var section in userMod.Section)
			{
				if (section.SectionId == sectionid) isModx = true;
			}
			if (isModx ||(User as CustomPrincipal).UserId == post.AuthorId) return View(model);
			else
			{
				return HttpNotFound();
			}

		}

		[HttpPost]
		public ActionResult Edit(EditViewModel editPost)
		{
			var editedPost = _unitOfWork.Repository<Post>().Get(editPost.Post.PostId);
			var forbiddenWords = SharedMethods.CheckForForbiddenWords.CheckForForbiddenWord(editPost.Post.Content, _unitOfWork);
			if(forbiddenWords.Count > 0)
			{

				var modelx = new EditViewModel()
				{
					Post = editedPost,
					ForbiddenWords = forbiddenWords
				};
				return View(modelx);

			}
			else
			{

				editedPost.Content = editPost.Post.Content;
				editedPost.LastModified = DateTime.Now;
				editedPost.Content = _emotesService.FindEmotes(editedPost.Content);
				_unitOfWork.Complete();
				ModelState.Clear();
				var userId = (User as CustomPrincipal).UserId;
				bool isModx = false;
				var userMod = _unitOfWork.Repository<User>().Get(userId);
				var sectionid = _unitOfWork.Topics.GetTopicsSection(editedPost.TopicId);
				foreach (var section in userMod.Section)
				{
					if (section.SectionId == sectionid) isModx = true;
				}
				var model = new PostView()
				{
					isMod = isModx,
					Topic = _unitOfWork.Topics.Get(editPost.Post.TopicId),

				};

				return RedirectToAction("Create", "Post", new { id = editPost.Post.TopicId });

			}

		}
		
		public void EditPostEmote(Post post)
		{
			var emotes = _unitOfWork.Repository<Emoticon>().GetAll();
			foreach (var emote in emotes)
			{
				var substring = string.Format("<img src=\"{0}\" width=\"25\" height=\"28\"  \" />", emote.SourceLink);
				
				
					post.Content = post.Content.Replace(substring, emote.Name );
				
			}
			
		}

		public ActionResult Report(int id )
		{

			var post = _unitOfWork.Repository<Post>().Get(id);
			string title;
			if(post.Content.Length >=10)
			{
				title = post.Content.Substring(0, 10);
			}
			else
			{
				title = post.Content.Substring(0, post.Content.Length);
			}
			var moderators = _unitOfWork.Sections.GetSectionModerators(post.Topic.SectionId);
			string url = string.Format("<a href=http://localhost:54983/Post/Create/{0}> Topic link </a>", post.TopicId);
			Session["Cooldown"] = DateTime.Now;
			foreach(var mod in moderators )
			{
				var message = new Message()
				{
					Title = "Post has been reported - " + title + "...",
					BelongsTo = mod.UserId,
					Content=post.Content +"<p> Link to the topic: "+ url +"</p>",
					SenderId= (User as CustomPrincipal).UserId,
					ReceiverId=mod.UserId,
					SendDate = DateTime.Now,

			};
				_unitOfWork.Repository<Message>().Add(message);
			}
			_unitOfWork.Complete();

			return RedirectToAction("Create", "Post", new { @id = post.TopicId });
			
		}

		public FileResult Download (int id)
		{

			var attachmentx = _unitOfWork.Repository<Post>().Find(x => x.PostId == id).FirstOrDefault();
			return File(attachmentx.Attachment.AttachmentData, System.Net.Mime.MediaTypeNames.Application.Octet, attachmentx.Attachment.AttachmentName);
		}

	}
}