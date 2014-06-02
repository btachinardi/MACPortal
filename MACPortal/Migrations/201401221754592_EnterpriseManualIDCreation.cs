namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnterpriseManualIDCreation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enterprise", "EnterpriseID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enterprise", "EnterpriseID", c => c.Int(nullable: false, identity: true));
        }
    }
}
