using Zenject;

namespace MassiveCore.Framework
{
    public class ProfileInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
#if DEBUG
            Container.Bind<IProfile>().To<ProfilePrefs>().AsSingle();
#else
            Container.Bind<IProfile>().To<ProfilePrefs>().AsSingle();
#endif
        }
    }
}
