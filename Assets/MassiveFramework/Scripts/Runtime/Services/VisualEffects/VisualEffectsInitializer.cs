using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class VisualEffectsInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IPool _pool;

        [Inject]
        private readonly VisualEffectFactory _visualEffectFactory;

        public override UniTask<bool> Initialize()
        {
            BindPoolFactories();
            CompleteInitialize(true);
            return base.Initialize();
        }

        protected virtual void BindPoolFactories()
        {
            var factory = new VisualEffectPoolFactory(_visualEffectFactory);
            (_pool as Pool).BindFactory<VisualEffect>(factory);
        }
    }
}
