using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public static class GraphicExtensions
    {
        public static void SetAlpha(this Graphic graphic, float alpha)
        {
            var color = graphic.color;
            graphic.color = new Color(color.r, color.g, color.b, alpha);
        }
    }
}
