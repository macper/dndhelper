using System;
using System.Linq;
using System.Xml.Serialization;

namespace DnDHelper.Domain.Bonuses
{
    public class ACBonus : BaseBonus
    {
        public string ACType { get; set; }

        public ACBonus()
        {
        }

        public ACBonus(string source, string type, int value) : base(source, value)
        {
            ACType = type;
        }

        public override void Evaluate(Character character)
        {
            character.CurrentStats.AC.Increase(ACType, Value);
        }

        public override string Name
        {
            get { return "Premia do KP"; }
        }

        public override string Description
        {
            get
            {
                return string.Format("{0}{1} ({2})", Value > 0 ? "+" : "", Value, EnumsDictionary.ACTypes.Single(s => (string)s.Value == ACType).Name);
            }
        }
    }


}