using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace DnDHelper.Domain
{
    public static class Rules
    {
        public static readonly int TurnLengthInSeconds = 6;

        public static int GetStandardBonus(int value)
        {
            return (int)Math.Floor(((double)value - 10) / 2);
        }

        public static int GetBaseAttack(SkillRate skillRate, int level)
        {
            switch (skillRate)
            {
                case SkillRate.High:
                    return level;

                case SkillRate.Medium:
                    return (int)(level*0.75);

                case SkillRate.Low:
                    return (int)(level * 0.5);

                default:throw new NotImplementedException();
            }
        }
       
        #region Rzuty

        public static Dictionary<int, Throw> GetThrowTable(bool highEndurance, bool highReflex, bool highWill)
        {
            Dictionary<int, Throw> dict = new Dictionary<int, Throw>();
            Throw currentThrow = new Throw();
            if (highEndurance) currentThrow.EnduranceThrow = 2;
            if (highReflex) currentThrow.ReflexThrow = 2;
            if (highWill) currentThrow.WillThrow = 2;
            int level = 1;
            while (level <= 20)
            {
                if (level % 2 == 0)
                {
                    if (highEndurance) currentThrow.EnduranceThrow++;
                    if (highReflex) currentThrow.ReflexThrow++;
                    if (highWill) currentThrow.WillThrow++;
                }
                if (level % 3 == 0)
                {
                    if (!highEndurance) currentThrow.EnduranceThrow++;
                    if (!highReflex) currentThrow.ReflexThrow++;
                    if (!highWill) currentThrow.WillThrow++;
                }
                dict[level] = new Throw() { EnduranceThrow = currentThrow.EnduranceThrow, ReflexThrow = currentThrow.ReflexThrow, WillThrow = currentThrow.WillThrow };
                level++;
            }
            return dict;
        }

        #endregion
    
        public static bool IsContainerItem(ItemPosition itemPosition)
        {
            return new[] { ItemPosition.Staff, ItemPosition.Arrow, ItemPosition.Potion, ItemPosition.Backpack, ItemPosition.Finger }.Contains(itemPosition);
        }

        public static bool IsBonusPosition(ItemPosition itemPosition)
        {
            return !new[] { ItemPosition.SecondWeapon, ItemPosition.Backpack, ItemPosition.Arrow, ItemPosition.Potion, ItemPosition.Staff, ItemPosition.None }.Contains(itemPosition);
        }

        public static BaseTypes GetDefaultType(ItemPosition position)
        {
            switch (position)
            {
                case ItemPosition.Arrow:
                    return BaseTypes.Arrow;

                case ItemPosition.Belt:
                    return BaseTypes.Belt;

                case ItemPosition.Boots:
                    return BaseTypes.Boots;

                case ItemPosition.Cloak:
                    return BaseTypes.Cloak;

                case ItemPosition.Finger:
                    return BaseTypes.Ring;

                case ItemPosition.Gloves:
                    return BaseTypes.Gloves;

                case ItemPosition.Head:
                    return BaseTypes.Helmet;

                case ItemPosition.LeftHand:
                    return BaseTypes.Shield;

                case ItemPosition.Neck:
                    return BaseTypes.Necklease;

                case ItemPosition.Potion:
                    return BaseTypes.Potion;

                case ItemPosition.RightHand:
                    return BaseTypes.OneHandWeapon;

                case ItemPosition.Staff:
                    return BaseTypes.Wand;

                case ItemPosition.Torso:
                    return BaseTypes.Armor;

                default:
                    return BaseTypes.None;
            }
        }
    }



    public enum BaseAttribute { Strength, Dexterity, Constitution, Wisdom, Inteligence, Charisma };

    public enum SkillRate { Low, Medium, High };
   

    
        
}
