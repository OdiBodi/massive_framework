using Zenject;

namespace MassiveCore.Framework
{
    public class PoolPoint : ApplicationPoint
    {
        [Inject]
        private readonly Pool pool;

        [Inject]
        private readonly Vfx.Factory vfxFactory;

        public override void Init()
        {
            pool.AddFactory<Vfx>(id => vfxFactory.Create(id).CacheGameObject);
            Complete();
        }
    }
}
