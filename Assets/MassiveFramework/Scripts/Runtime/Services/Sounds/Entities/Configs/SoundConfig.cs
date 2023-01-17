using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [CreateAssetMenu(fileName = "sound_config", menuName = "Massive Framework/Configs/Sound Config")]
    public class SoundConfig : ScriptableObject
    {
        [SerializeField]
        private string _id;

        [SerializeField]
        private Sound _sound;

        [SerializeField]
        private float _cooldownTime;

        public string Id => _id;
        public Sound Sound => _sound;
        public float CooldownTime => _cooldownTime;
    }
}
