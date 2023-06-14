using UniRx;

namespace MassiveCore.Framework.Runtime
{
    public interface IThemes
    {
        ReadOnlyReactiveProperty<ThemeConfig> Theme { get; }

        void Initialize();
        void ChangeTheme(string id);
    }
}
