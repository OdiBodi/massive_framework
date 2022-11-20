namespace MassiveCore.Framework
{
    public class AnalyticsInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
#if UNITY_IOS || UNITY_ANDROID
            Container.Bind<IAnalytics>().To<Analytics>().AsSingle();
#else
            Container.Bind<IAnalytics>().To<EditorAnalytics>().AsSingle();
#endif
        }
    }
}
