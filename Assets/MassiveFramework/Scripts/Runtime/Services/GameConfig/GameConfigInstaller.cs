using UnityEngine;

namespace MassiveCore.Framework
{
    public class GameConfigInstaller : ServiceInstaller
    {
        [SerializeField]
        private GameConfig _gameConfig;

        public override void InstallBindings()
        {
            Container.Bind<IGameConfig>().FromInstance(_gameConfig).AsSingle();
        }
    }
}
