using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

namespace MassiveCore.Framework
{
    public class CurrencyResource : IResource
    {
        [Inject]
        private readonly IProfile _profile;

        [Inject]
        private readonly IConfigs _configs;

        public string Id => Configs<ResourceConfig>().First().Id;
        public ReadOnlyReactiveProperty<int> Amount { get; private set; }
        private ReactiveProperty<int> CurrencyProperty => _profile.Property<int>(ProfileIds.Currency);

        [Inject]
        private void Inject()
        {
            InitAmountReactiveProperty();
        }

        public IEnumerable<T> Configs<T>()
            where T : ResourceConfig
        {
            var resourcesConfig = _configs.Config<ResourcesConfig>(); 
            var configs = resourcesConfig.ConfigsBy<T>("currency");
            return configs;
        }

        public async UniTask<bool> Spend(int amount)
        {
            if (CurrencyProperty.Value < amount)
            {
                return false;
            }
            CurrencyProperty.Value -= amount;
            return true;
        }

        public async UniTask<bool> Increase(int amount)
        {
            CurrencyProperty.Value += amount;
            return true;
        }

        public UniTask Reset(int value)
        {
            CurrencyProperty.Value = value;
            return UniTask.CompletedTask;
        }

        private void InitAmountReactiveProperty()
        {
            Amount = CurrencyProperty.ToReadOnlyReactiveProperty();
        }
    }
}
