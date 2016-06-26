namespace Bitlink.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Click",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LinkId = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Link", t => t.LinkId, cascadeDelete: true)
                .Index(t => t.LinkId);
            
            CreateTable(
                "dbo.Link",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                        Hash = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Uid = c.Guid(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Error",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLink",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        LinkId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.LinkId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Link", t => t.LinkId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.LinkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Click", "LinkId", "dbo.Link");
            DropForeignKey("dbo.UserLink", "LinkId", "dbo.Link");
            DropForeignKey("dbo.UserLink", "UserId", "dbo.User");
            DropIndex("dbo.UserLink", new[] { "LinkId" });
            DropIndex("dbo.UserLink", new[] { "UserId" });
            DropIndex("dbo.Click", new[] { "LinkId" });
            DropTable("dbo.UserLink");
            DropTable("dbo.Error");
            DropTable("dbo.User");
            DropTable("dbo.Link");
            DropTable("dbo.Click");
        }
    }
}
