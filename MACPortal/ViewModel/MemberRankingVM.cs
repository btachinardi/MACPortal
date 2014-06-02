using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MACPortal.Models.Users;

namespace MACPortal.ViewModel
{
    public class MemberRankingVM : BaseMemberVM
    {
        public RankingVM BrokersRanking { get; set; }
        public RankingVM ManagersRanking { get; set; }
        public RankingVM CoordinatorsRanking { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}