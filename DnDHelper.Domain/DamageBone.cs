using System;

namespace DnDHelper.Domain
{
    [Serializable]
    public struct DamageBone
    {
        public int MaxValue;
        public int Count;
        public string DamageType;

        public DamageBone(int maxValue, int count)
        {
            MaxValue = maxValue;
            Count = count;
            DamageType = DamageTypes.Physical;
        }

        public DamageBone(int maxValue, int count, string damageType)
        {
            MaxValue = maxValue;
            Count = count;
            DamageType = damageType;
        }

        public DamageBone(string value)
        {
            var arrVal = value.Split('/');
            Count = int.Parse(arrVal[0]);
            MaxValue = int.Parse(arrVal[1]);
            DamageType = arrVal[2];
        }

        public bool IsEmpty
        {
            get { return MaxValue == 0 && Count == 0; }
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}/{2}", Count, MaxValue, DamageType);
        }
    }
}