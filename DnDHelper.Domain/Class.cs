using System;
using System.Collections.Generic;

namespace DnDHelper.Domain
{
    [Serializable]
    public class ClassDefinition : BaseEntityDefinition<Class>
    {
        public int PW { get; set; }
        public SkillRate AttackSkill { get; set; }
        public bool HighReflexThrow { get; set; }
        public bool HighWillThrow { get; set; }
        public bool HighEnduranceThrow { get; set; }
        public List<List<SpellCasting>> SpellsPerLevel { get; set; }
        public int SkillsPoints { get; set; }
        public string Script { get; set; }
        public List<SkillPreference> SkillsPreference { get; set; }

        public ClassDefinition()
        {
            SpellsPerLevel = new List<List<SpellCasting>>();
            SkillsPreference = new List<SkillPreference>();
        }
    }

    public class Class : BaseEntityItem<ClassDefinition>
    {
        public int Level { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, Level);
        }
    }
}
