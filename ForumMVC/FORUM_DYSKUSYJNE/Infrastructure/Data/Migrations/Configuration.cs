namespace FORUM_DYSKUSYJNE.Infrastructure.Data
{
	using FORUM_DYSKUSYJNE.Core.Models;
	using System;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<ForumDatabase>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			this.MigrationsDirectory = @"Infrastructure\Data\Migrations";
		}

		protected override void Seed(ForumDatabase context)
		{
			
		
			var admin = context.Users.Where(u => u.Username == "Admin").FirstOrDefault();

			var roles = context.Roles.FirstOrDefault();
			var emoticons = context.Emoticons.FirstOrDefault();

			var ranks = context.Ranks.FirstOrDefault();
			if (roles == null)
			{
				var role1 = new Role()
				{
					RoleName = "Admin"
				};
			/*	var role2 = new Role()
				{
					RoleName = "Moderator"
				};
			*/
				var role3 = new Role()
				{
					RoleName = "User"
				};
				context.Roles.Add(role1);
				//context.Roles.Add(role2);
				context.Roles.Add(role3);
				
			}

			if (ranks==null)
			{
				var rank1 = new Rank()
				{
					RankName = "New member",
					Requirement =0,
				};
				var rank2 = new Rank()
				{
					RankName = "Junior member",
					Requirement =5,
				};
				var rank3 = new Rank()
				{
					RankName = "Member",
					Requirement =20,
				};
				var rank4 = new Rank()
				{
					RankName = "Active member",
					Requirement =100,
				};

				context.Ranks.Add(rank1);
				context.Ranks.Add(rank2);
				context.Ranks.Add(rank3);
				context.Ranks.Add(rank4);
				context.SaveChanges();

			}

			if (admin == null)
			{
				var rank = context.Ranks.Where(x => x.RankName == "Active member").First();
				var user = new User()
				{
					Username = "Admin",
					Name = "Maciej",
					Email = "alienown@o2.pl",
					Password = "asdasd",
					Location = "Poland",
					BirthdayDate = new DateTime(1990, 05, 05),
					IsActive = true,
					Avatar = null,
					ActivationCode = new Guid(),
					Rank = rank

				};
				var role = context.Roles.ToList();
				user.Role = role;
				context.Users.Add(user);
				

			}
			var groups = context.Groups.FirstOrDefault();
			if(groups== null)
			{

				var group1 = new Group()
				{
					GroupName = "SUPPORT"
				};
				var group2 = new Group()
				{
					GroupName = "COMMUNITY"
				};
				var group3 = new Group()
				{
					GroupName = "OFF TOPIC"
				};
				context.Groups.Add(group1);
				context.Groups.Add(group2);
				context.Groups.Add(group3);

			}

			var sections = context.Sections.FirstOrDefault();
			if(sections== null)
			{
				var section1 = new Section()
				{
					Name = "MEMES",
					Description = "Here you can post your highly creative memes",
					GroupId = 6,
					Order = 1,
					IconName="memes.png"
				};
				var section2 = new Section()
				{

					Name = "HOBBY",
					Description = "Talk about your interesting hobbies",
					GroupId = 6,
					Order = 2,
					IconName="hobby.png"
				};
				var section3 = new Section()
				{
					Name = "GENERAL DISCUSSION",
					Description = "Discuss Heroes of the Storm",
					GroupId = 5,
					Order = 1,
					IconName="general.png"
				};
				var section4 = new Section()
				{
					Name = "COMPETITIVE DISCUSSTION",
					Description = "Discuss Heroes of the Storm esports, tournaments, teams, and competitive play ",
					GroupId = 5,
					Order = 2,
					IconName="competitive.png"
				};
				var section5 = new Section()
				{
					Name = "LOOKING FOR GROUP",
					Description = "Looking to play with a party or wanting to promote the one you’re in? This is the place.",
					GroupId = 5,
					Order = 3,
					IconName="looking.png"
				};
				var section6 = new Section()
				{
					Name = "FEEDBACK",
					Description = "Share and discuss all feedback from Heroes of the Storm ",
					GroupId = 5,
					Order = 4,
					IconName="feedback.png"
				};
				var section7 = new Section()
				{
					Name = "COMMUNITY CREATIONS",
					Description = "Share and discuss artwork, cosplay, guides and other creations made by the Heroes of the Storm community.",
					GroupId = 5,
					Order = 5,
					IconName="community.png"
				};

				var section8 = new Section()
				{
					Name = "TECHNICAL SUPPORT",
					Description = "For problems installing, patching or playing the Heroes of the Storm, please contact us here. ",
					GroupId = 4,
					Order = 1,
					IconName="support.png"
				};
				var section9 = new Section()
				{
					Name = "GAMING AND HARDWARE",
					Description = "Want to talk about a non-Blizzard games, ask for PC hardware advice or event discuss the latest innovations in science? Come on in.",
					GroupId = 6,
					Order = 3,
					IconName="gaming.png"
				};
				var section10 = new Section()
				{
					Name = "MEDIA AND LITERATURE",
					Description = "They inspire entertain and delight – come here to chat about movies, TV, music and literature.",
					GroupId = 6,
					Order = 4,
					IconName="media.png"
				};

				context.Sections.Add(section1);
				context.Sections.Add(section2);
				context.Sections.Add(section3);
				context.Sections.Add(section4);
				context.Sections.Add(section5);
				context.Sections.Add(section6);
				context.Sections.Add(section7);
				context.Sections.Add(section8);
				context.Sections.Add(section9);
				context.Sections.Add(section10);

			}
			if (context.Topics.Count() == 0)
			{
				var topic = new Topic()
				{
					AuthorId = 1,
					Name = "test",
					LastModified = DateTime.Now,
					CreateDate = DateTime.Now,
					PostsCount = 0,
					SectionId = 2,
					Sticked = false,
					ViewsCount = 0,
				};
				context.Topics.Add(topic);
				context.SaveChanges();
			}
			if (emoticons==null)
			{
				var emote1 = new Emoticon()
				{
					Name="Kreygasm",
					SourceLink= "https://static-cdn.jtvnw.net/emoticons/v1/41/1.0",
				};

				var emote2 = new Emoticon()
				{

					Name ="cmonBruh",
					SourceLink = "https://static-cdn.jtvnw.net/emoticons/v1/84608/1.0",
				};

				var emote3 = new Emoticon()
				{

					Name ="Murky",
					SourceLink = "https://d1u5p3l4wpay3k.cloudfront.net/hots_es_gamepedia/b/bc/Murky_Happy_Emoji.png?version=305199b68c28fc73fc209659c1551f93",
				};

				var emote4 = new Emoticon()
				{

					Name ="Jebaited",
					SourceLink = "https://static-cdn.jtvnw.net/emoticons/v1/114836/1.0",
				};
				var emote5 = new Emoticon()
				{
					Name = "PJSalt",
					SourceLink = "https://static-cdn.jtvnw.net/emoticons/v1/36/1.0"
				};
				context.Emoticons.Add(emote1);
				context.Emoticons.Add(emote2);
				context.Emoticons.Add(emote3);
				context.Emoticons.Add(emote4);
				context.Emoticons.Add(emote5);
			}

			context.SaveChanges();

		}
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.
	}
}

