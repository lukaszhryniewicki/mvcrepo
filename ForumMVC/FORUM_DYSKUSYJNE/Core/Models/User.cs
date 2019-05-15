using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Core.Models
{

	public class User
	{

		public int UserId { get; set; }
		[Required]
		[MaxLength(10)]
		public string Username { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Location { get; set; }
		public int RankId { get; set; }
		public virtual Rank Rank { get; set; }
		public byte[] Avatar { get; set; }
		public Guid ActivationCode { get; set; }
		public string ForgottenPasswordCode { get; set; }
		public bool IsActive { get; set; }
		[Required]
		[DataType(DataType.Date)]
		public DateTime BirthdayDate { get; set; }
		public virtual ICollection<Role> Role { get; set; }
		public virtual ICollection<Post> Post { get; set; }
		public virtual ICollection<Topic> Topic { get; set; }
		public virtual ICollection<Message> ReceivedMessages { get; set; }
		public virtual ICollection<Message> SentMessages { get; set; }
		public virtual ICollection<Section> Section { get; set; }
	}

}