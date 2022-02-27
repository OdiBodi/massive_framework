using System;

namespace MassiveCore.Framework
{
    public class Enumeration : IEquatable<Enumeration>
    {
        public virtual bool Equals(Enumeration other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((Enumeration)obj);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }

        public static bool operator == (Enumeration left, Enumeration right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Enumeration left, Enumeration right)
        {
            return !(left == right);
        }
    }
}
