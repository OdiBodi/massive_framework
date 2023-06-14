using System.Collections.Generic;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class Vector2EqualityComparer : IEqualityComparer<Vector2>
    {
        public static readonly Vector2EqualityComparer Default = new(0.01f);
        
        private readonly float _error;

        public Vector2EqualityComparer(float error)
        {
            _error = error;
        }

        public bool Equals(Vector2 a, Vector2 b)
        {
            return a.x.EqualsTo(b.x, _error) && a.y.EqualsTo(b.y, _error);
        }

        public int GetHashCode(Vector2 vector)
        {
            return 0;
        }
    }
}
