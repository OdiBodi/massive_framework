using System.Collections.Generic;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class Vector3EqualityComparer : IEqualityComparer<Vector3>
    {
        private readonly float error;

        public static readonly Vector3EqualityComparer Default = new Vector3EqualityComparer(0.01f);

        public Vector3EqualityComparer(float error)
        {
            this.error = error;
        }

        public bool Equals(Vector3 a, Vector3 b)
        {
            return a.x.EqualsTo(b.x, error) && a.y.EqualsTo(b.y, error) && a.z.EqualsTo(b.z, error);
        }

        public int GetHashCode(Vector3 vector)
        {
            return 0;
        }
    }
}
