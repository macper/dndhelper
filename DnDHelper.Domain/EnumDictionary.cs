using System;
using System.Collections.Generic;

namespace DnDHelper.Domain
{
    public class EnumDictionaryEntry
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public EnumDictionaryEntry(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }

    public class EnumDictionaryEntry<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }

        public EnumDictionaryEntry(string name, T value)
        {
            Name = name;
            Value = value;
        }
    }
}