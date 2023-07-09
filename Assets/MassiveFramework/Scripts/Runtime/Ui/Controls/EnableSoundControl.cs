using UnityEngine;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class EnableSoundControl : BaseMonoBehaviour
    {
        [Inject]
        private readonly ISounds _sounds;

        [SerializeField]
        private string _soundId = "sound";

        private void OnEnable()
        {
            _sounds.PlaySound(_soundId);
        }
    }
}
