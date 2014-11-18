using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDHelper.Domain
{
    [Serializable]
    public class AC
    {
        public List<ACForType> ACForType { get; set; }
        public int Total
        {
            get { return ACForType.Sum(a => a.Value) + 10; }
        }

        public AC()
        {
            ACForType = new List<ACForType>();
        }

        public void Increase(string type, int value)
        {
            if (type != ACBonusTypes.Armor &&
                type != ACBonusTypes.Dexterity &&
                type != ACBonusTypes.MagicShield &&
                type != ACBonusTypes.NaturalArmor &&
                type != ACBonusTypes.Other)
                throw new NotImplementedException("Nieznany typ AC: " + type);
            var el = ACForType.SingleOrDefault(a => a.ACType == type);
            if (el == null)
            {
                el = new ACForType() { ACType = type };
                ACForType.Add(el);
            }
            el.Value += value;

        }

        public int GetForType(string type)
        {
            return ACForType.Any(s => s.ACType == type) ? ACForType.SingleOrDefault(s => s.ACType == type).Value : 0;
        }
    }

    [Serializable]
    public class ACForType
    {
        public string ACType { get; set; }
        public int Value { get; set; }

        public ACForType()
        {
        }

        public ACForType(string acType, int value)
        {
            ACType = acType;
            Value = value;
        }
    }

    public static class ACBonusTypes
    {
        public static readonly string Dexterity = "Zrêcznoœæ";
        public static readonly string Armor = "Zbroja";
        public static readonly string MagicShield = "Odbicie";
        public static readonly string NaturalArmor = "Naturalny pancerz";
        public static readonly string Other = "Inne";
    }
}