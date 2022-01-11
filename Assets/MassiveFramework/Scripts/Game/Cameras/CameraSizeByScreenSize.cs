using UnityEngine;

namespace MassiveCore.Framework
{
    [RequireComponent(typeof(Camera))]
    public class CameraSizeByScreenSize : BaseMonoBehaviour
    {
        [SerializeField]
        private Vector2 referenceResolution = new Vector2(1125f, 2436f);

        [SerializeField, Range(0f, 1f)]
        private float widthOrHeight;

        private void Awake()
        {
            var targetAspect = referenceResolution.x / referenceResolution.y;

            var camera = GetComponent<Camera>();
            if (camera.orthographic)
            {
                var originSize = camera.orthographicSize;
                var targetSize = originSize * (targetAspect / camera.aspect);
                camera.orthographicSize = Mathf.Lerp(targetSize, originSize, widthOrHeight);
            }
            else
            {
                var originFov = camera.fieldOfView;
                var horizontalFov = ComputeVerticalFov(originFov, 1 / targetAspect);
                var targetFov = ComputeVerticalFov(horizontalFov, camera.aspect);
                camera.fieldOfView = Mathf.Lerp(targetFov, originFov, widthOrHeight);
            }
        }

        private static float ComputeVerticalFov(float horizontalFovInDegrees, float aspectRatio)
        {
            var horizontalFovInRadians = horizontalFovInDegrees * Mathf.Deg2Rad;
            var verticalFovInRadians = 2f * Mathf.Atan(Mathf.Tan(horizontalFovInRadians / 2f) / aspectRatio);
            return verticalFovInRadians * Mathf.Rad2Deg;
        }
    }
}
