namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddNotification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserNotifications",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    NotificationId = c.String(nullable: false, maxLength: 128),
                    IsRead = c.Boolean(nullable: false),
                    Notifiaction_Id = c.Guid(),
                })
                .PrimaryKey(t => new { t.UserId, t.NotificationId })
                .ForeignKey("dbo.Notifications", t => t.Notifiaction_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Notifiaction_Id);

            CreateTable(
                "dbo.Notifications",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    DateTime = c.DateTime(nullable: false),
                    Type = c.Int(nullable: false),
                    OriginalDateTime = c.DateTime(),
                    OriginalVenue = c.String(),
                    Gig_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gigs", t => t.Gig_Id, cascadeDelete: true)
                .Index(t => t.Gig_Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNotifications", "Notifiaction_Id", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "Gig_Id", "dbo.Gigs");
            DropIndex("dbo.Notifications", new[] { "Gig_Id" });
            DropIndex("dbo.UserNotifications", new[] { "Notifiaction_Id" });
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropTable("dbo.Notifications");
            DropTable("dbo.UserNotifications");
        }
    }
}