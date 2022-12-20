namespace MassiveCore.Framework
{
    public class AnalyticsInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
#if UNITY_EDITOR || UNITY_WEBGL
            Container.Bind<IAnalytics>().To<EditorAnalytics>().AsSingle();
#elif UNITY_IOS || UNITY_ANDROID
            Container.Bind<IAnalytics>().To<Analytics>().AsSingle();
#endif
        }
    }
}
