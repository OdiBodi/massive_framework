using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public static class RectTransformExtensions
    {
        public static Vector3 WorldPosition(this RectTransform rectTransform, Camera camera)
        {
            return camera.ScreenToWorldPoint(rectTransform.position);
        }

        public static Vector2 ScreenPosition(this RectTransform rectTransform, Camera camera, Canvas canvas)
        {
            var position = rectTransform.position;
            var viewPoint = new Vector2(position.x / UnityEngine.Screen.width, position.y / UnityEngine.Screen.height);
            var screenPoint = camera.ViewportToScreenPoint(viewPoint);
            var canvasScaleFactor = canvas.scaleFactor;
            return new Vector2
            (
                screenPoint.x / canvasScaleFactor,
                screenPoint.y / canvasScaleFactor
            );
        }
    }
}
