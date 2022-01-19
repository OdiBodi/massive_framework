using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "game_config", menuName = "Massive Framework/Configs/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField]
        private LevelsConfig levelsConfig;

        public LevelsConfig LevelsConfig => levelsConfig;
    }
}
