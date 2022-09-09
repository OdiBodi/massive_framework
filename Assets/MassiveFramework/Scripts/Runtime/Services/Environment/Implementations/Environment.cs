using System.Linq;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class Environment : IEnvironment
    {
        [Inject]
        private readonly IGameConfig gameConfig;

        private readonly Light directionalLight;

        public Environment(Light directionalLight)
        {
            this.directionalLight = directionalLight;
        }

        public EnvironmentConfig ConfigBy(string name)
        {
            var environmentsConfig = gameConfig.Config<EnvironmentsConfig>();
            var environmentConfig = environmentsConfig.Configs.First(config => config.name == name);
            return environmentConfig;
        }

        public void ApplyConfig(string name)
        {
            var config = ConfigBy(name);
            config.DirectionalLightParams.ApplyTo(directionalLight);
            config.AmbientLightingParams.Apply();
        }
    }
}
