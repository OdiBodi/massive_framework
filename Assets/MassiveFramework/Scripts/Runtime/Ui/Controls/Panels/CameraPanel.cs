using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MassiveCore.Framework
{
    public class CameraPanel : BaseMonoBehaviour
    {
        [Inject]
        private readonly ICameras _cameras;

        [SerializeField]
        private RawImage _image;

        [SerializeField]
        private string _cameraName = "main";

        private void Awake()
        {
            _image.texture = _cameras.CameraBy(_cameraName).targetTexture;
        }
    }
}
