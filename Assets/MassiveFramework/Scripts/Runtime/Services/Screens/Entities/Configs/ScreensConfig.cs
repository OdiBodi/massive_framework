using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [CreateAssetMenu(fileName = "screens_config", menuName = "Massive Framework/Configs/Screens Config")]
    public class ScreensConfig : Config
    {
        [SerializeField]
        private ScreenConfig[] _configs;

        public ScreenConfig[] Configs => _configs;
    }
}
