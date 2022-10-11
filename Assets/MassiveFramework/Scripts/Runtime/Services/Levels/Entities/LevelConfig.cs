using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "level_config", menuName = "Massive Framework/Configs/Level Config")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField]
        private Level _prefab;

        public Level Prefab => _prefab;
    }
}
