using System;
using System.Xml.Serialization;

namespace DnDHelper.Domain
{
    [Serializable]
    [XmlInclude(typeof(BaseEntity))]
    public class Skill : BaseEntityItem<SkillDefinition>
    {
        public int Value { get; set; }
    }

    [XmlInclude(typeof(BaseEntity))]
    public class SkillDefinition : BaseEntityDefinition<Skill>
    {
        public BaseAttribute BonusProperty { get; set; }
        public int PanaltyModifier { get; set; }
        public string Description { get; set; }
    }
}
