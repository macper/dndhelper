using System;

namespace DnDHelper.Domain.Bonuses
{
    public class RangeBonus : BaseBonus
    {
        public RangeBonus()
        {
        }

        public RangeBonus(string source, int value) : base(source, value)
        {
        }

        public override void Evaluate(Character character)
        {
            
        }

        public override string Name
        {
            get { return "Premia do zasiêgu"; }
        }
    }
}