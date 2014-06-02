namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AvatarUrlFix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employee", "AvatarUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "AvatarUrl", c => c.String());
        }
    }
}
