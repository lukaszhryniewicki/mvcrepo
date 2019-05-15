
using FluentValidation.Attributes;
using FORUM_DYSKUSYJNE.Core.ModelValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Core.Models
{
	[Validator(typeof(AnnouncementValidator))]
	public class Announcement
	{
		public int Id { get; set; }

		public string Content { get; set; }

		public DateTime CreateDate { get; set; }

		public DateTime ExpirationDate { get; set; }
	}
}