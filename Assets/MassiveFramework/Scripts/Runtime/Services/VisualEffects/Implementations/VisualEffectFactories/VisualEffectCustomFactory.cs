using System.Linq;
using Zenject;

namespace MassiveCore.Framework
{
    public class VisualEffectCustomFactory : IFactory<string, VisualEffect>
    {
        private readonly DiContainer _diContainer;
        private readonly IConfigs _configs;

        public VisualEffectCustomFactory(DiContainer diContainer, IConfigs configs)
        {
            _diContainer = diContainer;
            _configs = configs;
        }

        public VisualEffect Create(string id)
        {
            var configs = _configs.Config<VisualEffectsConfig>().Configs;
            var prefab = configs.First(x => x.Id == id).VisualEffect;
            var visualEffect = _diContainer.InstantiatePrefabForComponent<VisualEffect>(prefab);
            visualEffect.name = id;
            return visualEffect;
        }
    }
}
