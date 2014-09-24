namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BrokerTypeForAllEmployees : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "BrokerType", c => c.Int(nullable: false));
            DropColumn("dbo.Broker", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Broker", "Type", c => c.Int(nullable: false));
            DropColumn("dbo.Employee", "BrokerType");
        }
    }
}
