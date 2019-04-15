using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace FORUM_DYSKUSYJNE.Database
{
	public partial class ForumDatabase : DbContext
	{
		public ForumDatabase(): base("name=ForumDatabase")
		{
		}
		
		public static ForumDatabase Create()
		{
			return new ForumDatabase();
		}
		public DbSet<ForbiddenWord> ForbiddenWords { get; set; }
		public DbSet<Emoticon> Emoticons { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Section> Sections { get; set; }
		public DbSet<Topic> Topics { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Message { get; set; }
		public DbSet<Rank> Ranks { get; set; }
		public DbSet<Announcement> Announcement { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{



	

			modelBuilder.Entity<Post>()
				.HasRequired(u => u.User)
				.WithMany(p => p.Post)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Topic>()
				.HasRequired(u => u.User)
				.WithMany(t => t.Topic)
				.WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.Receiver)
                .WithMany(t => t.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.Sender)
                .WithMany(t => t.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .WillCascadeOnDelete(false);

			modelBuilder.Entity<Attachment>()
				.HasKey(x => x.PostId)
				.HasRequired(x => x.Post)
				.WithOptional(x => x.Attachment)
				.WillCascadeOnDelete(true);

		}
	}
}