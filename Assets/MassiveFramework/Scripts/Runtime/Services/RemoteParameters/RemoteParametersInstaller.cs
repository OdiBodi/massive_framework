using Zenject;

namespace MassiveCore.Framework
{
    public class RemoteParametersInstaller : ServiceInstaller
    {
        [Inject]
        private readonly IConfigs _configs;

        private IRemoteParameters RemoteParameters => _configs.Config<FirebaseRemoteParameters>();

        public override void InstallBindings()
        {
            Container.Bind<IRemoteParameters>().FromInstance(RemoteParameters).AsSingle();
        }
    }
}
