using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MACPortal.Models.Users;

namespace MACPortal.ViewModel
{
    public class MemberHomeVM : BaseMemberVM
    {
        public RankingVM Ranking { get; set; }
        public RewardVM Rewards { get; set; }
    }
}