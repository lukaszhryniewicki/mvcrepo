using FluentValidation.Attributes;
using FORUM_DYSKUSYJNE.Core.Models;
using FORUM_DYSKUSYJNE.Web.ViewModelValidators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FORUM_DYSKUSYJNE.Web.ViewModels
{
	[Validator(typeof(ForbiddenWordsValidator))]
	public class ForbiddenWordViewModel
	{

		public IEnumerable<ForbiddenWord> forbiddenWords { get; set; }

		public ForbiddenWordViewModel(IEnumerable<ForbiddenWord> words)
		{
			forbiddenWords = words;
		}

		public ForbiddenWordViewModel()
		{
				
		}
		public string word { get; set; }
	}
}