using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "levels_config", menuName = "Massive Framework/Configs/Levels Config")]
    public class LevelsConfig : Config
    {
        [SerializeField]
        private LevelConfig[] configs;

        public LevelConfig[] Configs => configs;
    }
}
