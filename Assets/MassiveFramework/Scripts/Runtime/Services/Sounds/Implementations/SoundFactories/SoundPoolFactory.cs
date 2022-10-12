using UnityEngine;

namespace MassiveCore.Framework
{
    public class SoundPoolFactory : IPoolObjectFactory
    {
        private readonly SoundFactory _factory;

        public SoundPoolFactory(SoundFactory factory)
        {
            _factory = factory;
        }

        public IPoolObject Create(string id, Transform root)
        {
            return _factory.Create(id);
        }
    }
}
