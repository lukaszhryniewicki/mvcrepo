using FORUM_DYSKUSYJNE.Contracts;
using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Services
{
	public class ForbiddenWordsService : IForbiddenWordsService
	{
		private readonly IUnitOfWork _unitOfWork;
		public ForbiddenWordsService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public List<string> CheckForForbiddenWords(string text )
		{
			List<string> textArray, forbiddenWords = new List<string>();
			textArray = text.Split(' ').ToList();

			var words = _unitOfWork.Repository<ForbiddenWord>().GetAll();
			foreach (var word in textArray)
			{
				foreach (var forbidden in words)
				{
					if (word.ToLower().Contains(forbidden.Word.ToLower()))
					{
						forbiddenWords.Add(forbidden.Word); break;
					}
				}

			}
			return forbiddenWords;
		}
	}
}