namespace BuyArt.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "ShipCity", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Orders", "ShipAddress", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ShipAddress", c => c.String(maxLength: 100));
            AlterColumn("dbo.Orders", "ShipCity", c => c.String(maxLength: 30));
        }
    }
}
