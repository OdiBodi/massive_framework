using UnityEngine;

namespace MassiveCore.Framework
{
    public class GameConfigInstaller : ServiceInstaller
    {
        [SerializeField]
        private GameConfig gameConfig;

        public override void InstallBindings()
        {
            Container.Bind<IGameConfig>().To<GameConfig>().FromInstance(gameConfig).AsSingle();
        }
    }
}
