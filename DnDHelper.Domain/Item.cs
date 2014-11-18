using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.Domain
{
    [Serializable]
    [XmlInclude(typeof(BaseEntity))]
    public class Item : BaseEntityItem<ItemDefinition>
    {
        public int Charges { get; set; }
        
        [XmlIgnore]
        public string Details
        {
            get
            {
                return ToString();
            }
        }

        [XmlIgnore]
        public string ChargesDetails
        {
            get
            {
                return string.Format("{0} [{1}]", Name, Charges.ToString());
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Name, (Definition != null && Definition.Specials != null) ? "(" + Definition.Specials + ")" : string.Empty);
        }
    }

    [XmlInclude(typeof(BaseEntity))]
    public class ItemDefinition : BaseEntityDefinition<Item>
    {
        public string Specials { get; set; }
        public int Cost { get; set; }
        public BaseTypes BaseType { get; set; }
        public bool IsPrototype { get; set; }
        public string PrototypeName { get; set; }
        public int InitialCharges { get; set; }
        public List<BaseBonus> Bonuses { get; set; }
        public string Script { get; set; }

        public ItemDefinition()
        {
            Bonuses = new List<BaseBonus>();
        }

        public override Item CreateItem()
        {
            var item = base.CreateItem();
            item.Charges = InitialCharges;
            return item;
        }

        public bool IsRangedWeapon
        {
            get { return Bonuses.Any(b => b.GetType() == typeof (RangeBonus)); }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum BaseTypes { None, OneHandWeapon, TwoHandedWeapon, Armor, Shield, Ring, Necklease, Boots, Cloak, Other, Arrow, Belt, Wand, Gloves, Potion, Helmet, Scroll }

    public static class ItemPrototypes
    {
        public static readonly string TwoHandedSword = "Miecz dwuręczny";
        public static readonly string Helmet = "Hełm";
        public static readonly string StuddedLeatherArmor = "Skórzana ćwiekowana";
        public static readonly string Belt = "Pas";
        public static readonly string HeavyCrossbow = "Ciężka kusza";
        public static readonly string Bolt = "Bełt";
        public static readonly string ShortSword = "Miecz krótki";
        public static readonly string Scimitar = "Sejmitar";
        public static readonly string Rapier = "Rapier";
        public static readonly string Dagger = "Sztylet";

    }
}
