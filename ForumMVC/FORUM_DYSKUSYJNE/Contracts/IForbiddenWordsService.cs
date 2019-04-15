using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FORUM_DYSKUSYJNE.Contracts
{
	public interface IForbiddenWordsService
	{
		List<string> CheckForForbiddenWords(string text);
	}
}
