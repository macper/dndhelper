using System;

namespace DnDHelper.Domain
{
    [Serializable]
    public class AttackSkill
    {
        public int Melee { get; set; }
        public int Range { get; set; }
        public int NumberOfAttacks { get; set; }

        public bool Defined
        {
            get { return Melee > 0 || Range > 0; }
        }
    }
}