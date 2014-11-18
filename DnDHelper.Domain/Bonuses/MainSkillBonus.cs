using System;
using System.Linq;
using System.Xml.Serialization;

namespace DnDHelper.Domain.Bonuses
{
    public class MainSkillBonus : BaseBonus
    {
        public BaseAttribute Attribute { get; set; }

        public MainSkillBonus(string source, int value) : base(source, value)
        {
        }

        public MainSkillBonus()
        {
        }

        public override void Evaluate(Character character)
        {
            character.Controller.IncreaseMainSkill(Attribute, Value);
        }

        public override string Name
        {
            get { return "Premia do cechy"; }
        }

        public override string Description
        {
            get { return string.Format("{0} {1}", EnumsDictionary.MainSkills.Single(s => (BaseAttribute)s.Value == Attribute).Name, Value);}
        }
    }
}