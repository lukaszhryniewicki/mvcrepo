using FORUM_DYSKUSYJNE.Contracts;
using FORUM_DYSKUSYJNE.DataAccess;
using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.SharedMethods
{
	/// <summary>
	/// Wspoldzielona metoda wykorzystywana w kilku kontrolerach do sprawdzania czy w poscie znajduja sie emotki.
	/// </summary>
	public static class FindEmote
	{

		public static string FindEmotes(string content,IUnitOfWork unitOfWork)
		{

			var emotes = unitOfWork.Repository<Emoticon>().GetAll();

			foreach (var emote in emotes)
			{

				var substring = string.Format("<img src=\"{0}\" width=\"25\" height=\"28\"  \" />", emote.SourceLink);
				content = content.Replace(emote.Name, substring);

			}
			return content;
		}
	}
}