using System.Data.Entity.Core.Common.CommandTrees;

namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveManagerConstraint : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Broker", "ManagerID", "dbo.Manager");
        }
        
        public override void Down()
        {
            AddForeignKey("dbo.Broker", "ManagerID", "dbo.Manager");
        }
    }
}
