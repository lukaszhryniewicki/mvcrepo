using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace FORUM_DYSKUSYJNE.Core.Models
{
	public class Post
	{
		public int PostId { get; set; }
		[Required]
		public string Content { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime LastModified { get; set; }
		public int TopicId { get; set; }
		public virtual Topic Topic { get; set; }
		public virtual User User { get; set; }
		public int AttachmentId { get; set; }
		public virtual Attachment Attachment { get; set; }
		[ForeignKey("User")]
		public int AuthorId { get; set; }
	}
}