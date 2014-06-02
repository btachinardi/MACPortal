namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AvatarFieldsAdjustments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "AvatarMouth", c => c.Int());
            AddColumn("dbo.Employee", "AvatarAccessoryHead", c => c.Int());
            AddColumn("dbo.Employee", "AvatarAccessoryFace", c => c.Int());
            AddColumn("dbo.Employee", "AvatarAccessoryBody", c => c.Int());
            DropColumn("dbo.Employee", "AvatarAccessory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "AvatarAccessory", c => c.Int());
            DropColumn("dbo.Employee", "AvatarAccessoryBody");
            DropColumn("dbo.Employee", "AvatarAccessoryFace");
            DropColumn("dbo.Employee", "AvatarAccessoryHead");
            DropColumn("dbo.Employee", "AvatarMouth");
        }
    }
}
