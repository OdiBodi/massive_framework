using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class EnvironmentInstaller : ServiceInstaller
    {
        [SerializeField]
        private Light _directionalLight;

        public override void InstallBindings()
        {
            Container.Bind<IEnvironment>().To<Environment>().AsSingle().WithArguments(_directionalLight);
        }
    }
}
