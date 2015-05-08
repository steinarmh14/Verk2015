namespace FeedIt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Post : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "owner", c => c.String());
            AddColumn("dbo.Posts", "groupID", c => c.Int(nullable: false));
            DropTable("dbo.GroupPosts");
            DropTable("dbo.PostComments");
            DropTable("dbo.UserPosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPosts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        userID = c.String(),
                        postID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PostComments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        commentID = c.Int(nullable: false),
                        postID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GroupPosts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        groupID = c.Int(nullable: false),
                        postID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Posts", "groupID");
            DropColumn("dbo.Posts", "owner");
        }
    }
}
