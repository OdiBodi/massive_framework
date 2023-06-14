namespace MassiveCore.Framework.Runtime
{
    public class ThemesInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IThemes>().To<Themes>().AsSingle();
        }
    }
}
