using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "environments_config", menuName = "Massive Framework/Configs/Environments Config")]
    public class EnvironmentsConfig : ScriptableObject
    {
        [SerializeField]
        private EnvironmentConfig[] configs;

        public EnvironmentConfig[] Configs => configs;
    }
}
