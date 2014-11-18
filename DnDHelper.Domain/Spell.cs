using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DnDHelper.Domain
{
    [Serializable]
    [XmlInclude(typeof(BaseEntity))]
    public class SpellDefinition : BaseEntityDefinition<Spell>
    {
        public List<SpellType> SpellTypes { get; set; }
        public SpellShool SpellShool { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public SpellRange Range { get; set; }

        public SpellDefinition()
        {
            SpellTypes = new List<SpellType>();
        }

        public override string ToString()
        {
            return string.Format(@"Nazwa: {0}
Typ: {1}
Poziom: {2}
Szkoła: {3}
Zasięg: {5}
Czas trwania: {6}
Opis:
{4}", Name, ConvertTypesToString(), Level.ToString(), ConvertToString(SpellShool), Description, ConvertToString(Range), Duration);
        }

        public static string ConvertToString(SpellShool shool)
        {
            switch (shool)
            { 
                case SpellShool.Abjuration:
                    return "Odrzucanie";

                case SpellShool.Conjuration:
                    return "Wywoływanie";

                case SpellShool.Divination:
                    return "Poznanie";

                case SpellShool.Enchantment:
                    return "Uroki";

                case SpellShool.Illusion:
                    return "Iluzje";

                case SpellShool.Necromancy:
                    return "Nekromancja";

                case SpellShool.Transmutation:
                    return "Przemiany";

                case SpellShool.Summoning:
                    return "Przywoływanie";

                case SpellShool.Other:
                    return "Inna";
            }
            throw new NotImplementedException("Nieznana szkoła");
        }

        public static string ConvertToString(SpellRange range)
        {
            switch (range)
            {
                case SpellRange.Close:
                    return "Bliski";

                case SpellRange.Infinite:
                    return "Nieskończony";

                case SpellRange.Long:
                    return "Daleki";

                case SpellRange.Medium:
                    return "Średni";

                case SpellRange.Special:
                    return "Specjalny";

                case SpellRange.Touch:
                    return "Dotyk";

                case SpellRange.OnlyCaster:
                    return "Tylko na siebie";
            }
            throw new NotImplementedException("Nie znany typ zasięgu");
        }

        public string ConvertTypesToString()
        {
            StringBuilder sb = new StringBuilder();
            SpellTypes.ForEach(f => sb.AppendFormat("{0},", f));
            return sb.ToString().TrimEnd(',');
        }
    }

    [Serializable]
    [XmlInclude(typeof(BaseEntity))]
    public class Spell : BaseEntityItem<SpellDefinition>
    {
        [XmlAttribute]
        public bool IsCasted { get; set; }
    }

    [Serializable]
    public class SpellCasting
    {
        [XmlAttribute]
        public int Level { get; set; }
        [XmlAttribute]
        public int Count { get; set; }
        [XmlAttribute]
        public SpellType Type { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - poz.{1}({2})", Type, Level.ToString(), Count.ToString());
        }
    }

    public enum SpellType { All, Mage, Cleric, Druid, Ranger, Paladin, Bard, Other };
    public enum SpellShool { Conjuration, Illusion, Necromancy, Abjuration, Enchantment, Transmutation, Divination, Summoning, Other };
    public enum SpellRange { Touch, OnlyCaster, Close, Medium, Long, Infinite, Special }
    

}
