namespace FORUM_DYSKUSYJNE.Infrastructure.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Emoticons",
                c => new
                    {
                        EmoticonId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SourceLink = c.String(),
                    })
                .PrimaryKey(t => t.EmoticonId);
            
            CreateTable(
                "dbo.ForbiddenWords",
                c => new
                    {
                        ForbiddenWordId = c.Int(nullable: false, identity: true),
                        Word = c.String(),
                    })
                .PrimaryKey(t => t.ForbiddenWordId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        IconName = c.String(),
                        GroupId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SectionId)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicId = c.Int(nullable: false, identity: true),
                        ViewsCount = c.Int(nullable: false),
                        PostsCount = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Sticked = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TopicId)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .Index(t => t.AuthorId)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        TopicId = c.Int(nullable: false),
                        AttachmentId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .Index(t => t.TopicId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        AttachmentName = c.String(),
                        AttachmentData = c.Binary(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Location = c.String(nullable: false),
                        RankId = c.Int(nullable: false),
                        Avatar = c.Binary(),
                        ActivationCode = c.Guid(nullable: false),
                        ForgottenPasswordCode = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        BirthdayDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Ranks", t => t.RankId, cascadeDelete: true)
                .Index(t => t.RankId);
            
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Requirement = c.Int(nullable: false),
                        RankName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        SendDate = c.DateTime(nullable: false),
                        BelongsTo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Users", t => t.ReceiverId)
                .ForeignKey("dbo.Users", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_RoleId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_RoleId, t.User_UserId })
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Role_RoleId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.UserSections",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Section_SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Section_SectionId })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.Section_SectionId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Section_SectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Topics", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.UserSections", "Section_SectionId", "dbo.Sections");
            DropForeignKey("dbo.UserSections", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ReceiverId", "dbo.Users");
            DropForeignKey("dbo.Users", "RankId", "dbo.Ranks");
            DropForeignKey("dbo.Posts", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Attachments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Sections", "GroupId", "dbo.Groups");
            DropIndex("dbo.UserSections", new[] { "Section_SectionId" });
            DropIndex("dbo.UserSections", new[] { "User_UserId" });
            DropIndex("dbo.RoleUsers", new[] { "User_UserId" });
            DropIndex("dbo.RoleUsers", new[] { "Role_RoleId" });
            DropIndex("dbo.Messages", new[] { "ReceiverId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.Users", new[] { "RankId" });
            DropIndex("dbo.Attachments", new[] { "PostId" });
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            DropIndex("dbo.Posts", new[] { "TopicId" });
            DropIndex("dbo.Topics", new[] { "SectionId" });
            DropIndex("dbo.Topics", new[] { "AuthorId" });
            DropIndex("dbo.Sections", new[] { "GroupId" });
            DropTable("dbo.UserSections");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.Roles");
            DropTable("dbo.Messages");
            DropTable("dbo.Ranks");
            DropTable("dbo.Users");
            DropTable("dbo.Attachments");
            DropTable("dbo.Posts");
            DropTable("dbo.Topics");
            DropTable("dbo.Sections");
            DropTable("dbo.Groups");
            DropTable("dbo.ForbiddenWords");
            DropTable("dbo.Emoticons");
            DropTable("dbo.Announcements");
        }
    }
}
