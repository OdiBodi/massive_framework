using UnityEngine;

namespace MassiveCore.Framework
{
    [RequireComponent(typeof(Camera))]
    public class CameraCustomRenderTarget : BaseMonoBehaviour
    {
        [SerializeField]
        private Vector2Int resolution = -Vector2Int.one;

        private void Awake()
        {
            var width = resolution.x <= 0 ? UnityEngine.Screen.width : resolution.x; 
            var height = resolution.y <= 0 ? UnityEngine.Screen.height : resolution.y; 
            GetComponent<Camera>().targetTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
        }
    }
}
