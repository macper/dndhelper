using System;
using System.Xml.Serialization;

namespace DnDHelper.Domain.Bonuses
{
    public class ThrowBonus : BaseBonus
    {
        public Throw BonusType { get; set; }

        public ThrowBonus(string source, Throw bonus) : base(source, 0)
        {
            BonusType = bonus;
        }

        public ThrowBonus()
        {
            BonusType = new Throw();
        }

        public override void Evaluate(Character character)
        {
            character.CurrentStats.Throws.EnduranceThrow += BonusType.EnduranceThrow;
            character.CurrentStats.Throws.ReflexThrow += BonusType.ReflexThrow;
            character.CurrentStats.Throws.WillThrow += BonusType.WillThrow;
        }

        public bool IsThrowType(ThrowType throwType)
        {
            switch (throwType)
            {
                case ThrowType.Will:
                    return BonusType.WillThrow != 0;

                case ThrowType.Reflex:
                    return BonusType.ReflexThrow != 0;

                case ThrowType.Endurance:
                    return BonusType.EnduranceThrow != 0;
            }
            throw new NotImplementedException("Brak takiego typa");
        }

        public override string Name
        {
            get { return "Premia do rzutów obronnych"; }
        }

        public override string Description
        {
            get { return string.Format("Wytr. {0}, Ref. {1}, Wola {2}", BonusType.EnduranceThrow, BonusType.ReflexThrow, BonusType.WillThrow);}
        }

        public override bool IsPositive
        {
            get { return BonusType.EnduranceThrow + BonusType.ReflexThrow + BonusType.WillThrow > 0; }
        }
    }
}