using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public static class Vector3Extensions
    {
        public static bool EqualsTo(this Vector3 a, Vector3 b, float error = 0.01f)
        {
            return new Vector3EqualityComparer(error).Equals(a, b);
        }

        public static float SqrDistance(this Vector3 vector, Vector3 other)
        {
            return Vector3.SqrMagnitude(vector - other);
        }

        public static Vector3 SetX(this Vector3 vector, float value)
        {
            return new Vector3(value, vector.y, vector.z);
        }
        
        public static Vector3 SetY(this Vector3 vector, float value)
        {
            return new Vector3(vector.x, value, vector.z);
        }
        
        public static Vector3 SetZ(this Vector3 vector, float value)
        {
            return new Vector3(vector.x, vector.y, value);
        }
    }
}
