using System.Collections.Generic;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ColorEqualityComparer : IEqualityComparer<Color>
    {
        private readonly float _error;

        public static readonly ColorEqualityComparer Default = new(0.01f);

        public ColorEqualityComparer(float error)
        {
            _error = error;
        }

        public bool Equals(Color a, Color b)
        {
            return a.r.EqualsTo(b.r, _error) && a.g.EqualsTo(b.g, _error) && a.b.EqualsTo(b.b, _error) && a.a.EqualsTo(b.a, _error);
        }

        public int GetHashCode(Color color)
        {
            return 0;
        }
    }
}
