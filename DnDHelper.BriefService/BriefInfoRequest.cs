using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnDHelper.Domain;
using ServiceStack;
using ServiceStack.ServiceHost;

namespace DnDHelper.BriefService
{
    [Route("/briefInfo", "POST")]
    public class BriefInfoRequest : IReturn<BriefInfoResponse>
    {
        public IEnumerable<ChangeInfo> PartyMembers { get; set; }

        public IEnumerable<ChangeInfo> EnemyMembers { get; set; } 
    }
}