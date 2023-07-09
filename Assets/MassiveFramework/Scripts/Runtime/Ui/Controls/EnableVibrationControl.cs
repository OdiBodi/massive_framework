using UnityEngine;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class EnableVibrationControl : BaseMonoBehaviour
    {
        [Inject]
        private readonly IVibrations _vibrations;

        [SerializeField]
        private string _vibrationId = "vibration";

        private void OnEnable()
        {
            _vibrations.Vibrate(_vibrationId);
        }
    }
}
