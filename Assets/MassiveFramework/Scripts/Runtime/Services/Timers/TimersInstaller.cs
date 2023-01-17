namespace MassiveCore.Framework.Runtime
{
    public class TimersInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITimers>().To<Timers>().AsSingle();
        }
    }
}
