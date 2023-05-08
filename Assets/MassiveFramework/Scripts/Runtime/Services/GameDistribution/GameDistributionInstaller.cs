using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class GameDistributionInstaller : ServiceInstaller
    {
        [SerializeField]
        private GameDistribution _gameDistribution;

        public override void InstallBindings()
        {
            Container.Bind<IGameDistribution>().FromInstance(_gameDistribution).AsSingle();
        }
    }
}
