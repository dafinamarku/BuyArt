namespace ProjektiPerfundimtarIkub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCartRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserCart_CartId", "dbo.Carts");
            DropIndex("dbo.AspNetUsers", new[] { "UserCart_CartId" });
            AddColumn("dbo.Carts", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Carts", "User_Id");
            AddForeignKey("dbo.Carts", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "UserCart_CartId");
            DropColumn("dbo.Carts", "ClientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "ClientId", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserCart_CartId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Carts", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Carts", new[] { "User_Id" });
            DropColumn("dbo.Carts", "User_Id");
            CreateIndex("dbo.AspNetUsers", "UserCart_CartId");
            AddForeignKey("dbo.AspNetUsers", "UserCart_CartId", "dbo.Carts", "CartId");
        }
    }
}
