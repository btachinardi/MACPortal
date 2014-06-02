using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MACPortal.ViewModel
{
    public class MemberRewardsVM : BaseMemberVM
    {
        public List<int> TiersCost { get; set; }
        public List<List<RewardItemVM>> TiersRewards { get; set; }

        public string GoToTier { get; set; }
        public string GoToId { get; set; }

        public MemberRewardsVM(RewardVM reward)
        {
            TiersCost = new List<int>{1000, 2000, 4000, 6000, 10000};
            TiersRewards = new List<List<RewardItemVM>>();
            for (var i = 0; i < TiersCost.Count; i++)
            {
                TiersRewards.Add(new List<RewardItemVM>());
            }

            foreach (var item in reward.Items)
            {
                TiersRewards[item.Tier - 1].Add(item);
            }
        }
    }
}