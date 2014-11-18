using System;

namespace DnDHelper
{
    [Serializable]
    public class Attack
    {
        public string Name { get; set; }
        public int ToHit { get; set; }
        public string Damage { get; set; }

        public override string ToString()
        {
            return string.Format("({2}) +{0} {1}", ToHit.ToString(), Damage.ToString(), Name);
        }
    }
}