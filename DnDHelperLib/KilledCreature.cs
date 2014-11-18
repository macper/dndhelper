using System;

namespace DnDHelper
{
    [Serializable]
    public class KilledCreature
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public DateTime Date { get; set; }
    }
}