using System;
using System.Xml.Serialization;

namespace DnDHelper.Domain.Bonuses
{
    public class AttackBonus : BaseBonus
    {
        public bool Melee { get; set; }

        public AttackBonus()
        {
        }

        public AttackBonus(string source, int value) : base(source, value)
        {
        }

        public override void Evaluate(Character character)
        {
            if (Melee)
            {
                character.CurrentStats.Attack.Melee += Value;
            }
            else
            {
                character.CurrentStats.Attack.Range += Value;
            }
        }

        public override string Name
        {
            get { return "Premia do ataku"; }
        }
    }
}