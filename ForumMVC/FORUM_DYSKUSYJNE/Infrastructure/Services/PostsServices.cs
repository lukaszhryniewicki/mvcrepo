using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Infrastructure.Services

{
	public class PostsServices : IPostsService
	{
		private readonly IDataService _dataService;
		public PostsServices(IDataService dataService)
		{
			_dataService = dataService;

		}

		public void CreatePost(string content,Topic topic, User user, Attachment attachment)
		{
			
			

			var post = new Post()
			{
				Content = content,
				CreateDate = DateTime.Now,
				LastModified = DateTime.Now,
				AuthorId = user.UserId,
				Topic = topic,
				User = user,
				Attachment = attachment
			};

			post.Content = FindEmotes(post.Content);
			_dataService.GetDbSet<Post>().Add(post);

			var ranks = _dataService.GetDbSet<Rank>().ToList();

			var userPosts = _dataService.GetDbSet<Post>()
				.Where(x => x.AuthorId == user.UserId)
				.Count() + 1;

			foreach (var rank in ranks)
			{
				if (userPosts >= rank.Requirement) user.Rank = rank;
			}

			

			topic.LastModified = DateTime.Now;
			topic.PostsCount++;

			_dataService.SaveChanges();

		}


		public Attachment GetAttachment(HttpPostedFileBase file)
		{
			byte[] attachmentData = null;

			if (file != null && file.ContentLength > 0)
			{
				using (var binary = new BinaryReader(file.InputStream))
				{
					attachmentData = binary.ReadBytes(file.ContentLength);
				}
				
					Attachment _attachment = new Attachment()
					{
						AttachmentData = attachmentData,
						AttachmentName = file.FileName,
					};
					return _attachment;
			}

			return null;
		}

		public string FindEmotes(string text)
		{
			if (text == null) return null;
			var emotes = _dataService.GetDbSet<Emoticon>().ToList();

			if (text.Length > 0)
			{
				emotes.ForEach(x => text = text.Replace(x.Name, string.Format("<img src=\"{0}\" width=\"25\" height=\"28\"  \" />", x.SourceLink)));
			}

			return text;
		}

		public void DeletePost(int id)
		{
			var post = _dataService.GetDbSet<Post>()
				.Include(x => x.Topic)
				.Where(x => x.PostId == id)
				.Single();

			post.Topic.PostsCount--;

			if (post.Topic.PostsCount == 0)
			{
				_dataService.GetDbSet<Topic>().Remove(post.Topic);

			}
			_dataService.GetDbSet<Post>().Remove(post);
			_dataService.SaveChanges();
		}

		public void EditPost(int id, string text)
		{
			var post = GetPostbyId(id);
			text = FindEmotes(text);
			post.Content = text;
			post.LastModified = DateTime.Now;

			_dataService.SaveChanges();

		}

		public Post GetPostbyId(int? id)
		{
			return _dataService.GetDbSet<Post>()
							.Where(x => x.PostId == id)
							.Single();
		}

		public void ReportPost(Post post, IEnumerable<User> moderators, int sentById)
		{
			string title;
			if (post.Content.Length >= 10)
			{
				title = post.Content.Substring(0, 10);
			}
			else
			{
				title = post.Content.Substring(0, post.Content.Length);
			}

			string url = string.Format("<a href=http://localhost:54983/Post/Create/{0}> Topic link </a>", post.TopicId);

			foreach (var mod in moderators)
			{
				var message = new Message()
				{
					Title = "Post has been reported - " + title + "...",
					BelongsTo = mod.UserId,
					Content = post.Content + "<p> Link to the topic: " + url + "</p>",
					SenderId = sentById,
					ReceiverId = mod.UserId,
					SendDate = DateTime.Now,

				};
				_dataService.GetDbSet<Message>().Add(message);
			}
			_dataService.SaveChanges();
			
		}
	}
}