namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdPartyCoordinators : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sale", "ThirdPartyCoordinatorID", c => c.Int(nullable: false));
            DropColumn("dbo.Sale", "ThirdPartyBroker");
            DropColumn("dbo.Sale", "ThirdPartyManager");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sale", "ThirdPartyManager", c => c.String());
            AddColumn("dbo.Sale", "ThirdPartyBroker", c => c.String());
            DropColumn("dbo.Sale", "ThirdPartyCoordinatorID");
        }
    }
}
