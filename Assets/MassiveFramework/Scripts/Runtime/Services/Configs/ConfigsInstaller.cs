using UnityEngine;

namespace MassiveCore.Framework
{
    public class ConfigsInstaller : ServiceInstaller
    {
        [SerializeField]
        private Configs configs;

        public override void InstallBindings()
        {
            Container.Bind<IConfigs>().FromInstance(configs).AsSingle();
        }
    }
}
