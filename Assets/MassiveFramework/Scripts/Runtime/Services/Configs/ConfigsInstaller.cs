using UnityEngine;

namespace MassiveCore.Framework
{
    public class ConfigsInstaller : ServiceInstaller
    {
        [SerializeField]
        private Configs _configs;

        public override void InstallBindings()
        {
            Container.Bind<IConfigs>().FromInstance(_configs).AsSingle();
        }
    }
}
