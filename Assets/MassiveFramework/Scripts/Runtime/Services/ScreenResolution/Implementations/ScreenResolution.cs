using UniRx;
using UnityScreen = UnityEngine.Screen;

namespace MassiveCore.Framework
{
    public class ScreenResolution : IScreenResolution
    {
        private ReactiveProperty<Resolution> _resolution;

        public ReadOnlyReactiveProperty<Resolution> Resolution { get; private set; }

        public ScreenResolution()
        {
            InitializeResolutionReactiveProperties();
            SubscribeOnEveryUpdate();
        }

        private void InitializeResolutionReactiveProperties()
        {
            var resolution = new Resolution(UnityScreen.width, UnityScreen.height);
            _resolution = new ReactiveProperty<Resolution>(resolution);
            Resolution = _resolution.ToReadOnlyReactiveProperty();
        }

        private void SubscribeOnEveryUpdate()
        {
            Observable.EveryUpdate().Where(_ =>
            {
                var oldResolution = _resolution.Value;
                var newResolution = new Resolution(UnityScreen.width, UnityScreen.height);
                var result = oldResolution != newResolution;
                return result;
            }).Subscribe(_ =>
            {
                _resolution.Value = new Resolution(UnityScreen.width, UnityScreen.height);
            });
        }
    }
}
