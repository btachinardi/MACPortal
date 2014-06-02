namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAcceptSMSAndEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "AcceptSMS", c => c.String());
            AddColumn("dbo.Employee", "AcceptEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "AcceptEmail");
            DropColumn("dbo.Employee", "AcceptSMS");
        }
    }
}
