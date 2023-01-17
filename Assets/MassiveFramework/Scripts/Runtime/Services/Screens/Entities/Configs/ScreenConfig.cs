using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [CreateAssetMenu(fileName = "screen_config", menuName = "Massive Framework/Configs/Screen Config")]
    public class ScreenConfig : ScriptableObject
    {
        [SerializeField]
        private Screen _prefab;

        public Screen Prefab => _prefab;
    }
}
