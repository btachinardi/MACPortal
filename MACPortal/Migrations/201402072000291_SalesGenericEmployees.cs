namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesGenericEmployees : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EnterpriseCoordinator", newName: "CoordinatorEnterprise");
            DropForeignKey("dbo.Sale", "BrokerID", "dbo.Broker");
            DropForeignKey("dbo.Sale", "CoordinatorID", "dbo.Coordinator");
            DropForeignKey("dbo.Sale", "ManagerID", "dbo.Manager");
            DropIndex("dbo.Sale", new[] { "BrokerID" });
            DropIndex("dbo.Sale", new[] { "CoordinatorID" });
            DropIndex("dbo.Sale", new[] { "ManagerID" });
            CreateTable(
                "dbo.SaleEmployee",
                c => new
                    {
                        Sale_SaleID = c.Int(nullable: false),
                        Employee_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sale_SaleID, t.Employee_UserID })
                .ForeignKey("dbo.Sale", t => t.Sale_SaleID, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.Employee_UserID, cascadeDelete: true)
                .Index(t => t.Sale_SaleID)
                .Index(t => t.Employee_UserID);
            
            AddColumn("dbo.Employee", "TempPoints", c => c.Int(nullable: false));
            DropColumn("dbo.Sale", "CoordinatorID");
            DropColumn("dbo.Broker", "TempPoints");
            DropColumn("dbo.Manager", "TempPoints");
            DropColumn("dbo.Coordinator", "TempPoints");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Coordinator", "TempPoints", c => c.Int(nullable: false));
            AddColumn("dbo.Manager", "TempPoints", c => c.Int(nullable: false));
            AddColumn("dbo.Broker", "TempPoints", c => c.Int(nullable: false));
            AddColumn("dbo.Sale", "CoordinatorID", c => c.Int(nullable: false));
            DropForeignKey("dbo.SaleEmployee", "Employee_UserID", "dbo.Employee");
            DropForeignKey("dbo.SaleEmployee", "Sale_SaleID", "dbo.Sale");
            DropIndex("dbo.SaleEmployee", new[] { "Employee_UserID" });
            DropIndex("dbo.SaleEmployee", new[] { "Sale_SaleID" });
            DropColumn("dbo.Employee", "TempPoints");
            DropTable("dbo.SaleEmployee");
            CreateIndex("dbo.Sale", "ManagerID");
            CreateIndex("dbo.Sale", "CoordinatorID");
            CreateIndex("dbo.Sale", "BrokerID");
            AddForeignKey("dbo.Sale", "ManagerID", "dbo.Manager", "UserID");
            AddForeignKey("dbo.Sale", "CoordinatorID", "dbo.Coordinator", "UserID");
            AddForeignKey("dbo.Sale", "BrokerID", "dbo.Broker", "UserID");
            RenameTable(name: "dbo.CoordinatorEnterprise", newName: "EnterpriseCoordinator");
        }
    }
}
