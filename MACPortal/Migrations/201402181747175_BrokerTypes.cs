namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BrokerTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Broker", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Broker", "Type");
        }
    }
}
