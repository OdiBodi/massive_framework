using System.Linq;
using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "configs", menuName = "Massive Framework/Configs/Configs")]
    public class Configs : ScriptableObject, IConfigs
    {
        [SerializeField]
        private Config[] _configs;

        public T Config<T>()
            where T : Config
        {
            return _configs.OfType<T>().First();
        }
    }
}
