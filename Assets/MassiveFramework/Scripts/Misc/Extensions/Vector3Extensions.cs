using UnityEngine;

namespace MassiveCore.Framework
{
    public static class Vector3Extensions
    {
        public static bool EqualsTo(this Vector3 a, Vector3 b, float error = 0.01f)
        {
            return new Vector3EqualityComparer(error).Equals(a, b);
        }

        public static float RandomRange(this Vector2 vector)
        {
            return Random.Range(vector.x, vector.y);
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
