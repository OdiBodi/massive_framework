using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class CameraPanel : BaseMonoBehaviour
    {
        [Inject]
        private readonly IScreenResolution _screenResolution;

        [Inject]
        private readonly ICameras _cameras;

        [SerializeField]
        private RawImage _image;

        [SerializeField]
        private string _cameraName = "main";

        private void Awake()
        {
            SubscribeOnScreenResolution();
        }

        private void SubscribeOnScreenResolution()
        {
            _screenResolution.Resolution.Subscribe(_ => UpdateImageTexture()).AddTo(this);
        }
        
        private void UpdateImageTexture()
        {
            _image.texture = _cameras.CameraBy(_cameraName).targetTexture;
        }
    }
}
