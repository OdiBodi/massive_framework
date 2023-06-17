using System;

namespace MassiveCore.Framework.Runtime
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NumberAttribute : Attribute
    {
        public NumberAttribute(int number)
        {
            Number = number;
        }

        public int Number { get; }
    }
}
