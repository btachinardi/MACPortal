using MACPortal.DAL;
using WebGrease.Css.Extensions;

namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActiveMembersProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "Active");
        }
    }
}
