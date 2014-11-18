using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DnDHelper
{
    [Serializable]
    public class Skill
    {
        public string Name { get; set; }
        public int BaseValue { get; set; }
        public int BonusValue { get; set; }

        // TODO: wywalić
        [XmlIgnore]
        public int Value { get { return BaseValue + BonusValue; } }
        [XmlIgnore]
        public BaseAttribute BonusProperty { get; set; }
        [XmlIgnore]
        public string Description { get; set; }
    }
}
