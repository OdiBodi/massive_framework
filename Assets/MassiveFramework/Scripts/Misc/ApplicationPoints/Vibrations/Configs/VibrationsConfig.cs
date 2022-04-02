using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "vibrations_config", menuName = "Massive Framework/Configs/Vibrations Config")]
    public class VibrationsConfig : ScriptableObject
    {
        [SerializeField]
        private VibrationConfig[] iosConfigs;

        [SerializeField]
        private VibrationConfig[] androidConfigs;

        public VibrationConfig[] Configs
        {
            get
            {
#if UNITY_IOS
                return iosConfigs;
#elif UNITY_ANDROID
                return androidConfigs;
#endif
            }
        }
    }
}
