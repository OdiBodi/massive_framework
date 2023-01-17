using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [CreateAssetMenu(fileName = "visual_effects_config", menuName = "Massive Framework/Configs/Visual Effects Config")]
    public class VisualEffectsConfig : Config
    {
        [SerializeField]
        private VisualEffectConfig[] _configs;

        public VisualEffectConfig[] Configs => _configs;
    }
}
