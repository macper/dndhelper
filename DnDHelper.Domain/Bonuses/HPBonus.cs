using System;

namespace DnDHelper.Domain.Bonuses
{
    public class HPBonus : BaseBonus
    {
        public HPBonus(string source, int value) : base(source, value)
        {
        }

        public HPBonus()
        {
        }

        public override void Evaluate(Character character)
        {
            character.CurrentStats.HP += Value;
        }

        public override string Name
        {
            get { return "Premia do ¿ycia"; }
        }
    }
}