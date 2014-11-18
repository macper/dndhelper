using System.Linq;

namespace DnDHelper.Domain.Bonuses
{
    public class PanaltyBonus : BaseBonus
    {
        public PanaltyBonus()
        {
        }

        public PanaltyBonus(string source, int value) : base(source, value)
        {
        }

        public override void Evaluate(Character character)
        {
            character.CurrentStats.Panalty += Value;
            character.CurrentStats.Initiative += Value;
        }

        public override string Name
        {
            get { return "Kara do testów"; }
        }
    }
}