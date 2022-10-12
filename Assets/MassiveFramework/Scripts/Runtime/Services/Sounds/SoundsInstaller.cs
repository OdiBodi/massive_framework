namespace MassiveCore.Framework
{
    public class SoundsInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISounds>().To<Sounds>().AsSingle();
            Container.BindFactory<string, Sound, SoundFactory>().FromFactory<SoundCustomFactory>();
        }
    }
}
