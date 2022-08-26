using Zenject;

namespace MassiveCore.Framework
{
    public class LoggerInstaller : MonoInstaller
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
