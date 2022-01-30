using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "game_config", menuName = "Massive Framework/Configs/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField]
        private LevelsConfig levelsConfig;

        [SerializeField]
        private VfxConfigs vfxConfigs;

        public LevelsConfig LevelsConfig => levelsConfig;
        public VfxConfigs VfxConfigs => vfxConfigs;
    }
}
