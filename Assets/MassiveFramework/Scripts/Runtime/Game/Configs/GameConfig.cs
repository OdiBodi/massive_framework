using System.Linq;
using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "game_config", menuName = "Massive Framework/Configs/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField]
        private Config[] configs;

        public T Config<T>() where T : Config
        {
            return (T)configs.First(x => x.GetType() == typeof(T));
        }
    }
}
