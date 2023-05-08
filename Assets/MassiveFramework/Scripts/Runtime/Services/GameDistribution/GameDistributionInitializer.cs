using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class GameDistributionInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IGameDistribution _gameDistribution;

        public override UniTask<bool> Initialize()
        {
            _gameDistribution.Initialized += OnGameDistributionInitialized;
            _gameDistribution.Initialize();
            return base.Initialize();
        }

        private void OnGameDistributionInitialized(bool result)
        {
            CompleteInitialize(result);
#if DEBUG
            _gameDistribution.ShowConsole();
#endif
        }
    }
}
