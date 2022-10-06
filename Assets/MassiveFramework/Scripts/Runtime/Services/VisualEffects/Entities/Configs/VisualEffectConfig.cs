using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "visual_effect_config", menuName = "Massive Framework/Configs/Visual Effect Config")]
    public class VisualEffectConfig : ScriptableObject
    {
        [SerializeField]
        private string _id;

        [SerializeField]
        private VisualEffect _visualEffect;

        public string Id => _id;
        public VisualEffect VisualEffect => _visualEffect;
    }
}
