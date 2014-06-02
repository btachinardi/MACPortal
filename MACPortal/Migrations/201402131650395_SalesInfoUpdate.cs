namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesInfoUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sale", "NumberOfCoordinators", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sale", "NumberOfCoordinators");
        }
    }
}
