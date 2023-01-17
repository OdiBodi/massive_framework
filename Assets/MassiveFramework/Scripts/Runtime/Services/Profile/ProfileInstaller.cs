namespace MassiveCore.Framework.Runtime
{
    public class ProfileInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IProfile>().To<ProfilePrefs>().AsSingle();
        }
    }
}
