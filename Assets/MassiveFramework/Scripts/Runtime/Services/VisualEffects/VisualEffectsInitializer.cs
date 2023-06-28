using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class VisualEffectsInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IPool _pool;

        [Inject]
        private readonly VisualEffectPlaceholderFactory _visualEffectPlaceholderFactory;

        [SerializeField]
        private Transform _root;

        public override UniTask<bool> Initialize()
        {
            BindPoolFactories();
            CompleteInitialize(true);
            return base.Initialize();
        }

        protected virtual void BindPoolFactories()
        {
            var factory = new VisualEffectAbstractFactory(_visualEffectPlaceholderFactory);
            (_pool as Pool).BindObjectPool<VisualEffect>(factory, null, _root, 100);
        }
    }
}
