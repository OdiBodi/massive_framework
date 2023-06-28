using MassiveCore.Framework.Runtime.Patterns;

namespace MassiveCore.Framework.Runtime
{
    public class PoolObject : BaseMonoBehaviour, IPoolObject, IAbstractProduct
    {
        protected PoolObjectArguments _poolObjectArguments;

        public virtual string Id => name;

        public virtual void Request(IPoolObjectArguments poolObjectArguments)
        {
            _poolObjectArguments = poolObjectArguments as PoolObjectArguments;
            CacheTransform.SetParent(_poolObjectArguments.Root);
            this.ChangeActivity(true);
        }

        public virtual void Return()
        {
            CacheTransform.SetParent(_poolObjectArguments.Root);
            this.ChangeActivity(false);
        }
    }
}
