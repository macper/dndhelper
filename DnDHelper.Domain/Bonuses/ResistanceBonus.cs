using System;

namespace DnDHelper.Domain.Bonuses
{
    public class ResistanceBonus : BaseBonus
    {
        public ResistanceBonus(string source, int value) : base(source, value)
        {
        }

        public string DamageType { get; set; }
        public int OverrideValue { get; set; }

        public ResistanceBonus()
        {
        }

        public override void Evaluate(Character character)
        {
            
        }

        public override string Name
        {
            get
            {
                return "Redukcja obra¿eñ";
            }
        }

        public override string Description
        {
            get { return string.Format("{0}{2} ({1})", Value, DamageType, OverrideValue > 0 ? "/"+OverrideValue:"");}
        }
    }
}