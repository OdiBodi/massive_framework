namespace MassiveCore.Framework
{
    public class AdsInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
#if DEBUG
            Container.Bind<IAds>().To<EditorAds>().AsSingle();
#else
            Container.Bind<IAds>().To<EditorAds>().AsSingle(); // AppodealAds
#endif
        }
    }
}
