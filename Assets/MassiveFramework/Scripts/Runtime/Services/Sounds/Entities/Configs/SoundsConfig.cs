using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "sounds_config", menuName = "Massive Framework/Configs/Sounds Config")]
    public class SoundsConfig : Config
    {
        [SerializeField]
        private SoundConfig[] _configs;

        public SoundConfig[] Configs => _configs;
    }
}
