using System;

namespace MassiveCore.Framework.Runtime
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
