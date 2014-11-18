using System;

namespace DnDHelper.Domain
{
    [Serializable]
    public class KilledCreature
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public int Level { get; set; }
        public DateTime Date { get; set; }
    }
}