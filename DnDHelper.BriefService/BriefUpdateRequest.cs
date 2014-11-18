using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnDHelper.Domain;
using ServiceStack.ServiceHost;

namespace DnDHelper.BriefService
{
    public class BriefUpdateRequest : IReturn<bool>
    {
        public IEnumerable<CharacterBrief> Characters { get; set; }

        public IEnumerable<EnemyBrief> EnemyCharacters { get; set; } 

        public DateTime Time { get; set; }

        public bool RemoveAllEnemies { get; set; }
    }
}