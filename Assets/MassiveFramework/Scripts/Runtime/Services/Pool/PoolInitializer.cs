using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class PoolInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IPool _pool;

        public override async UniTask<bool> Initialize()
        {
            _pool.Initialize();
            CompleteInitialize(true);
            return await base.Initialize();
        }
    }
}
