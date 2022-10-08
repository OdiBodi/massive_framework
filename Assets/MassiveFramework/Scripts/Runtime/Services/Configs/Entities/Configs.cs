using System.Linq;
using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "configs", menuName = "Massive Framework/Configs/Configs")]
    public class Configs : ScriptableObject, IConfigs
    {
        [SerializeField]
        private Config[] configs;

        public T Config<T>() where T : Config
        {
            return (T)configs.First(x => x.GetType() == typeof(T));
        }
    }
}
