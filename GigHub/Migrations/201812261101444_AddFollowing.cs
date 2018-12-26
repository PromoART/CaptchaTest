namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FollowAttendances",
                c => new
                    {
                        FollowerId = c.String(nullable: false, maxLength: 128),
                        FolloweeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.FolloweeId })
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerId)
                .ForeignKey("dbo.AspNetUsers", t => t.FolloweeId)
                .Index(t => t.FollowerId)
                .Index(t => t.FolloweeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FollowAttendances", "FolloweeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowAttendances", "FollowerId", "dbo.AspNetUsers");
            DropIndex("dbo.FollowAttendances", new[] { "FolloweeId" });
            DropIndex("dbo.FollowAttendances", new[] { "FollowerId" });
            DropTable("dbo.FollowAttendances");
        }
    }
}
