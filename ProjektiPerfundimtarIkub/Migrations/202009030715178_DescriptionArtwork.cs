namespace ProjektiPerfundimtarIkub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptionArtwork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artworks", "Description", c => c.String(maxLength: 500));
            DropColumn("dbo.Artworks", "Decription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artworks", "Decription", c => c.String(maxLength: 500));
            DropColumn("dbo.Artworks", "Description");
        }
    }
}
