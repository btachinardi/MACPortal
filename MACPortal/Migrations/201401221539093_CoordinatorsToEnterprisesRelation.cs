namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoordinatorsToEnterprisesRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enterprise", "CoordinatorID", "dbo.Coordinator");
            DropIndex("dbo.Enterprise", new[] { "CoordinatorID" });
            CreateTable(
                "dbo.EnterpriseCoordinator",
                c => new
                    {
                        Enterprise_EnterpriseID = c.Int(nullable: false),
                        Coordinator_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Enterprise_EnterpriseID, t.Coordinator_UserID })
                .ForeignKey("dbo.Enterprise", t => t.Enterprise_EnterpriseID, cascadeDelete: true)
                .ForeignKey("dbo.Coordinator", t => t.Coordinator_UserID, cascadeDelete: true)
                .Index(t => t.Enterprise_EnterpriseID)
                .Index(t => t.Coordinator_UserID);
            
            DropColumn("dbo.Enterprise", "CoordinatorID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enterprise", "CoordinatorID", c => c.Int(nullable: false));
            DropForeignKey("dbo.EnterpriseCoordinator", "Coordinator_UserID", "dbo.Coordinator");
            DropForeignKey("dbo.EnterpriseCoordinator", "Enterprise_EnterpriseID", "dbo.Enterprise");
            DropIndex("dbo.EnterpriseCoordinator", new[] { "Coordinator_UserID" });
            DropIndex("dbo.EnterpriseCoordinator", new[] { "Enterprise_EnterpriseID" });
            DropTable("dbo.EnterpriseCoordinator");
            CreateIndex("dbo.Enterprise", "CoordinatorID");
            AddForeignKey("dbo.Enterprise", "CoordinatorID", "dbo.Coordinator", "UserID");
        }
    }
}
