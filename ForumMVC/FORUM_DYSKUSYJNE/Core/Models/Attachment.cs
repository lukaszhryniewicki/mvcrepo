using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Core.Models
{

	public class Attachment
	{
		[Required]
		public string AttachmentName { get; set; }
		[Required]
		public byte[] AttachmentData { get; set; }
		[Key, ForeignKey("Post")]
		public int PostId { get; set; }
		public virtual Post Post { get; set; }
	}

}