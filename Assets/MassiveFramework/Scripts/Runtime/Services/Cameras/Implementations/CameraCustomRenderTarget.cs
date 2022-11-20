using UniRx;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    [RequireComponent(typeof(Camera))]
    public class CameraCustomRenderTarget : BaseMonoBehaviour
    {
        private enum Depth
        {
            [InspectorName("0"), Number(0)]
            _0,
            [InspectorName("16"), Number(16)]
            _16,
            [InspectorName("24"), Number(24)]
            _24,
            [InspectorName("32"), Number(32)]
            _32
        }

        [Inject]
        private readonly IScreenResolution _screenResolution;

        [SerializeField]
        private Resolution _resolution = new(-1, -1);

        [SerializeField]
        private Depth _depth = Depth._24;

        [SerializeField]
        private RenderTextureFormat _format = RenderTextureFormat.ARGB32;

        private Camera _camera;

        private void Awake()
        {
            CacheCamera();
            SubscribeOnScreenResolution();
        }

        private void CacheCamera()
        {
            _camera = GetComponent<Camera>();
        }

        private void SubscribeOnScreenResolution()
        {
            _screenResolution.Resolution.Subscribe(_ => Reinitialize()).AddTo(this);
        }

        private void Reinitialize()
        {
            if (_camera.targetTexture)
            {
                _camera.targetTexture.Release();
                _camera.targetTexture = null;
            }
            Initialize();
        }

        private void Initialize()
        {
            var screenResolution = _screenResolution.Resolution.Value;
            var width = _resolution.width <= 0 ? screenResolution.width : _resolution.width; 
            var height = _resolution.height <= 0 ? screenResolution.height : _resolution.height;
            var depth = _depth.Number();
            _camera.targetTexture = new RenderTexture(width, height, depth, _format, 0);
        }
    }
}
