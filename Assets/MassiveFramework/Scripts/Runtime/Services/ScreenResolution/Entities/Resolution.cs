using System;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [Serializable]
    public struct Resolution : IEquatable<Resolution>
    {
        public int width;
        public int height;

        public Resolution(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public Vector2 Vector2()
        {
            var vector = new Vector2(width, height);
            return vector;
        }

        public bool Equals(Resolution other)
        {
            return width == other.width && height == other.height;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(width, height);
        }
        
        public static bool operator == (Resolution left, Resolution right)
        {
            return left.Equals(right);
        }

        public static bool operator != (Resolution left, Resolution right)
        {
            return !(left == right);
        }
    }
}
