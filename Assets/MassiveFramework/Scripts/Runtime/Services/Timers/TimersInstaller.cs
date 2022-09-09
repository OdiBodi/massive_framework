namespace MassiveCore.Framework
{
    public class TimersInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Timers>().AsSingle();
        }
    }
}
