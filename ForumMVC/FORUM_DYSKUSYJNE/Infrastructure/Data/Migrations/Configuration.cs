namespace FORUM_DYSKUSYJNE.Infrastructure.Data
{
	using FORUM_DYSKUSYJNE.Core.Models;
	using System;
	using System.Collections.Generic;
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

			


				var role1 = new Role()
				{
					RoleName = "Admin"
				};
				var role2 = new Role()
				{
					RoleName = "Moderator"
				};
				var role3 = new Role()
				{
					RoleName = "User"
				};
				context.Roles.AddOrUpdate(x=>x.RoleName,role1);
				context.Roles.AddOrUpdate(x=>x.RoleName,role2);
				context.Roles.AddOrUpdate(x => x.RoleName, role2);



				var rank4 = new Rank()
				{
					RankName = "Active member",
					Requirement = 100,
				};

				context.Ranks.AddOrUpdate(x=>x.RankName,
					new Rank()
					{
						RankName = "New member",
						Requirement = 0,
					}, new Rank()
					{
						RankName = "Junior member",
						Requirement = 5,
					}, new Rank()
					{
						RankName = "Member",
						Requirement = 20,
					}, new Rank()
					{
						RankName = "Active memberr",
						Requirement = 100,
					},
					rank4
					);


				context.Users.AddOrUpdate(x=>x.Username, 
					new User()
				{
					UserId = 1,
					Username = "Admin",
					Name = "Maciej",
					Email = "alienown@o2.pl",
					Password = "asdasd",
					Location = "Poland",
					BirthdayDate = new DateTime(1990, 05, 05),
					IsActive = true,
					Avatar = null,
					ActivationCode = new Guid(),
					Rank = rank4,
					Role = new List<Role>() { role1, role2, role3 }

				}
					);


				var group1 = new Group()
				{
					GroupId=1,
					GroupName = "SUPPORT"
				};
				var group2 = new Group()
				{
					GroupId=2,
					GroupName = "COMMUNITY"
				};
				var group3 = new Group()
				{
					GroupId=3,
					GroupName = "OFF TOPIC"
				};
				context.Groups.AddOrUpdate(x=>x.GroupName,
					new Group()
					{
						GroupName = "SUPPORT"
					},
					new Group()
					{
						GroupName = "COMMUNITY"
					},
					new Group()
					{
						GroupName = "OFF TOPIC"
					}
					);


				var section1 = new Section()
				{
					SectionId=1,
					Name = "MEMES",
					Description = "Here you can post your highly creative memes",
					GroupId = 3,
					Order = 1,
					IconName = "memes.png"
				};
				var section2 = new Section()
				{
					SectionId = 2,
					Name = "HOBBY",
					Description = "Talk about your interesting hobbies",
					GroupId = 3,
					Order = 2,
					IconName = "hobby.png"
				};
				var section3 = new Section()
				{
					SectionId = 3,
					Name = "GENERAL DISCUSSION",
					Description = "Discuss Heroes of the Storm",
					GroupId = 2,
					Order = 1,
					IconName = "general.png"
				};
				var section4 = new Section()
				{
					SectionId = 4,
					Name = "COMPETITIVE DISCUSSTION",
					Description = "Discuss Heroes of the Storm esports, tournaments, teams, and competitive play ",
					GroupId = 2,
					Order = 2,
					IconName = "competitive.png"
				};
				var section5 = new Section()
				{
					SectionId = 5,
					Name = "LOOKING FOR GROUP",
					Description = "Looking to play with a party or wanting to promote the one you’re in? This is the place.",
					GroupId = 2,
					Order = 3,
					IconName = "looking.png"
				};
				var section6 = new Section()
				{
					SectionId = 6,
					Name = "FEEDBACK",
					Description = "Share and discuss all feedback from Heroes of the Storm ",
					GroupId = 2,
					Order = 4,
					IconName = "feedback.png"
				};
				var section7 = new Section()
				{
					SectionId = 7,
					Name = "COMMUNITY CREATIONS",
					Description = "Share and discuss artwork, cosplay, guides and other creations made by the Heroes of the Storm community.",
					GroupId = 2,
					Order = 5,
					IconName = "community.png"
				};

				var section8 = new Section()
				{
					SectionId = 8,
					Name = "TECHNICAL SUPPORT",
					Description = "For problems installing, patching or playing the Heroes of the Storm, please contact us here. ",
					GroupId = 1,
					Order = 1,
					IconName = "support.png"
				};
				var section9 = new Section()
				{
					SectionId = 9,
					Name = "GAMING AND HARDWARE",
					Description = "Want to talk about a non-Blizzard games, ask for PC hardware advice or event discuss the latest innovations in science? Come on in.",
					GroupId = 3,
					Order = 3,
					IconName = "gaming.png"
				};
				var section10 = new Section()
				{
					SectionId = 10,
					Name = "MEDIA AND LITERATURE",
					Description = "They inspire entertain and delight – come here to chat about movies, TV, music and literature.",
					GroupId = 3,
					Order = 4,
					IconName = "media.png"
				};

				context.Sections.AddOrUpdate(x=>x.Name, section1);
				context.Sections.AddOrUpdate(x => x.Name, section2);
				context.Sections.AddOrUpdate(x => x.Name, section3);
				context.Sections.AddOrUpdate(x => x.Name, section4);
				context.Sections.AddOrUpdate(x => x.Name, section5);
				context.Sections.AddOrUpdate(x => x.Name, section6);
				context.Sections.AddOrUpdate(x => x.Name, section7);
				context.Sections.AddOrUpdate(x => x.Name, section8);
				context.Sections.AddOrUpdate(x => x.Name, section9);
				context.Sections.AddOrUpdate(x => x.Name, section10);





				var emote1 = new Emoticon()
				{
					EmoticonId=1,
					Name = "Kreygasm",
					SourceLink = "https://static-cdn.jtvnw.net/emoticons/v1/41/1.0",
				};

				var emote2 = new Emoticon()
				{
					EmoticonId = 2,
					Name = "cmonBruh",
					SourceLink = "https://static-cdn.jtvnw.net/emoticons/v1/84608/1.0",
				};

				var emote3 = new Emoticon()
				{
					EmoticonId = 3,
					Name = "Murky",
					SourceLink = "https://d1u5p3l4wpay3k.cloudfront.net/hots_es_gamepedia/b/bc/Murky_Happy_Emoji.png?version=305199b68c28fc73fc209659c1551f93",
				};

				var emote4 = new Emoticon()
				{
					EmoticonId = 4,
					Name = "Jebaited",
					SourceLink = "https://static-cdn.jtvnw.net/emoticons/v1/114836/1.0",
				};
				var emote5 = new Emoticon()
				{
					EmoticonId = 5,
					Name = "PJSalt",
					SourceLink = "https://static-cdn.jtvnw.net/emoticons/v1/36/1.0"
				};
				context.Emoticons.AddOrUpdate(x=>x.Name, emote1);
				context.Emoticons.AddOrUpdate(x => x.Name, emote2);
				context.Emoticons.AddOrUpdate(x => x.Name, emote3);
				context.Emoticons.AddOrUpdate(x => x.Name, emote4);
				context.Emoticons.AddOrUpdate(x => x.Name, emote5);

			context.SaveChanges();

		}
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.
	}
}

