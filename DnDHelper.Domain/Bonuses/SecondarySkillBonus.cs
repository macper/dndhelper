using System;
using System.Xml.Serialization;

namespace DnDHelper.Domain.Bonuses
{
    public class SecondarySkillBonus : BaseBonus
    {
        public string SkillName { get; set; }

        public SecondarySkillBonus(string source, int value) : base(source, value)
        {
        }

        public SecondarySkillBonus()
        {
        }

        public override void Evaluate(Character character)
        {
            character.Controller.ChangeSecondarySkill(SkillName, Value);
        }

        public override string Name
        {
            get { return "Premia do umiejêtnoœci: " + SkillName; }
        }
    }
}