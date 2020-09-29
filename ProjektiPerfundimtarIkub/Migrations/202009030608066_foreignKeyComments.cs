namespace ProjektiPerfundimtarIkub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignKeyComments : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropColumn("dbo.Comments", "ArtistId");
            RenameColumn(table: "dbo.Comments", name: "User_Id", newName: "ArtistId");
            AlterColumn("dbo.Comments", "ArtistId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "ArtistId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "ArtistId" });
            AlterColumn("dbo.Comments", "ArtistId", c => c.String());
            RenameColumn(table: "dbo.Comments", name: "ArtistId", newName: "User_Id");
            AddColumn("dbo.Comments", "ArtistId", c => c.String());
            CreateIndex("dbo.Comments", "User_Id");
        }
    }
}
