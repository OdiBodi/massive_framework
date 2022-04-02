using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "game_config", menuName = "Massive Framework/Configs/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField]
        private LevelsConfig levelsConfig;

        [SerializeField]
        private EnvironmentsConfig environmentsConfig;

        [SerializeField]
        private VfxConfigs vfxConfigs;

        [SerializeField]
        private VibrationsConfig vibrationsConfig;

        public LevelsConfig LevelsConfig => levelsConfig;
        public EnvironmentsConfig EnvironmentsConfig => environmentsConfig;
        public VfxConfigs VfxConfigs => vfxConfigs;
        public VibrationsConfig VibrationsConfig => vibrationsConfig;
    }
}
