using System.Collections.Generic;

namespace DnDHelper.Domain
{
    public class BriefUpdate
    {
        public IEnumerable<CharacterBrief> Characters { get; set; }
        public IEnumerable<EnemyBrief> EnemyCharacters { get; set; } 
    }
}