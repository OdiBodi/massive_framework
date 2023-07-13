using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public static class Vector2Extensions
    {
        public static bool EqualsTo(this Vector2 a, Vector2 b, float error = 0.01f)
        {
            return new Vector2EqualityComparer(error).Equals(a, b);
        }

        public static Vector2 Rotate(this Vector2 vector, float angleInDegrees)
        {
            var rotation = Quaternion.Euler(0, 0, angleInDegrees);
            var rotated = rotation * vector;
            return rotated;
        }

        public static float RandomRange(this Vector2 vector)
        {
            return Random.Range(vector.x, vector.y);
        }

        public static float SqrDistance(this Vector2 vector, Vector2 other)
        {
            return Vector2.SqrMagnitude(vector - other);
        }

        public static Vector2 SetX(this Vector2 vector, float value)
        {
            return new Vector2(value, vector.y);
        }
        
        public static Vector2 SetY(this Vector2 vector, float value)
        {
            return new Vector2(vector.x, value);
        }
    }
}
