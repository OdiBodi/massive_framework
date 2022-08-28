using Zenject;

namespace MassiveCore.Framework
{
    public class ProfileInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IProfile>().To<ProfilePrefs>().AsSingle();
        }
    }
}
