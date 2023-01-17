namespace MassiveCore.Framework.Runtime
{
    public class AdsInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
#if UNITY_EDITOR
            Container.Bind<IAds>().To<EditorAds>().AsSingle();
#elif UNITY_IOS || UNITY_ANDROID
            Container.Bind<IAds>().To<EditorAds>().AsSingle(); // AppodealAds
#elif UNITY_WEBGL
            Container.Bind<IAds>().To<YandexAds>().AsSingle();
#endif
        }
    }
}
