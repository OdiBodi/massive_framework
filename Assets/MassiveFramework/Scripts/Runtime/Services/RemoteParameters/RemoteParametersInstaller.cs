using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class RemoteParametersInstaller : ServiceInstaller
    {
        [Inject]
        private readonly IConfigs _configs;

#if UNITY_IOS || UNITY_ANDROID
        private IRemoteParameters RemoteParameters => _configs.Config<FirebaseRemoteParameters>();
#else
        private IRemoteParameters RemoteParameters => _configs.Config<StaticRemoteParameters>();
#endif

        public override void InstallBindings()
        {
            Container.Bind<IRemoteParameters>().FromInstance(RemoteParameters).AsSingle();
        }
    }
}
