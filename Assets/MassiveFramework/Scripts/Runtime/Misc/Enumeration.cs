using System;

namespace MassiveCore.Framework
{
    public class Enumeration : IEquatable<Enumeration>
    {
        public virtual bool Equals(Enumeration other)
        {
            if (other is null)
            {
                return false;
            }
            return GetType() == other.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }

        public static bool operator == (Enumeration left, Enumeration right)
        {
            return left.Equals(right);
        }

        public static bool operator != (Enumeration left, Enumeration right)
        {
            return !(left == right);
        }
    }
}
