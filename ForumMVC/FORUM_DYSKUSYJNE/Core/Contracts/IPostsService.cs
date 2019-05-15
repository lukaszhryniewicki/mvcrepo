using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace FORUM_DYSKUSYJNE.Core.Contracts
{
	public interface IPostsService
	{
		void CreatePost(string content, Topic topic, User user, Attachment attachment);
		Attachment GetAttachment( HttpPostedFileBase file);
		string FindEmotes(string text);
		void DeletePost(int id);
		void EditPost(int id,string text);
		Post GetPostbyId(int? id);
		void ReportPost(Post post, IEnumerable<User> moderators, int sentById);
	}
}
