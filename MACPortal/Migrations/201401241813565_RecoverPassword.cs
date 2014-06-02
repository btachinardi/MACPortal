namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecoverPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "RecoverPasswordToken", c => c.String());
            AddColumn("dbo.UserProfile", "RecoverPasswordExpiration", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "RecoverPasswordExpiration");
            DropColumn("dbo.UserProfile", "RecoverPasswordToken");
        }
    }
}
