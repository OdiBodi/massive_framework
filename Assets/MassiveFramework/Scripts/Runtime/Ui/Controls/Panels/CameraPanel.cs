using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MassiveCore.Framework
{
    public class CameraPanel : BaseMonoBehaviour
    {
        [Inject]
        private readonly ICameras cameras;

        [SerializeField]
        private RawImage image;

        [SerializeField]
        private string cameraName;

        private void Awake()
        {
            image.texture = cameras.CameraBy(cameraName).targetTexture;
        }
    }
}
