using System;

namespace DnDHelper.Domain.Bonuses
{
    public class NumberOfAttacksBonus : BaseBonus
    {
        public NumberOfAttacksBonus(string source, int value) : base(source, value)
        {
        }

        public override bool IsPositive
        {
            get { return true; }
        }

        public override string Name
        {
            get
            {
                return "Premia do liczby ataków";
            }
        }

        public override void Evaluate(Character character)
        {
            character.CurrentStats.Attack.NumberOfAttacks += Value;
        }

        public NumberOfAttacksBonus()
        {
        }
    }
}