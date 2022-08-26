using UnityEngine;

namespace MassiveCore.Framework
{
    public static class ColorExtensions
    {
        public static bool EqualsTo(this Color a, Color b, float error = 0.01f)
        {
            return new ColorEqualityComparer(error).Equals(a, b);
        }

        public static Color Alpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
    }
}
