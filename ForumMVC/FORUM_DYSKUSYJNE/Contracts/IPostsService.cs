using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using FORUM_DYSKUSYJNE.Database;
using FORUM_DYSKUSYJNE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORUM_DYSKUSYJNE.Contracts
{
	public interface IPostsService
	{
		PostView GetTopicModel(Topic topic, User user,List<string> forbiddenWords);
		PostView CreatePost(List<string> forbiddenWords);
		Attachment GetAttachment();
	}
}
