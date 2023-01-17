using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class ClickVibrationControl : BaseMonoBehaviour, IPointerClickHandler
    {
        [Inject]
        private readonly IVibrations _vibrations;

        [SerializeField]
        private string _vibrationId = "click";

        public void OnPointerClick(PointerEventData eventData)
        {
            _vibrations.Vibrate(_vibrationId);
        }
    }
}
