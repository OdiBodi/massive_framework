using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [CreateAssetMenu(fileName = "environments_config", menuName = "Massive Framework/Configs/Environments Config")]
    public class EnvironmentsConfig : Config
    {
        [SerializeField]
        private EnvironmentConfig[] _configs;

        public EnvironmentConfig[] Configs => _configs;
    }
}
