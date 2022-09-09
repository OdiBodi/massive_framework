using UnityEngine;

namespace MassiveCore.Framework
{
    public class EnvironmentInstaller : ServiceInstaller
    {
        [SerializeField]
        private Light directionalLight;

        public override void InstallBindings()
        {
            Container.Bind<IEnvironment>().To<Environment>().AsSingle().WithArguments(directionalLight);
        }
    }
}
