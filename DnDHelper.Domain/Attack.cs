using System;

namespace DnDHelper.Domain
{
    [Serializable]
    public class Attack
    {
        public string Name { get; set; }
        public int ToHit { get; set; }
        public Damage Damage { get; set; }
        public int Range { get; set; }
        public int NumberOfAttacks { get; set; }
        public bool Custom { get; set; }
        public string MissileName { get; set; }

        public override string ToString()
        {
            return string.Format("{2}: {4} at. +{0} {1} {3}", ToHit, Damage, Name, Range > 0 ? "Zas. " + Range.ToString() : "", NumberOfAttacks);
        }
    }
}