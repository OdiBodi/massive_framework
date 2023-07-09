using UnityEngine;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class EnableSoundControl : BaseMonoBehaviour
    {
        [Inject]
        private readonly IProfile _profile;

        [Inject]
        private readonly ISounds _sounds;

        [SerializeField]
        private string _soundId = "sound";

        private void OnEnable()
        {
            _sounds.PlaySoundByEnable(_soundId, _profile);
        }
    }
}
