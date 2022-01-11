using System.Collections.Generic;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ColorEqualityComparer : IEqualityComparer<Color>
    {
        private readonly float error;

        public static readonly ColorEqualityComparer Default = new ColorEqualityComparer(0.01f);

        public ColorEqualityComparer(float error)
        {
            this.error = error;
        }

        public bool Equals(Color a, Color b)
        {
            return a.r.EqualsTo(b.r, error) && a.g.EqualsTo(b.g, error) && a.b.EqualsTo(b.b, error) && a.a.EqualsTo(b.a, error);
        }

        public int GetHashCode(Color color)
        {
            return 0;
        }
    }
}
