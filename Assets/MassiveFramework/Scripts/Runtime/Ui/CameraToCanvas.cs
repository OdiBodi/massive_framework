using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    [RequireComponent(typeof(Canvas))]
    public class CameraToCanvas : BaseMonoBehaviour
    {
        [Inject]
        private readonly ICameras cameras;

        [SerializeField]
        private string cameraName;

        [SerializeField]
        private float planeDistance = 100f;

        private void Awake()
        {
            var canvas = GetComponent<Canvas>();
            canvas.worldCamera = cameras.CameraBy(cameraName);
            canvas.planeDistance = planeDistance;
        }
    }
}
