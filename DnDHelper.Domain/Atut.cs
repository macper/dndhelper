using System;
using System.Xml.Serialization;

namespace DnDHelper.Domain
{
    [XmlInclude(typeof(BaseEntity))]
    [Serializable]
    public class Atut : BaseEntityItem<AtutDefinition>
    {
        public string AdditionalInfo { get; set; }
    }

    [XmlInclude(typeof(BaseEntity))]
    public class AtutDefinition : BaseEntityDefinition<Atut>
    {
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Script { get; set; }

        public static class CommonAtutes
        {
            public static readonly string TwoWeaponsCombat = "Walka dwoma orężami";
            public static readonly string AdvancedTwoWeaponsCombat = "Doskonalsza walka dwoma orężami";
            public static readonly string MasterTwoWeaponsCombat = "Potężniejsza walkda dwoma orężami";
            public static readonly string FineCombat = "Finezja w broni";
        }
    }
}
