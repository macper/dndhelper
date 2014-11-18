using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper.Domain.Bonuses
{
    public class OverallBonus : BaseBonus
    {
        public override void Evaluate(Character character)
        {
            character.CurrentStats.Attack.Melee += Value;
            character.CurrentStats.Attack.Range += Value;
            character.CurrentStats.Throws.Add(new Throw(Value, Value, Value));
            character.CurrentStats.Skills.ForEach(s => s.Value += Value);
        }

        public override string Name
        {
            get
            {
                return "Premia do rzutów";
            }
        }
    }
}
