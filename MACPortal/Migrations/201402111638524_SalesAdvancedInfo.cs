namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesAdvancedInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sale", "SaleDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Sale", "Style", c => c.Int(nullable: false));
            AddColumn("dbo.Sale", "Unit", c => c.String());
            AddColumn("dbo.Sale", "Tower", c => c.String());
            AddColumn("dbo.Sale", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Sale", "ThirdPartyBroker", c => c.String());
            AddColumn("dbo.Sale", "ThirdPartyManager", c => c.String());
            AddColumn("dbo.Sale", "OnlineCoordinatorID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sale", "OnlineCoordinatorID");
            DropColumn("dbo.Sale", "ThirdPartyManager");
            DropColumn("dbo.Sale", "ThirdPartyBroker");
            DropColumn("dbo.Sale", "Type");
            DropColumn("dbo.Sale", "Tower");
            DropColumn("dbo.Sale", "Unit");
            DropColumn("dbo.Sale", "Style");
            DropColumn("dbo.Sale", "SaleDate");
        }
    }
}
