using System.Linq;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class SoundCustomFactory : IFactory<string, Sound>
    {
        private readonly DiContainer _diContainer;
        private readonly IConfigs _configs;

        public SoundCustomFactory(DiContainer diContainer, IConfigs configs)
        {
            _diContainer = diContainer;
            _configs = configs;
        }

        public Sound Create(string id)
        {
            var configs = _configs.Config<SoundsConfig>().Configs;
            var prefab = configs.First(x => x.Id == id).Sound;
            var sound = _diContainer.InstantiatePrefabForComponent<Sound>(prefab);
            sound.name = id;
            return sound;
        }
    }
}
