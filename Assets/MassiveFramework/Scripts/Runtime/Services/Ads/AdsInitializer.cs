using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class AdsInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IAds _ads;

        public override async UniTask<bool> Initialize()
        {
            var result = await _ads.Initialize();
            CompleteInitialize(result);
            return await base.Initialize();
        }
    }
}
