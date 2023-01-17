using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [CreateAssetMenu(fileName = "vibrations_config", menuName = "Massive Framework/Configs/Vibrations Config")]
    public class VibrationsConfig : Config
    {
#if UNITY_IOS
        [SerializeField]
        private VibrationConfig[] _iosConfigs;
#elif UNITY_ANDROID
        [SerializeField]
        private VibrationConfig[] _androidConfigs;
#else
        private readonly VibrationConfig[] _configs = {};
#endif
        public VibrationConfig[] Configs
        {
            get
            {
#if UNITY_IOS
                return iosConfigs;
#elif UNITY_ANDROID
                return _androidConfigs;
#else
                return _configs;
#endif
            }
        }
    }
}
