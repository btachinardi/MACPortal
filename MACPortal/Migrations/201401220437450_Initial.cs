namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RewardClaim",
                c => new
                    {
                        RewardClaimID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        RewardID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RewardClaimID)
                .ForeignKey("dbo.Employee", t => t.RewardClaimID)
                .ForeignKey("dbo.Reward", t => t.RewardID, cascadeDelete: true)
                .Index(t => t.RewardClaimID)
                .Index(t => t.RewardID);
            
            CreateTable(
                "dbo.Reward",
                c => new
                    {
                        RewardID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RewardTierID = c.Int(),
                    })
                .PrimaryKey(t => t.RewardID)
                .ForeignKey("dbo.RewardTier", t => t.RewardTierID)
                .Index(t => t.RewardTierID);
            
            CreateTable(
                "dbo.RewardTier",
                c => new
                    {
                        RewardTierID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Cost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RewardTierID);
            
            CreateTable(
                "dbo.RewardCompany",
                c => new
                    {
                        RewardCompanyID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ImageUrl = c.String(),
                        ExternalUrl = c.String(),
                    })
                .PrimaryKey(t => t.RewardCompanyID);
            
            CreateTable(
                "dbo.RewardProduct",
                c => new
                    {
                        RewardProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ImageUrl = c.String(),
                        ExternalUrl = c.String(),
                    })
                .PrimaryKey(t => t.RewardProductID);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        CPF = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        CurrentAcceptedAgreement = c.String(),
                        Birthday = c.DateTime(),
                        Gender = c.Int(),
                        ComercialName = c.String(),
                        AvatarHair = c.Int(),
                        AvatarHairColor = c.Int(),
                        AvatarSkinColor = c.Int(),
                        AvatarFace = c.Int(),
                        AvatarCloth = c.Int(),
                        Accessory = c.Int(),
                        HomePhonePrefix = c.String(),
                        HomePhone = c.String(),
                        CellPhonePrefix = c.String(),
                        CellPhone = c.String(),
                        CEP = c.String(),
                        Street = c.String(),
                        Number = c.String(),
                        Complement = c.String(),
                        Neighborhood = c.String(),
                        City = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.UserProfile", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Sale",
                c => new
                    {
                        SaleID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Value = c.Decimal(nullable: false, storeType: "money"),
                        Modifier = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EnterpriseID = c.Int(nullable: false),
                        BrokerID = c.Int(nullable: false),
                        ManagerID = c.Int(nullable: false),
                        CoordinatorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleID)
                .ForeignKey("dbo.Broker", t => t.BrokerID)
                .ForeignKey("dbo.Coordinator", t => t.CoordinatorID)
                .ForeignKey("dbo.Enterprise", t => t.EnterpriseID, cascadeDelete: true)
                .ForeignKey("dbo.Manager", t => t.ManagerID)
                .Index(t => t.BrokerID)
                .Index(t => t.CoordinatorID)
                .Index(t => t.EnterpriseID)
                .Index(t => t.ManagerID);
            
            CreateTable(
                "dbo.Enterprise",
                c => new
                    {
                        EnterpriseID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Modifier = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CoordinatorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnterpriseID)
                .ForeignKey("dbo.Coordinator", t => t.CoordinatorID)
                .Index(t => t.CoordinatorID);
            
            CreateTable(
                "dbo.RewardsToCombosMapping",
                c => new
                    {
                        ComboRewardID = c.Int(nullable: false),
                        RewardID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ComboRewardID, t.RewardID })
                .ForeignKey("dbo.ComboReward", t => t.ComboRewardID)
                .ForeignKey("dbo.Reward", t => t.RewardID)
                .Index(t => t.ComboRewardID)
                .Index(t => t.RewardID);
            
            CreateTable(
                "dbo.CompaniesToGiftsMapping",
                c => new
                    {
                        GiftRewardID = c.Int(nullable: false),
                        RewardCompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GiftRewardID, t.RewardCompanyID })
                .ForeignKey("dbo.GiftReward", t => t.GiftRewardID, cascadeDelete: true)
                .ForeignKey("dbo.RewardCompany", t => t.RewardCompanyID, cascadeDelete: true)
                .Index(t => t.GiftRewardID)
                .Index(t => t.RewardCompanyID);
            
            CreateTable(
                "dbo.GiftReward",
                c => new
                    {
                        RewardID = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.RewardID)
                .ForeignKey("dbo.Reward", t => t.RewardID)
                .Index(t => t.RewardID);
            
            CreateTable(
                "dbo.ComboReward",
                c => new
                    {
                        RewardID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RewardID)
                .ForeignKey("dbo.Reward", t => t.RewardID)
                .Index(t => t.RewardID);
            
            CreateTable(
                "dbo.ExperienceReward",
                c => new
                    {
                        RewardID = c.Int(nullable: false),
                        AmountOfPeople = c.Int(nullable: false),
                        RewardCompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RewardID)
                .ForeignKey("dbo.Reward", t => t.RewardID)
                .ForeignKey("dbo.RewardCompany", t => t.RewardCompanyID, cascadeDelete: true)
                .Index(t => t.RewardID)
                .Index(t => t.RewardCompanyID);
            
            CreateTable(
                "dbo.ProductReward",
                c => new
                    {
                        RewardID = c.Int(nullable: false),
                        RewardProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RewardID)
                .ForeignKey("dbo.Reward", t => t.RewardID)
                .ForeignKey("dbo.RewardProduct", t => t.RewardProductID, cascadeDelete: true)
                .Index(t => t.RewardID)
                .Index(t => t.RewardProductID);
            
            CreateTable(
                "dbo.Broker",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        ManagerID = c.Int(nullable: false),
                        TempPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Employee", t => t.UserID)
                .ForeignKey("dbo.Manager", t => t.ManagerID)
                .Index(t => t.UserID)
                .Index(t => t.ManagerID);
            
            CreateTable(
                "dbo.Manager",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        TempPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Employee", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Coordinator",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        TempPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Employee", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coordinator", "UserID", "dbo.Employee");
            DropForeignKey("dbo.Manager", "UserID", "dbo.Employee");
            DropForeignKey("dbo.Broker", "ManagerID", "dbo.Manager");
            DropForeignKey("dbo.Broker", "UserID", "dbo.Employee");
            DropForeignKey("dbo.ProductReward", "RewardProductID", "dbo.RewardProduct");
            DropForeignKey("dbo.ProductReward", "RewardID", "dbo.Reward");
            DropForeignKey("dbo.ExperienceReward", "RewardCompanyID", "dbo.RewardCompany");
            DropForeignKey("dbo.ExperienceReward", "RewardID", "dbo.Reward");
            DropForeignKey("dbo.ComboReward", "RewardID", "dbo.Reward");
            DropForeignKey("dbo.GiftReward", "RewardID", "dbo.Reward");
            DropForeignKey("dbo.Sale", "ManagerID", "dbo.Manager");
            DropForeignKey("dbo.Sale", "EnterpriseID", "dbo.Enterprise");
            DropForeignKey("dbo.Sale", "CoordinatorID", "dbo.Coordinator");
            DropForeignKey("dbo.Enterprise", "CoordinatorID", "dbo.Coordinator");
            DropForeignKey("dbo.Sale", "BrokerID", "dbo.Broker");
            DropForeignKey("dbo.Employee", "UserID", "dbo.UserProfile");
            DropForeignKey("dbo.CompaniesToGiftsMapping", "RewardCompanyID", "dbo.RewardCompany");
            DropForeignKey("dbo.CompaniesToGiftsMapping", "GiftRewardID", "dbo.GiftReward");
            DropForeignKey("dbo.RewardsToCombosMapping", "RewardID", "dbo.Reward");
            DropForeignKey("dbo.RewardsToCombosMapping", "ComboRewardID", "dbo.ComboReward");
            DropForeignKey("dbo.Reward", "RewardTierID", "dbo.RewardTier");
            DropForeignKey("dbo.RewardClaim", "RewardID", "dbo.Reward");
            DropForeignKey("dbo.RewardClaim", "RewardClaimID", "dbo.Employee");
            DropIndex("dbo.Coordinator", new[] { "UserID" });
            DropIndex("dbo.Manager", new[] { "UserID" });
            DropIndex("dbo.Broker", new[] { "ManagerID" });
            DropIndex("dbo.Broker", new[] { "UserID" });
            DropIndex("dbo.ProductReward", new[] { "RewardProductID" });
            DropIndex("dbo.ProductReward", new[] { "RewardID" });
            DropIndex("dbo.ExperienceReward", new[] { "RewardCompanyID" });
            DropIndex("dbo.ExperienceReward", new[] { "RewardID" });
            DropIndex("dbo.ComboReward", new[] { "RewardID" });
            DropIndex("dbo.GiftReward", new[] { "RewardID" });
            DropIndex("dbo.Sale", new[] { "ManagerID" });
            DropIndex("dbo.Sale", new[] { "EnterpriseID" });
            DropIndex("dbo.Sale", new[] { "CoordinatorID" });
            DropIndex("dbo.Enterprise", new[] { "CoordinatorID" });
            DropIndex("dbo.Sale", new[] { "BrokerID" });
            DropIndex("dbo.Employee", new[] { "UserID" });
            DropIndex("dbo.CompaniesToGiftsMapping", new[] { "RewardCompanyID" });
            DropIndex("dbo.CompaniesToGiftsMapping", new[] { "GiftRewardID" });
            DropIndex("dbo.RewardsToCombosMapping", new[] { "RewardID" });
            DropIndex("dbo.RewardsToCombosMapping", new[] { "ComboRewardID" });
            DropIndex("dbo.Reward", new[] { "RewardTierID" });
            DropIndex("dbo.RewardClaim", new[] { "RewardID" });
            DropIndex("dbo.RewardClaim", new[] { "RewardClaimID" });
            DropTable("dbo.Coordinator");
            DropTable("dbo.Manager");
            DropTable("dbo.Broker");
            DropTable("dbo.ProductReward");
            DropTable("dbo.ExperienceReward");
            DropTable("dbo.ComboReward");
            DropTable("dbo.GiftReward");
            DropTable("dbo.CompaniesToGiftsMapping");
            DropTable("dbo.RewardsToCombosMapping");
            DropTable("dbo.Enterprise");
            DropTable("dbo.Sale");
            DropTable("dbo.Employee");
            DropTable("dbo.UserProfile");
            DropTable("dbo.RewardProduct");
            DropTable("dbo.RewardCompany");
            DropTable("dbo.RewardTier");
            DropTable("dbo.Reward");
            DropTable("dbo.RewardClaim");
        }
    }
}
