using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DnDHelper
{
    [Serializable]
    public class Race
    {
        public string Name { get; set; }
        public List<RaceBonus> Bonuses { get; set; }
        public string MethodToExecute { get; set; }

        public Race()
        {
            Bonuses = new List<RaceBonus>();
        }
    }
}
