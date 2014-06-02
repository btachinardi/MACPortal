namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvancedAvatarSupport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "AvatarClothes", c => c.Int());
            AddColumn("dbo.Employee", "AvatarClothesColor", c => c.Int());
            AddColumn("dbo.Employee", "AvatarEyes", c => c.Int());
            AddColumn("dbo.Employee", "AvatarEyesColor", c => c.Int());
            AddColumn("dbo.Employee", "AvatarNose", c => c.Int());
            AddColumn("dbo.Employee", "AvatarEars", c => c.Int());
            AddColumn("dbo.Employee", "AvatarAccessory", c => c.Int());
            AddColumn("dbo.Employee", "AvatarUrl", c => c.String());
            DropColumn("dbo.Employee", "AvatarCloth");
            DropColumn("dbo.Employee", "Accessory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "Accessory", c => c.Int());
            AddColumn("dbo.Employee", "AvatarCloth", c => c.Int());
            DropColumn("dbo.Employee", "AvatarUrl");
            DropColumn("dbo.Employee", "AvatarAccessory");
            DropColumn("dbo.Employee", "AvatarEars");
            DropColumn("dbo.Employee", "AvatarNose");
            DropColumn("dbo.Employee", "AvatarEyesColor");
            DropColumn("dbo.Employee", "AvatarEyes");
            DropColumn("dbo.Employee", "AvatarClothesColor");
            DropColumn("dbo.Employee", "AvatarClothes");
        }
    }
}
