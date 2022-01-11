using UnityEngine;

namespace MassiveCore.Framework
{
    public static class Vector2Extensions
    {
        public static float RandomRange(this Vector2 vector)
        {
            return Random.Range(vector.x, vector.y);
        }
    }
}
