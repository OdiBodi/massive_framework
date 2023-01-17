namespace MassiveCore.Framework.Runtime
{
    public class VibrationsInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
#if UNITY_EDITOR
            Container.Bind<IVibrations>().To<EditorVibrations>().AsSingle();
#else
            Container.Bind<IVibrations>().To<Vibrations>().AsSingle();
#endif
        }
    }
}
