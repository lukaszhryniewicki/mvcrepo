using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FORUM_DYSKUSYJNE.Core.Contracts
{
	public interface ISectionsService
	{
		IEnumerable<User> GetSectionModerators(int id);
		int GetTopOrder();
		IEnumerable<Section> GetAllOrderedSections();
		IEnumerable<Section> GetOrderedSections(int id);
		string GetSelectedSectionName(Section edit);
		void CreateSection(string name, string description, string selectedSection, string selectedGroup);
		void DeleteSection(int id);
		Section GetSectionById(int id);
		IEnumerable<Section> GetSectionsInGroup(int groupId);
		void EditSection(int editedSectionId, string selectedSection, string description, string name, Group group);	}
}
