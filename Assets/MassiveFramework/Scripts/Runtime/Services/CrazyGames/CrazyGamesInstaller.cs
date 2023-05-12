using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class CrazyGamesInstaller : ServiceInstaller
    {
        [SerializeField]
        private CrazyGames _crazyGames;

        public override void InstallBindings()
        {
            Container.Bind<ICrazyGames>().FromInstance(_crazyGames).AsSingle();
        }
    }
}
