using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class GameConfigInstaller : MonoInstaller
    {
        [SerializeField]
        private GameConfig gameConfig;

        public override void InstallBindings()
        {
            Container.Bind<GameConfig>().FromInstance(gameConfig).AsSingle();
        }
    }
}
