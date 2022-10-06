using UnityEngine;

namespace MassiveCore.Framework
{
    [RequireComponent(typeof(Camera))]
    public class CameraCustomRenderTarget : BaseMonoBehaviour
    {
        [SerializeField]
        private Vector2Int _resolution = -Vector2Int.one;

        private void Awake()
        {
            var width = _resolution.x <= 0 ? UnityEngine.Screen.width : _resolution.x; 
            var height = _resolution.y <= 0 ? UnityEngine.Screen.height : _resolution.y; 
            GetComponent<Camera>().targetTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
        }
    }
}
