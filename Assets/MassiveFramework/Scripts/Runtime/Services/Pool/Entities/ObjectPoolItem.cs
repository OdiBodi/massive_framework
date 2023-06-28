using MassiveCore.Framework.Runtime.Patterns;

namespace MassiveCore.Framework.Runtime
{
    public struct ObjectPoolItem
    {
        public IObjectPool<IPoolObject> ObjectPool;
        public IPoolObjectArguments PoolObjectArguments;
    }
}
