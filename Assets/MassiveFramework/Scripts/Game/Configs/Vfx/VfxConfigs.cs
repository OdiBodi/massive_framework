using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "vfx_configs", menuName = "Massive Framework/Configs/Vfx Configs")]
    public class VfxConfigs : ScriptableObject
    {
        [SerializeField]
        private VfxConfig[] configs;

        public VfxConfig[] Configs => configs;
    }
}
