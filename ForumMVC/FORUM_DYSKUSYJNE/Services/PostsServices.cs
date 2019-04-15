using FORUM_DYSKUSYJNE.Contracts;
using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using FORUM_DYSKUSYJNE.Database;
using FORUM_DYSKUSYJNE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Services
{
	public class PostsServices:IPostsService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ISectionsService _sectionsService;
		public PostsServices(IUnitOfWork unitOfWork, ISectionsService sectionsService)
		{
			_unitOfWork = unitOfWork;
			_sectionsService = sectionsService;

		}

		public PostView CreatePost(List<string> forbiddenWords, Topic topic, CustomPrincipal user)
		{
			bool isModx = false;

			var sectionid = _unitOfWork.Topics.GetTopicsSection(topic.TopicId);
			if (user != null)
			{
				var userMod = _unitOfWork.Repository<User>().Get(user.UserId);
				isModx= _sectionsService.IsUserMod(sectionid, userMod);


			}
			if (forbiddenWords.Count>0)
			{
				 var model = new PostView()
				{
					isMod = isModx,
					Topic = _unitOfWork.Topics.Get(topic.TopicId),
					TopicId = topic.TopicId,
					ForbiddenWords = forbiddenWords
				};
				return model;
			}
		}

		public PostView CreatePost(List<string> forbiddenWords)
		{
			throw new NotImplementedException();
		}

		public Attachment GetAttachment()
		{
			byte[] attachmentData = null;
			if (Request.Files.Count > 0)
			{
				HttpPostedFileBase attachment = HttpContext.Current.Request.Files["UserAttachment"];

				if (attachment.ContentLength > 0)
				{
					using (var binary = new BinaryReader(attachment.InputStream))
					{
						attachmentData = binary.ReadBytes(attachment.ContentLength);
					}
					if (attachmentData.Length <= 3145728 * 5)
					{

						Attachment _attachment = new Attachment()
						{
							AttachmentData = attachmentData,
							AttachmentName = Path.GetFileName(HttpContext.Current.Request.Files["UserAttachment"].FileName),
						};
						return _attachment;

					}
					else
					{
						ModelState.AddModelError("AttachmentSizeError", "Attachment size too big");
					}

				}
			}
			return null;
		}

		public PostView GetTopicModel(Topic topic, User user, List<string> forbiddenWords=null)
		{
			bool isModx = false;

			var sectionid = _unitOfWork.Topics.GetTopicsSection(topic.TopicId);
			if (user != null)
			{
				var userMod = _unitOfWork.Repository<User>().Get(user.UserId);
				_sectionsService.IsUserMod(sectionid, userMod);

			}

			if (HttpContext.Current.Session[topic.TopicId.ToString()] == null)
			{
				topic.ViewsCount++;
				_unitOfWork.Complete();
				HttpContext.Current.Session[topic.TopicId.ToString()] = true;
			}

			var model = new PostView()
			{
				isMod = isModx,
				Topic = _unitOfWork.Topics.Get(topic.TopicId),
				TopicId = topic.TopicId,
				ForbiddenWords = forbiddenWords
			};
			return model;
		}
		
	}
}