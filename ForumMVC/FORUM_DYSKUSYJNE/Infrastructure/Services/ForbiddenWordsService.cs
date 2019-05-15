using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace FORUM_DYSKUSYJNE.Infrastructure.Services
{
	public class ForbiddenWordsService : IForbiddenWordsService
	{
		public readonly IDataService _dataService;
		public ForbiddenWordsService(IDataService dataService)
		{
			_dataService = dataService;
		}
		public List<string> CheckForForbiddenWords(string text )
		{
			
			List<string> textArray, forbiddenWords = new List<string>();
			if(!string.IsNullOrEmpty(text))
			{

				textArray = text.Split(' ').ToList();

				var words = _dataService.GetDbSet<ForbiddenWord>().ToList();

				forbiddenWords = textArray
					.Where(x => words.Any(y => y.Word.Contains(x.ToLower())))
					.ToList();

			}
				return forbiddenWords;
		
		}

		public void CreateWord(string word)
		{
			var newWord = new ForbiddenWord()
			{
				Word = word
			};

			_dataService.GetDbSet<ForbiddenWord>().Add(newWord);
			_dataService.SaveChanges();
		}

		public void DeleteWord(int id)
		{
			var word = _dataService.GetDbSet<ForbiddenWord>()
				.Where(x => x.ForbiddenWordId == id)
				.SingleOrDefault();

			_dataService.GetDbSet<ForbiddenWord>().Remove(word);
			_dataService.SaveChanges();
		}

		public IEnumerable<ForbiddenWord> GetAllWords()
		{
			return _dataService.GetDbSet<ForbiddenWord>()
				.ToList();
		}

		public bool IsWordUnique(string word)
		{
			var unique=_dataService.GetDbSet<ForbiddenWord>()
				.Where(x=>x.Word == word)
				.SingleOrDefault();

			if (unique == null) return true;

			return false;	
		}
	}
}