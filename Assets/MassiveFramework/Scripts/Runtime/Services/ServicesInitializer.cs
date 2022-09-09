using System.Collections.Generic;
using System.Linq;
using UniRx;
using Unity.Linq;

namespace MassiveCore.Framework
{
    public class ServicesInitializer : BaseMonoBehaviour
    {
        private ReactiveProperty<bool> initialized;

        public ReadOnlyReactiveProperty<bool> Initialized { get; private set; }

        private IEnumerable<IServiceInitializer> Initializers => CacheGameObject.Descendants()
            .OfType<IServiceInitializer>().Where(service => (service as BaseMonoBehaviour).Activity());

        private void Awake()
        {
            InitLoadedReactiveProperties();
            InitializeServices();
        }

        private void InitLoadedReactiveProperties()
        {
            initialized = new ReactiveProperty<bool>();
            var initializedReadOnlyProperty = initialized.ToReadOnlyReactiveProperty();
            Initialized = new ReadOnlyReactiveProperty<bool>(initializedReadOnlyProperty);
        }

        private async void InitializeServices()
        {
            foreach (var initializer in Initializers)
            {
                await initializer.Initialize();
            }
            initialized.Value = true;
        }
    }
}
