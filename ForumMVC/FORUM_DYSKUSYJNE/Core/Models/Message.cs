using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Core.Models
{
	public class Message
	{
		
		public int MessageId { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Content { get; set; }
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		[ForeignKey("SenderId")]
		public virtual User Sender { get; set; }
		[ForeignKey("ReceiverId")]
		public virtual User Receiver { get; set; }
		public DateTime SendDate { get; set; }
		[Required]
		public int BelongsTo { get; set; }
	}
}