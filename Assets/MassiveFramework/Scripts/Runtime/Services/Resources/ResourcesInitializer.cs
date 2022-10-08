using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class ResourcesInitializer : ServiceInitializer
    {
        [Inject]
        private readonly ResourceFactory _resourceFactory;

        [Inject]
        private readonly IResources _resources;

        public override UniTask<bool> Initialize()
        {
            BindResources();
            CompleteInitialize(true);
            return base.Initialize();
        }

        [Inject]
        private void Inject(IProfile profile)
        {
            profile.OnPreLoading += () => InitializeProfileValues(profile);
        }

        protected virtual void BindResources()
        {
            var currencyResource = _resourceFactory.Create<CurrencyResource>();
            _resources.BindResource(currencyResource);
        }

        protected virtual void InitializeProfileValues(IProfile profile)
        {
            profile.Property(ProfileIds.Currency, 0);
        }
    }
}
