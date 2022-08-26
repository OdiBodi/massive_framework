using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MassiveCore.Framework
{
    public class CameraPanel : BaseMonoBehaviour
    {
        [Inject]
        private readonly Cameras cameras;

        [SerializeField]
        private RawImage image;

        [SerializeField]
        private string cameraName;

        private void Awake()
        {
            image.texture = cameras.CameraByName(cameraName).targetTexture;
        }
    }
}
