using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DnDHelper
{
    [Serializable]
    public class Item
    {
        public string Name { get; set; }
        public string Specials { get; set; }
        public int Cost { get; set; }
        public string Damage { get; set; }
        public string Range { get; set; }
        public BaseTypes BaseType { get; set; }
        public int AC { get; set; }
        public int MaxDexterityBonus { get; set; }
        public int Panalty { get; set; }

        public int Charges { get; set; }
        
        [XmlIgnore]
        public string Type
        {
            get { return Damage != null ? "Broń" : "Zbroja"; }
        }
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
            if (Type == "Broń")
            {
                return string.Format("{0} - {1} {2}", Name, Damage, Specials);
            }
            else
            {
                switch (BaseType)
                {
                    case BaseTypes.HeavyArmor:
                    case BaseTypes.MediumArmor:
                    case BaseTypes.LightArmor:
                    case BaseTypes.Shield:
                        return string.Format("{0} - +{1}KP, Max. ZR {2}, Kary -{3} {4}", Name, AC.ToString(), MaxDexterityBonus.ToString(), Panalty.ToString(), Specials);

                    default:
                        return string.Format("{0} {1}", Name, Specials);
                }
                
            }
        }
    }



    public enum BaseTypes { LightBlade, HeavyBlade, Axe, Bow, Crossbow, Spear, Blunt, LightArmor, MediumArmor, HeavyArmor, Shield, Ring, Necklease, Boots, Cloak, Other, Arrow, Belt, Wand, Gloves, Potion, Helmet, Scroll }

   
}
