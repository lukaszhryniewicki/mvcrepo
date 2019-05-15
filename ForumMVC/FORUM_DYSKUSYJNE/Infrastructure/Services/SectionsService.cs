using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace FORUM_DYSKUSYJNE.Infrastructure.Services
{
	public class SectionsService : ISectionsService
	{
		private readonly IDataService _dataService;

		public SectionsService(IDataService dataService)
		{
			_dataService = dataService;
		}

		public void CreateSection(string name, string description, string selectedSection, string selectedGroup)
		{
			if(selectedSection != null)
			{

				var sectionId = Convert.ToInt32(selectedSection);

				var section = _dataService.GetDbSet<Section>()
					.Where(x => x.SectionId == sectionId)
					.SingleOrDefault();


				var sectionsAfter = _dataService.GetDbSet<Section>()
					.Where(x => x.Order >= section.Order)
					.ToList();

				foreach (var item in sectionsAfter)
				{
					item.Order = item.Order + 1;
				}

				var newSection = new Section()
				{
					Order = section.Order,
					Name = name,
					Description = description,
					GroupId = Convert.ToInt32(selectedGroup)
				};

				_dataService.GetDbSet<Section>().Add(newSection);

			}
			else
			{

				var newSection = new Section()
				{
					Order = GetTopOrder() + 1,
					Name = name,
					Description = description,
					GroupId = Convert.ToInt32(selectedGroup)
				};

				_dataService.GetDbSet<Section>().Add(newSection);
				
			}

			_dataService.SaveChanges();

		}
		public void DeleteSection(int id)
		{
			var section = _dataService.GetDbSet<Section>()
				.Where(x => x.SectionId == id)
				.SingleOrDefault();

			_dataService.GetDbSet<Section>().Remove(section);
			_dataService.SaveChanges();
		}

		public void EditSection(int editedSectionId, string selectedSection, string description, string name, Group group)
		{
			var editedSection = GetSectionById(editedSectionId);
			var selectedSectionId = Convert.ToInt16(selectedSection);
			//bez zmiany kolejnosci
			if (editedSection.SectionId == selectedSectionId)
			{
				editedSection.Group = group;
				editedSection.Description = description;
				editedSection.Name = name;
			}//dodaj przed
			else
			{

				var section = GetSectionById(selectedSectionId);

				_dataService.GetDbSet<Section>()
					.Where(x => x.Order >= section.Order)
					.ToList()
					.ForEach(x => x.Order = x.Order + 1);


				editedSection.Order = section.Order;
				editedSection.Group = group;
				editedSection.Description = description;
				editedSection.Name = name;

			}

			_dataService.SaveChanges();
		}

		public IEnumerable<Section> GetAllOrderedSections()
		{
			return _dataService.GetDbSet<Section>()
				.OrderBy(o => o.Order)
				.ToList();

		}

		public IEnumerable<Section> GetOrderedSections(int id)
		{
			return _dataService.GetDbSet<Section>()
				.Where(s => s.GroupId == id)
				.OrderBy(o => o.Order)
				.ToList();

		}

		public Section GetSectionById(int id)
		{
			return _dataService.GetDbSet<Section>()
				.Where(x => x.SectionId == id)
				.SingleOrDefault();
		}

		public IEnumerable<User> GetSectionModerators(int id)
		{
			return _dataService.GetDbSet<Section>()
				.Where(s => s.SectionId == id)
				.Select(u => u.User)
				.FirstOrDefault()
				.ToList();

		}

		public IEnumerable<Section> GetSectionsInGroup(int groupId)
		{
			return _dataService.GetDbSet<Section>()
				.Where(x => x.GroupId == groupId)
				.ToList();
		}

		public string GetSelectedSectionName(Section edit)
		{
			return _dataService.GetDbSet<Section>()
				.Where(g => g.GroupId == edit.GroupId && g.Order > edit.Order)
				.OrderBy(o => o.Order)
				.Select(s => s.Name)
				.FirstOrDefault();
		}

		public int GetTopOrder()
		{
			return _dataService.GetDbSet<Section>()
				.OrderByDescending(o => o.Order)
				.Select(o => o.Order)
				.First();
		}


	}
}