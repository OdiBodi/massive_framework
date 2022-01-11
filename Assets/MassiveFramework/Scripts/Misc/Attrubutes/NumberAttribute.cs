using System;

namespace MassiveCore.Framework
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NumberAttribute : Attribute
    {
        public int Number { get; }
        public NumberAttribute(int number)
        {
            Number = number;
        }
    }
}
