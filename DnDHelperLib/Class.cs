using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DnDHelper
{
    [Serializable]
    public class Class
    {
        public string Name { get; set; }
        public int PW { get; set; }
        public SkillRate AttackSkill { get; set; }
        public bool HighReflexThrow { get; set; }
        public bool HighWillThrow { get; set; }
        public bool HighEnduranceThrow { get; set; }
        public List<List<SpellCasting>> SpellsPerLevel { get; set; }
        public int SkillsPoints { get; set; }
        public string MethodToExecute { get; set; }
    }
}
