namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnterprisesCodes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enterprise", "Code", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enterprise", "Code");
        }
    }
}
