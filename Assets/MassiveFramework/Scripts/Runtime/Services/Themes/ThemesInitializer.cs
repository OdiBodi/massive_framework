using System.Linq;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class ThemesInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IProfile _profile;

        [Inject]
        private readonly IConfigs _configs;

        [Inject]
        private readonly IThemes _themes;

        private ThemeConfig[] ThemeConfigs => _configs.Config<ThemesConfig>().Configs;

        [Inject]
        private void Inject(IProfile profile)
        {
            profile.PreLoading += () => InitializeProfileValues(profile);
        }

        public override UniTask<bool> Initialize()
        {
            InitializeTheme();
            CompleteInitialize(true);
            return base.Initialize();
        }

        private void InitializeProfileValues(IProfile profile)
        {
            var defaultTheme = ThemeConfigs[0].Id;
            profile.Property(ProfileIds.Theme, defaultTheme);
        }

        private void InitializeTheme()
        {
            var theme = _profile.Property<string>(ProfileIds.Theme).Value;
            if (ThemeConfigs.All(config => config.Id != theme))
            {
                theme = ThemeConfigs[0].Id;
            }
            _themes.Initialize();
            _themes.ChangeTheme(theme);
        }
    }
}
