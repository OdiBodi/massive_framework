using Zenject;

namespace MassiveCore.Framework
{
    public class PoolInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Pool>().FromComponentInHierarchy().AsSingle();
        }
    }
}
