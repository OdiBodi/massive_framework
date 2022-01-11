using Zenject;

namespace MassiveCore.Framework
{
    public class VibrationsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
#if UNITY_EDITOR
            Container.Bind<IVibrations>().To<EditorVibrations>().AsSingle();
#elif UNITY_IOS
            Container.Bind<IVibrations>().To<IosVibrations>().AsSingle();
#elif UNITY_ANDROID
            Container.Bind<IVibrations>().To<AndroidVibrations>().AsSingle();
#endif
        }
    }
}
