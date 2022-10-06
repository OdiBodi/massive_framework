using System;

namespace MassiveCore.Framework
{
    public class GameStatesInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameStates>().To<GameStates>().AsSingle();
            Container.BindFactory<Type, IGameState, GameStateFactory>().FromFactory<GameStateCustomFactory>();
        }
    }
}
