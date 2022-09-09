using UnityEngine;

namespace MassiveCore.Framework
{
    public static class Vector2Extensions
    {
        public static bool EqualsTo(this Vector2 a, Vector2 b, float error = 0.01f)
        {
            return new Vector2EqualityComparer(error).Equals(a, b);
        }

        public static float RandomRange(this Vector2 vector)
        {
            return Random.Range(vector.x, vector.y);
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
