namespace BuyArt.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyArtworks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artworks",
                c => new
                    {
                        ArtworkId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 40),
                        Photo = c.String(),
                        Price = c.Double(nullable: false),
                        Width = c.Double(nullable: false),
                        Height = c.Double(nullable: false),
                        Description = c.String(),
                        CategoryId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        ArtistId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ArtworkId)
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.SubjectId)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Bio = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        ShipCity = c.String(maxLength: 30),
                        ShipAddress = c.String(maxLength: 100),
                        CustomerId = c.String(maxLength: 128),
                        ArtworkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId)
                .ForeignKey("dbo.Artworks", t => t.ArtworkId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ArtworkId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Styles",
                c => new
                    {
                        StyleId = c.Int(nullable: false, identity: true),
                        StyleName = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.StyleId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.SubjectId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CommentText = c.String(),
                        ArtistId = c.String(maxLength: 128),
                        ArtworkId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Artworks", t => t.ArtworkId)
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId)
                .Index(t => t.ArtistId)
                .Index(t => t.ArtworkId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ApplicationUserApplicationUsers",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ApplicationUser_Id1 })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id1)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1);
            
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
            
            CreateTable(
                "dbo.StyleArtworks",
                c => new
                    {
                        Style_StyleId = c.Int(nullable: false),
                        Artwork_ArtworkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Style_StyleId, t.Artwork_ArtworkId })
                .ForeignKey("dbo.Styles", t => t.Style_StyleId, cascadeDelete: true)
                .ForeignKey("dbo.Artworks", t => t.Artwork_ArtworkId, cascadeDelete: true)
                .Index(t => t.Style_StyleId)
                .Index(t => t.Artwork_ArtworkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Comments", "ArtistId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ArtworkId", "dbo.Artworks");
            DropForeignKey("dbo.Artworks", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Artworks", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.StyleArtworks", "Artwork_ArtworkId", "dbo.Artworks");
            DropForeignKey("dbo.StyleArtworks", "Style_StyleId", "dbo.Styles");
            DropForeignKey("dbo.Carts", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CartArtworks", "Artwork_ArtworkId", "dbo.Artworks");
            DropForeignKey("dbo.CartArtworks", "Cart_CartId", "dbo.Carts");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "ArtworkId", "dbo.Artworks");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserApplicationUsers", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Artworks", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.StyleArtworks", new[] { "Artwork_ArtworkId" });
            DropIndex("dbo.StyleArtworks", new[] { "Style_StyleId" });
            DropIndex("dbo.CartArtworks", new[] { "Artwork_ArtworkId" });
            DropIndex("dbo.CartArtworks", new[] { "Cart_CartId" });
            DropIndex("dbo.ApplicationUserApplicationUsers", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.ApplicationUserApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Comments", new[] { "ArtworkId" });
            DropIndex("dbo.Comments", new[] { "ArtistId" });
            DropIndex("dbo.Carts", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "ArtworkId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Artworks", new[] { "ArtistId" });
            DropIndex("dbo.Artworks", new[] { "SubjectId" });
            DropIndex("dbo.Artworks", new[] { "CategoryId" });
            DropTable("dbo.StyleArtworks");
            DropTable("dbo.CartArtworks");
            DropTable("dbo.ApplicationUserApplicationUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
            DropTable("dbo.Subjects");
            DropTable("dbo.Styles");
            DropTable("dbo.Carts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Artworks");
        }
    }
}
