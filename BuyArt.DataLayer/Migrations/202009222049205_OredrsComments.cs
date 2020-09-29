namespace BuyArt.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OredrsComments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "ArtworkId", "dbo.Artworks");
            DropIndex("dbo.Comments", new[] { "ArtworkId" });
            RenameColumn(table: "dbo.Comments", name: "ArtistId", newName: "AuthorId");
            RenameIndex(table: "dbo.Comments", name: "IX_ArtistId", newName: "IX_AuthorId");
            AddColumn("dbo.Artworks", "AvailabilityStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "OrderStatus", c => c.String());
            AddColumn("dbo.Orders", "RequiredDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "DeliveredDate", c => c.DateTime());
            AddColumn("dbo.Comments", "CommentOrLike", c => c.String());
            AddColumn("dbo.Comments", "Like", c => c.Boolean());
            AlterColumn("dbo.Comments", "ArtworkId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "ArtworkId");
            AddForeignKey("dbo.Comments", "ArtworkId", "dbo.Artworks", "ArtworkId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ArtworkId", "dbo.Artworks");
            DropIndex("dbo.Comments", new[] { "ArtworkId" });
            AlterColumn("dbo.Comments", "ArtworkId", c => c.Int());
            DropColumn("dbo.Comments", "Like");
            DropColumn("dbo.Comments", "CommentOrLike");
            DropColumn("dbo.Orders", "DeliveredDate");
            DropColumn("dbo.Orders", "RequiredDate");
            DropColumn("dbo.Orders", "OrderStatus");
            DropColumn("dbo.Artworks", "AvailabilityStatus");
            RenameIndex(table: "dbo.Comments", name: "IX_AuthorId", newName: "IX_ArtistId");
            RenameColumn(table: "dbo.Comments", name: "AuthorId", newName: "ArtistId");
            CreateIndex("dbo.Comments", "ArtworkId");
            AddForeignKey("dbo.Comments", "ArtworkId", "dbo.Artworks", "ArtworkId");
        }
    }
}
