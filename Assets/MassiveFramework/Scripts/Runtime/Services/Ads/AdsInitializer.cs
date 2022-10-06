using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class AdsInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IAds _ads;

        public override async UniTask<bool> Initialize()
        {
            await _ads.Initialize();
            CompleteInitialize(true);
            return await base.Initialize();
        }
    }
}
