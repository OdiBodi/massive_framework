using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    [RequireComponent(typeof(Canvas))]
    public class CameraToCanvas : BaseMonoBehaviour
    {
        [Inject]
        private readonly ICameras _cameras;

        [SerializeField]
        private string _cameraName = "main";

        [SerializeField]
        private float _planeDistance = 100f;

        private void Awake()
        {
            var canvas = GetComponent<Canvas>();
            canvas.worldCamera = _cameras.CameraBy(_cameraName);
            canvas.planeDistance = _planeDistance;
        }
    }
}
