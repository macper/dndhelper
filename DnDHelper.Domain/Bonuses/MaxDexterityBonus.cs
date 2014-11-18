using System;
using System.Linq;

namespace DnDHelper.Domain.Bonuses
{
    public class MaxDexterityBonus : BaseBonus
    {
        public MaxDexterityBonus()
        {
        }

        public MaxDexterityBonus(string source, int value) : base(source, value)
        {
        }

        public override void Evaluate(Character character)
        {
            var acForDext = character.CurrentStats.AC.ACForType.SingleOrDefault(s => s.ACType == ACBonusTypes.Dexterity);
            if (acForDext != null && acForDext.Value > Value)
            {
                acForDext.Value = Value;
            }
        }

        public override string Name
        {
            get { return "Ograniczenie premii za zrêcznoœæ"; }
        }

        public override bool IsPositive
        {
            get { return false; }
        }
    }
}