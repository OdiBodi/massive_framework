using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "resources_config", menuName = "Massive Framework/Configs/Resources Config")]
    public class ResourcesConfig : Config
    {
        [SerializeField]
        private ResourceConfig[] _configs;

        public ResourceConfig[] Configs => _configs;

        public IEnumerable<T> ConfigsBy<T>(string id)
            where T : ResourceConfig
        {
            var configs = _configs.OfType<T>().Where(config => config.Id == id);
            return configs;
        }
    }
}
