using System;

namespace DnDHelper.Domain.Bonuses
{
    public class InitiativeBonus : BaseBonus
    {
        public InitiativeBonus()
        {
        }

        public InitiativeBonus(string source, int value) : base(source, value)
        {
        }

        public override void Evaluate(Character character)
        {
            character.CurrentStats.Initiative += Value;
        }

        public override string Name
        {
            get { return "Premia do inicjatywy"; }
        }
    }
}