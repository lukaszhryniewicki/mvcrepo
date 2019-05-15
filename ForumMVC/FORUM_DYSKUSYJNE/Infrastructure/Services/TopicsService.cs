using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Infrastructure.Services
{
	public class TopicsServices : ITopicsService
	{
		private readonly IDataService _dataService;
		public TopicsServices(IDataService dataService)
		{
			 _dataService = dataService;
		}
		public IEnumerable<Topic> GetTopicsInSection(int sectionId)
		{
			return _dataService.GetDbSet<Topic>()
				.Where(x => x.SectionId == sectionId)
				.ToList();
				
		}
		public Topic GetTopicById(int id)
		{
			return _dataService.GetDbSet<Topic>()
				.Where(x => x.TopicId == id)
				.FirstOrDefault();
		}

		public int? GetTopicsSection(int id)
		{
			return _dataService.GetDbSet<Topic>()
				.Where(t => t.TopicId == id)
				.FirstOrDefault()
				.SectionId;
		}
		
		public void IncreaseTopicViewCount(int topicId)
		{
			if (HttpContext.Current.Session[topicId.ToString()] == null)
			{
				var topic = _dataService.GetDbSet<Topic>()
					.Where(x => x.TopicId == topicId)
					.SingleOrDefault();

				topic.ViewsCount++;
				_dataService.SaveChanges();
				HttpContext.Current.Session[topic.TopicId.ToString()] = true;
			}
		}

		public IEnumerable<Topic> GetSectionTopicsByQuery(string query, int sectionId)
		{
			 
			return	_dataService.GetDbSet<Topic>()
					.Where(x => x.SectionId == sectionId && x.Post.Any(y=>y.Content.Contains(query) || x.Name.Contains(query)))
					.ToList();

		}

		public int GetTopicIdByDateAndAuthor(DateTime createDate, int authorId)
		{
			return _dataService.GetDbSet<Topic>()
				.Where(x => x.CreateDate == createDate && x.AuthorId == authorId)
				.Select(x => x.TopicId)
				.SingleOrDefault();

		}

		public Topic CreateTopic(string topicName, int userId, int sectionId)
		{
			var topic = new Topic()
			{
				AuthorId = userId,
				LastModified = DateTime.Now,
				CreateDate = DateTime.Now,
				Name = topicName,
				SectionId = sectionId,
				Sticked = false,
				ViewsCount = 1,
			};
			HttpContext.Current.Session[topic.TopicId.ToString()] = true;
			_dataService.GetDbSet<Topic>().Add(topic);
			_dataService.SaveChanges();
			return topic;
		}

		public void DeleteTopic(int id)
		{
			var topic = _dataService.GetDbSet<Topic>()
				.Where(x => x.TopicId == id)
				.SingleOrDefault();

			_dataService.GetDbSet<Topic>().Remove(topic);
			_dataService.SaveChanges();
		}

		public void ToggleStickTopic(int id)
		{
			var topic = _dataService.GetDbSet<Topic>()
				.Where(x => x.TopicId == id)
				.SingleOrDefault();

			topic.Sticked = !topic.Sticked;

			_dataService.SaveChanges();
		}
	}
			
}