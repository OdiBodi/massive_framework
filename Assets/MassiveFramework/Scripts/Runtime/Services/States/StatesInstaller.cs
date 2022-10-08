using System;

namespace MassiveCore.Framework
{
    public class StatesInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IStates>().To<States>().AsSingle();
            Container.BindFactory<Type, IState, StateFactory>().FromFactory<StateCustomFactory>();
        }
    }
}
