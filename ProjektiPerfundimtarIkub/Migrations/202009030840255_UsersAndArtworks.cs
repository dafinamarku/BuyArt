namespace ProjektiPerfundimtarIkub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersAndArtworks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artworks", "Width", c => c.Double(nullable: false));
            AddColumn("dbo.Artworks", "Height", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "Bio", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Bio");
            DropColumn("dbo.Artworks", "Height");
            DropColumn("dbo.Artworks", "Width");
        }
    }
}
