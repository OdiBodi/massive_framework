using System.Collections.Generic;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class Vector3EqualityComparer : IEqualityComparer<Vector3>
    {
        public static readonly Vector3EqualityComparer Default = new(0.01f);

        private readonly float _error;

        public Vector3EqualityComparer(float error)
        {
            _error = error;
        }

        public bool Equals(Vector3 a, Vector3 b)
        {
            return a.x.EqualsTo(b.x, _error) && a.y.EqualsTo(b.y, _error) && a.z.EqualsTo(b.z, _error);
        }

        public int GetHashCode(Vector3 vector)
        {
            return 0;
        }
    }
}
