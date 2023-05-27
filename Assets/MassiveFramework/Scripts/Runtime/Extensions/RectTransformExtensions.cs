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

        public static Bounds WorldBounds(this RectTransform rectTransform)
        {
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            var bounds = new Bounds(corners[0], Vector3.zero);
            for (var i = 1; i < 4; i++)
            {
                bounds.Encapsulate(corners[i]);
            }
            return bounds;
        }
        
        public static bool Contains(this RectTransform rectTransform, RectTransform otherRectTransform)
        {
            var bounds0 = rectTransform.WorldBounds();
            var bounds1 = otherRectTransform.WorldBounds();
            return bounds0.Intersects(bounds1);
        }

        public static bool Contains(this RectTransform rectTransform, Vector2 position)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, position);
        }

        public static Vector2 ToLocalPosition(this RectTransform rectTransform, RectTransform local)
        {
            var worldPosition = rectTransform.position;
            var screenPosition = RectTransformUtility.WorldToScreenPoint(null, worldPosition);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(local, screenPosition, null, out var localPosition);
            return localPosition;
        }
    }
}
