using System;

namespace DnDHelper.Domain
{
    [Serializable]
    public class Throw
    {
        public int WillThrow { get; set; }
        public int ReflexThrow { get; set; }
        public int EnduranceThrow { get; set; }

        public Throw()
        {
        }

        public Throw(int willThrow, int reflexThrow, int enduranceThrow)
        {
            WillThrow = willThrow;
            ReflexThrow = reflexThrow;
            EnduranceThrow = enduranceThrow;
        }

        public void Add(Throw @throw)
        {
            WillThrow += @throw.WillThrow;
            ReflexThrow += @throw.ReflexThrow;
            EnduranceThrow += @throw.EnduranceThrow;
        }

        public void Change(int value, ThrowType type)
        {
            switch (type)
            {
                case ThrowType.Will:
                    WillThrow = value;
                    break;

                case ThrowType.Reflex:
                    ReflexThrow = value;
                    break;
                    
                case ThrowType.Endurance:
                    EnduranceThrow = value;
                    break;
            }
        }
    }

    public enum ThrowType { Will, Reflex, Endurance }
}
