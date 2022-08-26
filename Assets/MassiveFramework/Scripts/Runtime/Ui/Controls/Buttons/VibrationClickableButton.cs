using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class VibrationClickableButton : BaseMonoBehaviour
    {
        [Inject]
        private readonly IVibrations vibrations;

        [SerializeField]
        private BaseButton button;

        [SerializeField]
        private string vibrationId;

        private void Start()
        {
            SubscribeOnButton();
        }

        private void SubscribeOnButton()
        {
            button.OnClicked += () => vibrations.Vibrate(vibrationId);
        }
    }
}
