using System.Linq;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class Environment
    {
        [Inject]
        private readonly GameConfig gameConfig;

        private readonly Light directionalLight;

        public Environment(Light directionalLight)
        {
            this.directionalLight = directionalLight;
        }

        public void ApplyConfig(string name)
        {
            var config = ConfigBy(name);
            config.DirectionalLightConfig.ApplyTo(directionalLight);
            config.AmbientLightingConfig.Apply();
        }

        private EnvironmentConfig ConfigBy(string name)
        {
            return gameConfig.EnvironmentsConfig.Configs.First(config => config.name == name);
        }
    }
}
