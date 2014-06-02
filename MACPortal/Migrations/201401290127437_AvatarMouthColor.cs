namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AvatarMouthColor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "AvatarMouthColor", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "AvatarMouthColor");
        }
    }
}
