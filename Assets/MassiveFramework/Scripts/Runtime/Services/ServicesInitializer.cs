using System.Collections.Generic;
using System.Linq;
using UniRx;
using Unity.Linq;

namespace MassiveCore.Framework
{
    public class ServicesInitializer : BaseMonoBehaviour
    {
        private ReactiveProperty<bool> _initialized;

        public ReadOnlyReactiveProperty<bool> Initialized { get; private set; }

        private IEnumerable<IServiceInitializer> Initializers => CacheGameObject.Descendants()
            .OfInterfaceComponent<IServiceInitializer>().Where(service => (service as BaseMonoBehaviour).Activity());

        private void Awake()
        {
            InitLoadedReactiveProperties();
            InitializeServices();
        }

        private void InitLoadedReactiveProperties()
        {
            _initialized = new ReactiveProperty<bool>();
            var initializedReadOnlyProperty = _initialized.ToReadOnlyReactiveProperty();
            Initialized = new ReadOnlyReactiveProperty<bool>(initializedReadOnlyProperty);
        }

        private async void InitializeServices()
        {
            foreach (var initializer in Initializers)
            {
                var result = await initializer.Initialize();
                var serviceName = (initializer as BaseMonoBehaviour).name;
                if (result)
                {
                    _logger.Print($"Service \"{serviceName}\" initialized!");
                }
                else
                {
                    _logger.PrintError($"Service \"{serviceName}\" didn't initialize!");
                }
            }
            _initialized.Value = true;
        }
    }
}
