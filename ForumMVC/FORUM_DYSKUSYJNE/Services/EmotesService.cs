using FORUM_DYSKUSYJNE.Contracts;
using FORUM_DYSKUSYJNE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Services
{
	public class EmotesService : IEmotesService
	{
		public readonly IUnitOfWork _unitOfWork;
		public EmotesService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public string FindEmotes(string text)
		{
			var emotes = _unitOfWork.Repository<Emoticon>().GetAll();

			foreach (var emote in emotes)
			{

				var substring = string.Format("<img src=\"{0}\" width=\"25\" height=\"28\"  \" />", emote.SourceLink);
				text = text.Replace(emote.Name, substring);

			}
			return text;
		}
	}
}