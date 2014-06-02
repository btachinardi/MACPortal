using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using MACPortal.Models;
using MACPortal.Models.Rewards;
using MACPortal.Models.Users;

namespace MACPortal.DAL
{
    public class PortalContext : DbContext
    {
        
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<RewardTier> RewardTiers { get; set; }
        public DbSet<RewardCompany> RewardCompanies { get; set; }
        public DbSet<RewardProduct> RewardProducts { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Sale> Sales { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<GiftReward>()
            .HasMany(c => c.Companies)
            .WithMany()
            .Map(x =>
            {
                x.MapLeftKey("GiftRewardID");
                x.MapRightKey("RewardCompanyID");
                x.ToTable("CompaniesToGiftsMapping");
            });

            modelBuilder.Entity<ComboReward>()
            .HasMany(c => c.Rewards)
            .WithMany()
            .Map(x =>
            {
                x.MapLeftKey("ComboRewardID");
                x.MapRightKey("RewardID");
                x.ToTable("RewardsToCombosMapping");
            });

            /**
            modelBuilder.Entity<Sale>()
            .HasMany(s => s.Coordinators)
            .WithMany()
            .Map(x =>
            {
                x.MapLeftKey("SaleID");
                x.MapRightKey("CoordinatorID");
                x.ToTable("SalesToCoordinatorsMapping");
            });
             */

            /**
            modelBuilder.Entity<Coordinator>()
            .HasMany(c => c.Enterprises)
            .WithMany(e => e.Coordinators)
            .Map(x =>
            {
                x.MapLeftKey("UserID");
                x.MapRightKey("EnterpriseID");
                x.ToTable("CoordinatorsEnterprisesMapping");
            });
             * */
        }

        public override int SaveChanges()
        {
            var changeSet = ChangeTracker.Entries<Employee>();

            if (changeSet != null)
            {
                foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged))
                {
                    entry.Entity.CPF = entry.Entity.CPF.Replace(".", String.Empty).Replace("-", String.Empty).Trim();
                }
            }
            return base.SaveChanges();
        }
    }
}