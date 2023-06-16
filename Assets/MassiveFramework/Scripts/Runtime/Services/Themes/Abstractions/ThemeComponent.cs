using UniRx;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public abstract class ThemeComponent : BaseMonoBehaviour
    {
        [Inject]
        private readonly IThemes _themes;

        private void Awake()
        {
            SubscribeOnThemes();
        }

        private void SubscribeOnThemes()
        {
            _themes.Theme.Where(config => config).Subscribe(OnThemeChanged).AddTo(this);
        }

        protected abstract void OnThemeChanged(ThemeConfig config);
    }
}
