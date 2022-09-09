using UnityEngine;

namespace MassiveCore.Framework
{
    public static class CameraExtensions
    {
        public static Vector2 WorldToScreenPoint(this Camera camera, Canvas canvas, Vector3 worldPoint, Vector2 offset)
        {
            Vector2 screenPoint = camera.WorldToScreenPoint(worldPoint);
            var canvasScaleFactor = canvas.scaleFactor;
            return new Vector2
            (
                (screenPoint.x + offset.x) / canvasScaleFactor,
                (screenPoint.y + offset.y) / canvasScaleFactor
            );
        }
    }
}
