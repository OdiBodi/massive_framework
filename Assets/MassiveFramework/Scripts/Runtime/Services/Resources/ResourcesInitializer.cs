using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class ResourcesInitializer : ServiceInitializer
    {
        [Inject]
        protected readonly ResourceFactory _resourceFactory;

        [Inject]
        protected readonly IResources _resources;

        [Inject]
        private void Inject(IProfile profile)
        {
            profile.PreLoading += () => InitializeProfileValues(profile);
        }

        public override UniTask<bool> Initialize()
        {
            BindResources();
            CompleteInitialize(true);
            return base.Initialize();
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
