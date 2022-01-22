using Zenject;

namespace MassiveCore.Framework
{
    public class ProfileInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IProfile), typeof(ICustomProfile)).To<CustomProfilePrefs>().AsSingle();
        }
    }
}
