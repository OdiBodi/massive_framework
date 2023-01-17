using System;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework.Runtime
{
    public class StatesInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IStates>().To<States>().AsSingle();
            Container.BindFactory<Type, IState<UniTask>, StateFactory<UniTask>>().FromFactory<StateCustomFactory<UniTask>>();
        }

        private void AOTCodeGeneration()
        {
            new StateCustomFactory<UniTask>(Container);
        }
    }
}
