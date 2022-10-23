using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace MassiveCore.Framework
{
    public class ClickSoundControl : BaseMonoBehaviour, IPointerClickHandler
    {
        [Inject]
        private readonly IProfile _profile;

        [Inject]
        private readonly ISounds _sounds;

        [SerializeField]
        private string _soundId = "click";

        public void OnPointerClick(PointerEventData eventData)
        {
            _sounds.PlaySoundByEnable(_soundId, _profile);
        }
    }
}
