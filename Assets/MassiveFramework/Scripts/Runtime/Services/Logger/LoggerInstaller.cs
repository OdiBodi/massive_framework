namespace MassiveCore.Framework
{
    public class LoggerInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
#if DEBUG
            Container.Bind<ILogger>().To<DebugLogger>().AsSingle();
#else
            Container.Bind<ILogger>().To<ReleaseLogger>().AsSingle();
#endif
        }
    }
}
