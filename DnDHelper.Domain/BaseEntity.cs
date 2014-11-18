using System;
using System.Xml.Serialization;

namespace DnDHelper.Domain
{
    [Serializable]
    [XmlInclude( typeof( AppSetting ) )]
    [XmlInclude( typeof( AtutDefinition ) )]
    [XmlInclude( typeof( Character ) )]
    [XmlInclude( typeof( CharacterGroup ) )]
    [XmlInclude( typeof( ClassDefinition ) )]
    [XmlInclude( typeof( EffectDefinition ) )]
    [XmlInclude( typeof( ItemDefinition ) )]
    [XmlInclude( typeof( RaceDefinition ) )]
    [XmlInclude( typeof( SkillDefinition ) )]
    [XmlInclude( typeof( SpellDefinition))]
    [XmlInclude( typeof( Experience ) )]
    [XmlInclude( typeof( Script ) )]
    public abstract class BaseEntity : ICustomSerializable
    {
        [XmlAttribute]
        public string Name { get; set; }
        public Guid? Id { get; set; }

        public virtual void Serialize()
        {
            if (Id == null)
                Id = Guid.NewGuid();
        }

        public virtual void Deserialize()
        {
        }
    }
}