using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace DnDHelper.Domain.Bonuses
{
    public static class BonusFactory
    {
        public static readonly List<Type> BonusList;

        static BonusFactory()
        {
            BonusList = new List<Type>();
            var assembly = Assembly.GetExecutingAssembly();
            BonusList.AddRange(assembly.GetTypes().Where(t => t.BaseType == typeof(BaseBonus)));
        }

        public static BaseBonus GetByName(string name)
        {
            return BonusList.Single(t => t.Name == name).GetConstructor(new Type[] {}).Invoke(new object[] {}) as BaseBonus;
        }

        public static BaseBonus GetByName(string name, string source)
        {
            var bonus = GetByName(name);
            bonus.Source = source;
            return bonus;
        }
    }
}