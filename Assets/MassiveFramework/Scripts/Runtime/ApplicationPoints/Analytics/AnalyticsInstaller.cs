using Zenject;

namespace MassiveCore.Framework
{
    public class AnalyticsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
#if DEBUG
            Container.Bind<IAnalytics>().To<EditorAnalytics>().AsSingle();
#else
            Container.Bind<IAnalytics>().To<Analytics>().AsSingle();
#endif
        }
    }
}
