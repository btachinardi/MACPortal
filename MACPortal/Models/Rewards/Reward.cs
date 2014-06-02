using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MACPortal.Models.Users;

namespace MACPortal.Models.Rewards
{
    [Table("Reward")]
    public class Reward
    {
        public int RewardID { get; set; }
        [Required]
        public string Name { get; set; }
        public int? RewardTierID { get; set; }

        [ForeignKey("RewardTierID")]
        public virtual RewardTier Tier { get; set; }
        public virtual ICollection<RewardClaim> Claims { get; set; }
    }

    [Table("ProductReward")]
    public class ProductReward : Reward
    {
        public int RewardProductID { get; set; }

        [ForeignKey("RewardProductID")]
        public virtual RewardProduct Product { get; set; }
    }

    [Table("GiftReward")]
    public class GiftReward : Reward
    {
        [Column(TypeName = "Money")]
        public decimal Value { get; set; }

        public virtual ICollection<RewardCompany> Companies { get; set; }
    }

    [Table("ExperienceReward")]
    public class ExperienceReward : Reward
    {
        public int AmountOfPeople { get; set; }
        public int RewardCompanyID { get; set; }

        [ForeignKey("RewardCompanyID")]
        public virtual RewardCompany Location { get; set; }
    }

    [Table("ComboReward")]
    public class ComboReward : Reward
    {
        public virtual ICollection<Reward> Rewards { get; set; }
    }

    public class RewardTier
    {
        public int RewardTierID { get; set; }
        [Required]
        public string Name { get; set; }
        public int Cost { get; set; }
    }

    public class RewardProduct
    {
        public int RewardProductID { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ExternalUrl { get; set; }
    }

    public class RewardCompany
    {
        public int RewardCompanyID { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ExternalUrl { get; set; }
    }

    public class RewardClaim
    {
        public int RewardClaimID { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeID { get; set; }
        public int RewardID { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("RewardID")]
        public virtual Reward Reward { get; set; }
    }
}