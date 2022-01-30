using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "vfx_config", menuName = "Massive Framework/Configs/Vfx Config")]
    public class VfxConfig : ScriptableObject
    {
        [SerializeField]
        private string id;

        [SerializeField]
        private Vfx vfx;

        public string Id => id;
        public Vfx Vfx => vfx;
    }
}
