using System;
using System.Collections.Generic;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.Domain
{
    [Serializable]
    public class RaceDefinition : BaseEntityDefinition<Race>
    {
        public int BaseSpeed { get; set; }
        public List<BaseBonus> Bonuses { get; set; }

        public string Script { get; set; }

        public RaceDefinition()
        {
            Bonuses = new List<BaseBonus>();
        }
    }

    public class Race : BaseEntityItem<RaceDefinition>
    {
        
    }
}
