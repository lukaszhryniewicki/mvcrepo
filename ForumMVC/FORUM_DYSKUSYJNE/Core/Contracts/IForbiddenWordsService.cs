using FORUM_DYSKUSYJNE.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORUM_DYSKUSYJNE.Core.Contracts
{
	public interface IForbiddenWordsService
	{
		List<string> CheckForForbiddenWords(string text);
		void DeleteWord(int id);
		IEnumerable<ForbiddenWord> GetAllWords();
		bool IsWordUnique(string word);
		void CreateWord(string word);
	}
}
