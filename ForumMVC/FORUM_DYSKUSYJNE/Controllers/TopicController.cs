using FORUM_DYSKUSYJNE.Contracts;
using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using FORUM_DYSKUSYJNE.DataAccess;
using FORUM_DYSKUSYJNE.Database;
using FORUM_DYSKUSYJNE.Models;
using FORUM_DYSKUSYJNE.SharedMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FORUM_DYSKUSYJNE.Controllers
{
	public class TopicController : Controller
	{
		private IUnitOfWork _unitOfWork;
		public TopicController(IUnitOfWork unitOfWork)
		{
			_unitOfWork=unitOfWork;
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
			
			
			 if (ModelState.IsValid)
			{
				var forbiddenWords = SharedMethods.CheckForForbiddenWords.CheckForForbiddenWord(viewmodel.TopicName, _unitOfWork);
				forbiddenWords.AddRange(SharedMethods.CheckForForbiddenWords.CheckForForbiddenWord(viewmodel.PostContent, _unitOfWork));
				if (forbiddenWords.Count > 0)
				{

					var modelx = new TopicCreateView()
					{

						TopicName = viewmodel.TopicName,
						PostContent = viewmodel.PostContent,
						ForbiddenWords = forbiddenWords,
					};


					return View("Create", modelx);
				}

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

				var topic = new Topic()
				{
					Name = viewmodel.TopicName,
					CreateDate = DateTime.Now,
					LastModified = DateTime.Now,
					AuthorId = (User as CustomPrincipal).UserId,
					SectionId = viewmodel.SectionId,
					Sticked = false,
				};
				

				topic.ViewsCount++;
				Session[topic.TopicId.ToString()] = true;
				_unitOfWork.Topics.Add(topic);


				var post = new Post()
				{
					Content = viewmodel.PostContent,
					CreateDate = DateTime.Now,
					LastModified = DateTime.Now,
					AuthorId = (User as CustomPrincipal).UserId,
					TopicId = topic.TopicId,
					Attachment = attachmentx
				};
				post.Content = FindEmote.FindEmotes(post.Content, _unitOfWork);
				_unitOfWork.Repository<Post>().Add(post);
				topic.PostsCount++;
				_unitOfWork.Complete();

				return RedirectToAction("Create", "Post", new { id = topic.TopicId });
			}
			else
			{
				
				var model = new TopicCreateView()
				{
					SectionId = viewmodel.SectionId
				};
				return View("Create",model);
			}
			
		}
		public ActionResult Delete(int id,int sectionId)
		{

			var topic = _unitOfWork.Topics.Get(id);
			_unitOfWork.Topics.Remove(topic);
			_unitOfWork.Complete();

			return RedirectToAction("Section","Home", new{ id=sectionId });
		}
		public ActionResult ToggleStick(int id, int sectionId)
		{
			var topic = _unitOfWork.Topics.Get(id);
			if (topic.Sticked)
			{
				topic.Sticked = false;
			}
			else
			{
				topic.Sticked = true;
			}

			_unitOfWork.Complete();

			return RedirectToAction("Section", "Home", new { id = sectionId });
		}
		[HttpPost]
		public ActionResult Search(SectionView model)
		{
			return RedirectToAction("Section", "Home", new { id = model.SectionId , query = model.SearchTerm});
		}


	}
}