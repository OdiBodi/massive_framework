using System.Linq;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class Environment : IEnvironment
    {
        [Inject]
        private readonly IConfigs _configs;

        private readonly Light _directionalLight;

        public Environment(Light directionalLight)
        {
            _directionalLight = directionalLight;
        }

        public EnvironmentConfig ConfigBy(string name)
        {
            var environmentsConfig = _configs.Config<EnvironmentsConfig>();
            var environmentConfig = environmentsConfig.Configs.First(config => config.name == name);
            return environmentConfig;
        }

        public void ApplyConfig(string name)
        {
            var config = ConfigBy(name);
            config.DirectionalLightParams.ApplyTo(_directionalLight);
            config.AmbientLightingParams.Apply();
        }
    }
}
