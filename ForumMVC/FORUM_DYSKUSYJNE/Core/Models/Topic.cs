using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FORUM_DYSKUSYJNE.Core.Models
{

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
		public virtual User User { get; set; }
		[ForeignKey("User")]
		public int AuthorId { get; set; }
		public virtual ICollection<Post> Post { get; set; }
		public int SectionId { get; set; }
		public Section Section { get; set; }
	}

}