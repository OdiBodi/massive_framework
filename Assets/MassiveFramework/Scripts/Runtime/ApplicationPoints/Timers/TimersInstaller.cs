using Zenject;

namespace MassiveCore.Framework
{
    public class TimersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Timers>().AsSingle();
        }
    }
}
