using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [CreateAssetMenu(fileName = "themes_config", menuName = "Massive Framework/Configs/Themes Config")]
    public class ThemesConfig : Config
    {
        [SerializeField]
        private ThemeConfig[] configs;

        public ThemeConfig[] Configs => configs;
    }
}
