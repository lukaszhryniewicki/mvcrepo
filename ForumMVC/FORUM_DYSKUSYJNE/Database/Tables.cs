using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Database
{

	public class Emoticon
	{
		public int EmoticonId { get; set; }
		public string Name { get; set; }
		public string SourceLink { get; set; }
	}

	public class ForbiddenWord
	{
		public int ForbiddenWordId { get; set; }
		public string Word { get; set; }
	}

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

	public class Announcement
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(30)]
		public string Content { get; set; }
		public DateTime CreateDate { get; set; }
		[Required]
		public DateTime ExpirationDate { get; set; }
	}

	public class Rank
	{
		public int Id { get; set; }
		public int Requirement { get; set; }
		public string RankName { get; set; }
	}

	public class Role
	{

		public int RoleId { get; set; }
		[Required]
		public string RoleName { get; set; }
		public virtual ICollection<User> User { get; set; }
	}

	public class Group
	{
		public int GroupId { get; set; }
		[Required]
		public string GroupName { get; set; }
		public virtual ICollection<Section> Section { get; set; }
	}

	public class Section
	{
		
		public int SectionId { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		public string IconName { get; set; }
		public int GroupId { get; set; }
		public int Order { get; set; }
		public Group Group { get; set; }
		public virtual ICollection<Topic> Topic { get; set; }
		public virtual ICollection<User> User { get; set; }
	}

	public class Topic
	{
		
		public int TopicId { get; set; }
		[Required]
		public int ViewsCount { get; set; }
		public int PostsCount { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public bool Sticked { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime LastModified { get; set; }
		public User User { get; set; }
		[ForeignKey("User")]
		public int AuthorId { get; set; }
		public virtual ICollection<Post> Post { get; set; }
		public int SectionId { get; set; }
		public Section Section { get; set; }
	}

	public class Attachment
	{

		public string AttachmentName { get; set; }
		public byte[] AttachmentData { get; set; }
		[Key,ForeignKey("Post")]
		public int PostId { get; set; }
		public virtual Post Post { get; set; }
	}

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
		public int BelongsTo { get; set; }
	}
}