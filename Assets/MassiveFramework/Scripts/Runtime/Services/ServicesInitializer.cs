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
            InitializeLoadedReactiveProperties();
            InitializeServices();
        }

        private void InitializeLoadedReactiveProperties()
        {
            _initialized = new ReactiveProperty<bool>();
            Initialized = _initialized.ToReadOnlyReactiveProperty();
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
