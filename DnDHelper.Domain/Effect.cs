using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.Domain
{
    [Serializable]
    [XmlInclude(typeof(BaseEntity))]
    public class Effect : BaseEntityItem<EffectDefinition>
    {
        public string InstanceName { get; set; }
        public string OriginalInstanceName { get; set; }
        public int? Duration { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public bool IsPermanent { get { return Duration == null; } }
        public List<BaseBonus> Bonuses { get; set; }
        public int Counter { get; set; }
        public bool Calculated { get; set; }
        public string CustomText { get; set; }
    }

    [XmlInclude(typeof(BaseEntity))]
    public class EffectDefinition : BaseEntityDefinition<Effect>
    {
        public string Description { get; set; }
        public List<BaseBonus> Bonuses { get; set; }
        public string Script { get; set; }
        public int? DefaultDuration { get; set; }
        public int DefaultCounter { get; set; }

        public bool IsPositive { get; set; }

        public bool IsBattleEffect { get; set; }

        public EffectDefinition()
        {
            Bonuses = new List<BaseBonus>();
        }

        public override Effect CreateItem()
        {
            var item = base.CreateItem();
            item.InstanceName = Name;
            item.Duration = DefaultDuration;
            item.Counter = DefaultCounter;
            item.Bonuses = new List<BaseBonus>(Bonuses);
            return item;
        }
    }
}