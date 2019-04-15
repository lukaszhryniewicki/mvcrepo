using FORUM_DYSKUSYJNE.Database;
using FORUM_DYSKUSYJNE.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FORUM_DYSKUSYJNE.Contracts;

namespace FORUM_DYSKUSYJNE.SharedMethods
{

	public static class CheckForForbiddenWords
	{
		public static List<string> CheckForForbiddenWord(string text,IUnitOfWork unitOfWork)
		{

			List<string> textArray, forbiddenWords = new List<string>();
			textArray = text.Split(' ').ToList();
			var words = unitOfWork.Repository<ForbiddenWord>().GetAll();
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