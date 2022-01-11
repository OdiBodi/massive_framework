using System.Collections.Generic;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class Vector2EqualityComparer : IEqualityComparer<Vector2>
    {
        private readonly float error;

        public static readonly Vector2EqualityComparer Default = new Vector2EqualityComparer(0.01f);

        public Vector2EqualityComparer(float error)
        {
            this.error = error;
        }

        public bool Equals(Vector2 a, Vector2 b)
        {
            return a.x.EqualsTo(b.x, error) && a.y.EqualsTo(b.y, error);
        }

        public int GetHashCode(Vector2 vector)
        {
            return 0;
        }
    }
}
