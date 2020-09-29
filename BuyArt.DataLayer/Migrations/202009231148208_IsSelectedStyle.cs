namespace BuyArt.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsSelectedStyle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Styles", "IsSelected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Styles", "IsSelected");
        }
    }
}
