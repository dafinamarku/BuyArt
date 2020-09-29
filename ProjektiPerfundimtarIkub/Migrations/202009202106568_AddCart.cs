namespace ProjektiPerfundimtarIkub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCart : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "ShipCity", c => c.String(maxLength: 30));
            AlterColumn("dbo.Orders", "ShipAddress", c => c.String(maxLength: 100));
            DropColumn("dbo.Orders", "Quantity");
            DropColumn("dbo.Orders", "OrderStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "ShipAddress", c => c.String());
            AlterColumn("dbo.Orders", "ShipCity", c => c.String());
        }
    }
}
