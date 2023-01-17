using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [CreateAssetMenu(fileName = "levels_config", menuName = "Massive Framework/Configs/Levels Config")]
    public class LevelsConfig : Config
    {
        [SerializeField]
        private LevelConfig[] _configs;

        public LevelConfig[] Configs => _configs;
    }
}
