using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public static class ColorExtensions
    {
        public static bool EqualsTo(this Color a, Color b, float error = 0.01f)
        {
            return new ColorEqualityComparer(error).Equals(a, b);
        }

        public static Color Color(this Color color, Color other)
        {
            return new Color(other.r, other.g, other.b, color.a);
        }

        public static Color Alpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        public static string Hex(this Color color)
        {
            return ColorUtility.ToHtmlStringRGBA(color);
        }
    }
}
