using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper.Domain.Bonuses
{
    public class WeaponBonus : BaseBonus
    {
        public Item Item { get; set; }
        public BaseBonus Bonus { get; set; }

        public override string Description
        {
            get { return string.Format("{0} : {1} ({2})", Item.Name, Bonus, Source); }
        }

        public override string Name
        {
            get
            {
                return "Bonus dla broni";
            }
        }

        public WeaponBonus()
        {
        }

        public WeaponBonus(Item item, string source)
        {
            Item = item;
            Source = source;
        }

        public override void Evaluate(Character character)
        {
        }
    }
}
