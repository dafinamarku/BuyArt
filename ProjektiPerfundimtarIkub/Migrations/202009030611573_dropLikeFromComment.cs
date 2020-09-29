namespace ProjektiPerfundimtarIkub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropLikeFromComment : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comments", "Like");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Like", c => c.Boolean(nullable: false));
        }
    }
}
