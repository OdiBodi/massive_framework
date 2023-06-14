using System.Linq;
using UniRx;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class Themes : IThemes
    {
        [Inject]
        private readonly IProfile _profile;

        [Inject]
        private readonly IConfigs _configs;

        private readonly ReactiveProperty<ThemeConfig> _theme;

        public Themes()
        {
            _theme = new ReactiveProperty<ThemeConfig>();
            Theme = _theme.ToReadOnlyReactiveProperty();
        }

        private ReactiveProperty<string> ProfileThemeProperty => _profile.Property<string>(ProfileIds.Theme);
        private ThemeConfig[] ThemeConfigs => _configs.Config<ThemesConfig>().Configs;
        public ReadOnlyReactiveProperty<ThemeConfig> Theme { get; }

        public void Initialize()
        {
            SubscribeOnTheme();
        }

        public void ChangeTheme(string id)
        {
            var themeConfig = ThemeConfigs.FirstOrDefault(config => config.Id == id);
            if (!themeConfig)
            {
                return;
            }
            _theme.Value = themeConfig;
        }

        private void SubscribeOnTheme()
        {
            _theme.Where(config => config).Subscribe(config => ProfileThemeProperty.Value = config.Id);
        }
    }
}
