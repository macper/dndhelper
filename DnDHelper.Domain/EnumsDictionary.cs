using System;
using System.Collections.Generic;
using DnDHelper.Domain.Bonuses;
using IronPython.Runtime.Exceptions;

namespace DnDHelper.Domain
{
    public static class EnumsDictionary
    {
        public static List<EnumDictionaryEntry> ItemTypes { get; set; }
        public static List<EnumDictionaryEntry> BonusTypes { get; set; }
        public static List<EnumDictionaryEntry> MainSkills { get; set; }
        public static List<EnumDictionaryEntry> ACTypes { get; set; }
        public static List<EnumDictionaryEntry> BonesTypes { get; set; }
        public static List<EnumDictionaryEntry> DamageTypes { get; set; }
        public static List<EnumDictionaryEntry<ItemPosition>> ItemPositions { get; set; }
        public static List<EnumDictionaryEntry<SpellType>> SpellTypes { get; set; }
        public static List<EnumDictionaryEntry<SpellShool>> SpellShools { get; set; }
        public static List<EnumDictionaryEntry<SpellRange>> SpellRanges { get; set; }
        public static List<EnumDictionaryEntry<SkillRate>> SkillRates { get; set; }

        static EnumsDictionary()
        {
            ItemTypes = new List<EnumDictionaryEntry>();
            ItemTypes.AddRange(new [] { 
                                          new EnumDictionaryEntry("(brak)", BaseTypes.None), 
                                          new EnumDictionaryEntry("Broń jednoręczna", BaseTypes.OneHandWeapon),
                                          new EnumDictionaryEntry("Broń dwuręczna", BaseTypes.TwoHandedWeapon),
                                          new EnumDictionaryEntry("Zbroja", BaseTypes.Armor), 
                                          new EnumDictionaryEntry("Strzały", BaseTypes.Arrow),
                                          new EnumDictionaryEntry("Pas", BaseTypes.Belt),
                                          new EnumDictionaryEntry("Buty", BaseTypes.Boots),
                                          new EnumDictionaryEntry("Płaszcz", BaseTypes.Cloak),
                                          new EnumDictionaryEntry("Rękawice", BaseTypes.Gloves),
                                          new EnumDictionaryEntry("Hełm", BaseTypes.Helmet),
                                          new EnumDictionaryEntry("Amulet", BaseTypes.Necklease),
                                          new EnumDictionaryEntry("Inne", BaseTypes.Other),
                                          new EnumDictionaryEntry("Mikstura", BaseTypes.Potion),
                                          new EnumDictionaryEntry("Pierścień", BaseTypes.Ring),
                                          new EnumDictionaryEntry("Zwój", BaseTypes.Scroll),
                                          new EnumDictionaryEntry("Tarcza", BaseTypes.Shield),
                                          new EnumDictionaryEntry("Różdżka", BaseTypes.Wand) 
                                      });

            BonusTypes = new List<EnumDictionaryEntry>();
            foreach (var type in Bonuses.BonusFactory.BonusList)
            {
                var instance = Activator.CreateInstance(type) as BaseBonus;
                if (instance == null)
                    throw new TypeErrorException("Niespodziewany typ - powinien być BaseBonus");

                BonusTypes.Add(new EnumDictionaryEntry(instance.Name, type.Name));
            }

            MainSkills = new List<EnumDictionaryEntry>();
            MainSkills.AddRange(new []
                                    {
                                        new EnumDictionaryEntry("Siła", BaseAttribute.Strength),
                                        new EnumDictionaryEntry("Zręczność", BaseAttribute.Dexterity),
                                        new EnumDictionaryEntry("Kondycja", BaseAttribute.Constitution),
                                        new EnumDictionaryEntry("Inteligencja", BaseAttribute.Inteligence),
                                        new EnumDictionaryEntry("Mądrość", BaseAttribute.Wisdom),
                                        new EnumDictionaryEntry("Charyzma", BaseAttribute.Charisma) 
                                    });

            ACTypes = new List<EnumDictionaryEntry>();
            ACTypes.AddRange(new []
                                 {
                                     new EnumDictionaryEntry("Zbroja", ACBonusTypes.Armor),
                                     new EnumDictionaryEntry("Zręczność", ACBonusTypes.Dexterity),
                                     new EnumDictionaryEntry("Odbicie", ACBonusTypes.MagicShield),
                                     new EnumDictionaryEntry("Naturalny pancerz", ACBonusTypes.NaturalArmor),
                                     new EnumDictionaryEntry("Inne", ACBonusTypes.Other),
                                 });

            BonesTypes = new List<EnumDictionaryEntry>();
            BonesTypes.AddRange(new []
                                    {
                                        new EnumDictionaryEntry("K20", 20),
                                        new EnumDictionaryEntry("K12", 12),
                                        new EnumDictionaryEntry("K10", 10),
                                        new EnumDictionaryEntry("K8",8),
                                        new EnumDictionaryEntry("K6",6),
                                        new EnumDictionaryEntry("K4",4),
                                        new EnumDictionaryEntry("K1",1) 
                                    });

            DamageTypes = new List<EnumDictionaryEntry>();
            DamageTypes.AddRange(new []
                                     {
                                         new EnumDictionaryEntry("Fizyczne", Domain.DamageTypes.Physical),
                                         new EnumDictionaryEntry("Kwas", Domain.DamageTypes.Acid),
                                         new EnumDictionaryEntry("Boska energia", Domain.DamageTypes.DivineEnergy),
                                         new EnumDictionaryEntry("Ogień", Domain.DamageTypes.Fire),
                                         new EnumDictionaryEntry("Lód", Domain.DamageTypes.Ice),
                                         new EnumDictionaryEntry("Energia negatywna", Domain.DamageTypes.NegativeEnergy),
                                     });

            ItemPositions = new List<EnumDictionaryEntry<ItemPosition>>();
            ItemPositions.AddRange(new []
                                       {
                                           new EnumDictionaryEntry<ItemPosition>("Prawa ręka", ItemPosition.RightHand),
                                           new EnumDictionaryEntry<ItemPosition>("Lewa ręka", ItemPosition.LeftHand),
                                           new EnumDictionaryEntry<ItemPosition>("Pociski", ItemPosition.Arrow),
                                           new EnumDictionaryEntry<ItemPosition>("Plecak", ItemPosition.Backpack),
                                           new EnumDictionaryEntry<ItemPosition>("Pas", ItemPosition.Belt),
                                           new EnumDictionaryEntry<ItemPosition>("Buty", ItemPosition.Boots),
                                           new EnumDictionaryEntry<ItemPosition>("Płaszcz", ItemPosition.Cloak),
                                           new EnumDictionaryEntry<ItemPosition>("Pierścienie", ItemPosition.Finger),
                                           new EnumDictionaryEntry<ItemPosition>("Rękawice", ItemPosition.Gloves),
                                           new EnumDictionaryEntry<ItemPosition>("Głowa", ItemPosition.Head),
                                           new EnumDictionaryEntry<ItemPosition>("Amulet", ItemPosition.Neck),
                                           new EnumDictionaryEntry<ItemPosition>("Mikstury", ItemPosition.Potion),
                                           new EnumDictionaryEntry<ItemPosition>("Ródżki", ItemPosition.Staff),
                                           new EnumDictionaryEntry<ItemPosition>("Zbroja", ItemPosition.Torso)
                                       });

            SpellTypes = new List<EnumDictionaryEntry<SpellType>>();
            SpellTypes.AddRange(new []
                                    {
                                        new EnumDictionaryEntry<SpellType>("(wszystkie)", SpellType.All),
                                        new EnumDictionaryEntry<SpellType>("Bard", SpellType.Bard),
                                        new EnumDictionaryEntry<SpellType>("Kapłan", SpellType.Cleric),
                                        new EnumDictionaryEntry<SpellType>("Druid", SpellType.Druid),
                                        new EnumDictionaryEntry<SpellType>("Mag", SpellType.Mage),
                                        new EnumDictionaryEntry<SpellType>("Paladin", SpellType.Paladin),
                                        new EnumDictionaryEntry<SpellType>("Tropiciel", SpellType.Ranger),
                                        new EnumDictionaryEntry<SpellType>("Inny", SpellType.Other) 
                                    });

            SpellShools = new List<EnumDictionaryEntry<SpellShool>>();
            SpellShools.AddRange(new []
                                     {
                                         new EnumDictionaryEntry<SpellShool>(SpellDefinition.ConvertToString(SpellShool.Abjuration),SpellShool.Abjuration),
                                         new EnumDictionaryEntry<SpellShool>(SpellDefinition.ConvertToString(SpellShool.Conjuration),SpellShool.Conjuration),
                                         new EnumDictionaryEntry<SpellShool>(SpellDefinition.ConvertToString(SpellShool.Divination),SpellShool.Divination),
                                         new EnumDictionaryEntry<SpellShool>(SpellDefinition.ConvertToString(SpellShool.Enchantment),SpellShool.Enchantment),
                                         new EnumDictionaryEntry<SpellShool>(SpellDefinition.ConvertToString(SpellShool.Illusion),SpellShool.Illusion),
                                         new EnumDictionaryEntry<SpellShool>(SpellDefinition.ConvertToString(SpellShool.Necromancy),SpellShool.Necromancy),
                                         new EnumDictionaryEntry<SpellShool>(SpellDefinition.ConvertToString(SpellShool.Summoning),SpellShool.Summoning),
                                         new EnumDictionaryEntry<SpellShool>(SpellDefinition.ConvertToString(SpellShool.Transmutation),SpellShool.Transmutation),
                                         new EnumDictionaryEntry<SpellShool>(SpellDefinition.ConvertToString(SpellShool.Other), SpellShool.Other)
                                     });

            SpellRanges = new List<EnumDictionaryEntry<SpellRange>>();
            SpellRanges.AddRange(new []
                                     {
                                         new EnumDictionaryEntry<SpellRange>(SpellDefinition.ConvertToString(SpellRange.Infinite), SpellRange.Infinite),
                                         new EnumDictionaryEntry<SpellRange>(SpellDefinition.ConvertToString(SpellRange.Close), SpellRange.Close),
                                         new EnumDictionaryEntry<SpellRange>(SpellDefinition.ConvertToString(SpellRange.Long), SpellRange.Long),
                                         new EnumDictionaryEntry<SpellRange>(SpellDefinition.ConvertToString(SpellRange.Medium), SpellRange.Medium),
                                         new EnumDictionaryEntry<SpellRange>(SpellDefinition.ConvertToString(SpellRange.OnlyCaster), SpellRange.OnlyCaster),
                                         new EnumDictionaryEntry<SpellRange>(SpellDefinition.ConvertToString(SpellRange.Special), SpellRange.Special),
                                         new EnumDictionaryEntry<SpellRange>(SpellDefinition.ConvertToString(SpellRange.Touch), SpellRange.Touch),
                                     });

            SkillRates = new List<EnumDictionaryEntry<SkillRate>>();
            SkillRates.AddRange(new [] 
                                    {
                                        new EnumDictionaryEntry<SkillRate>("Wysoki", SkillRate.High),
                                        new EnumDictionaryEntry<SkillRate>("Średni", SkillRate.Medium),
                                        new EnumDictionaryEntry<SkillRate>("Niski", SkillRate.Low),
                                    });
            
        }


    }
}