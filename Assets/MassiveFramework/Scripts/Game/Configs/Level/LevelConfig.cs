using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "level_config", menuName = "Configs/Level Config")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField]
        private Level prefab;

        public Level Prefab => prefab;
    }
}
