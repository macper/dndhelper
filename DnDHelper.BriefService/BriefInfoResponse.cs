using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnDHelper.Domain;

namespace DnDHelper.BriefService
{
    public class BriefInfoResponse
    {
        public IEnumerable<CharacterBrief> Characters { get; set;}

        public IEnumerable<EnemyBrief> Enemies { get; set; } 
    }
}