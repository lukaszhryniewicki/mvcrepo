using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;

namespace FORUM_DYSKUSYJNE.Core.Contracts
{
	public interface ITopicsService
	{
		Topic GetTopicById(int id);
		int? GetTopicsSection(int id);
		IEnumerable<Topic> GetTopicsInSection(int sectionId);
		IEnumerable<Topic> GetSectionTopicsByQuery(string query,int sectionId);
		void IncreaseTopicViewCount(int topicId);
		int GetTopicIdByDateAndAuthor(DateTime createDate, int authorId);
		Topic CreateTopic(string topicName, int userId, int sectionId);
		void DeleteTopic(int id);
		void ToggleStickTopic(int id);
	}
}