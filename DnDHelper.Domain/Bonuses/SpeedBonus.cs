using System;

namespace DnDHelper.Domain.Bonuses
{
    public class SpeedBonus : BaseBonus
    {
        public SpeedBonus()
        {
        }

        public bool MultiplyMode { get; set; }

        public SpeedBonus(string source, int value, bool multiplyMode) : base(source, value)
        {
            MultiplyMode = multiplyMode;
        }

        public override void Evaluate(Character character)
        {
            if (MultiplyMode)
            {
                if (Value > 0)
                {
                    character.CurrentStats.Speed *= Value;
                }
                else
                {
                    character.CurrentStats.Speed = character.CurrentStats.Speed/(-1*Value);
                }
            }
            else
            {
                character.CurrentStats.Speed += Value;
            }
        }

        public override string Name
        {
            get
            {
                return "Bonus do szybkoœci";
            }
        }
    }

    
}