using System.Xml.Serialization;

namespace DnDHelper
{
    public class RaceBonus
    {
        [XmlAttribute]
        public BaseAttribute Attribute { get; set; }
        [XmlAttribute]
        public int Value { get; set; }

        public RaceBonus(BaseAttribute attribute, int value)
        {
            Attribute = attribute;
            Value = value;
        }

        public RaceBonus()
        {
        }
    }
}