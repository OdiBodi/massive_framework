namespace MassiveCore.Framework.Runtime
{
    public class ScreenResolutionInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IScreenResolution>().To<ScreenResolution>().AsSingle();
        }
    }
}
