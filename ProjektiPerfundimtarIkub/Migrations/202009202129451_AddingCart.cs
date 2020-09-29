namespace ProjektiPerfundimtarIkub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        ClientId = c.String(),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateTable(
                "dbo.CartArtworks",
                c => new
                    {
                        Cart_CartId = c.Int(nullable: false),
                        Artwork_ArtworkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Cart_CartId, t.Artwork_ArtworkId })
                .ForeignKey("dbo.Carts", t => t.Cart_CartId, cascadeDelete: true)
                .ForeignKey("dbo.Artworks", t => t.Artwork_ArtworkId, cascadeDelete: true)
                .Index(t => t.Cart_CartId)
                .Index(t => t.Artwork_ArtworkId);
            
            AddColumn("dbo.AspNetUsers", "UserCart_CartId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "UserCart_CartId");
            AddForeignKey("dbo.AspNetUsers", "UserCart_CartId", "dbo.Carts", "CartId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserCart_CartId", "dbo.Carts");
            DropForeignKey("dbo.CartArtworks", "Artwork_ArtworkId", "dbo.Artworks");
            DropForeignKey("dbo.CartArtworks", "Cart_CartId", "dbo.Carts");
            DropIndex("dbo.CartArtworks", new[] { "Artwork_ArtworkId" });
            DropIndex("dbo.CartArtworks", new[] { "Cart_CartId" });
            DropIndex("dbo.AspNetUsers", new[] { "UserCart_CartId" });
            DropColumn("dbo.AspNetUsers", "UserCart_CartId");
            DropTable("dbo.CartArtworks");
            DropTable("dbo.Carts");
        }
    }
}
