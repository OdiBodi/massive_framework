using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [CreateAssetMenu(fileName = "visual_effect_config", menuName = "Massive Framework/Configs/Visual Effect Config")]
    public class VisualEffectConfig : ScriptableObject
    {
        [SerializeField]
        private string _id;

        [SerializeField]
        private VisualEffect _visualEffect;

        [SerializeField]
        private float _cooldownTime;

        public string Id => _id;
        public VisualEffect VisualEffect => _visualEffect;
        public float CooldownTime => _cooldownTime;
    }
}
