using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.Domain
{
    public class CustomAttack
    {
        public string Name { get; set; }
        public List<Bonuses.BaseBonus> Bonuses { get; set; }

        public CustomAttack()
        {
            Bonuses = new List<BaseBonus>();
        }
    }
}
