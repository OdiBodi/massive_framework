using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "levels_config", menuName = "Configs/Levels Config")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField]
        private LevelConfig[] configs;

        public LevelConfig[] Configs => configs;
    }
}
