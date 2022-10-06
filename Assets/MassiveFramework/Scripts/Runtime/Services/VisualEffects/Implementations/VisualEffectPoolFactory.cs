using UnityEngine;

namespace MassiveCore.Framework
{
    public class VisualEffectPoolFactory : IPoolObjectFactory
    {
        private readonly VisualEffect.Factory _factory;

        public VisualEffectPoolFactory(VisualEffect.Factory factory)
        {
            _factory = factory;
        }

        public IPoolObject Create(string id, Transform root)
        {
            return _factory.Create(id);
        }
    }
}
