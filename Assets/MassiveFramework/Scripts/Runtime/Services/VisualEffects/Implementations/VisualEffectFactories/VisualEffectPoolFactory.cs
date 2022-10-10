using UnityEngine;

namespace MassiveCore.Framework
{
    public class VisualEffectPoolFactory : IPoolObjectFactory
    {
        private readonly VisualEffectFactory _factory;

        public VisualEffectPoolFactory(VisualEffectFactory factory)
        {
            _factory = factory;
        }

        public IPoolObject Create(string id, Transform root)
        {
            return _factory.Create(id);
        }
    }
}
