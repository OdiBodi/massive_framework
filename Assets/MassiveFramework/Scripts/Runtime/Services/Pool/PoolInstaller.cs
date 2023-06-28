namespace MassiveCore.Framework.Runtime
{
    public class PoolInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPool>().To<Pool>().AsSingle();
        }
    }
}
