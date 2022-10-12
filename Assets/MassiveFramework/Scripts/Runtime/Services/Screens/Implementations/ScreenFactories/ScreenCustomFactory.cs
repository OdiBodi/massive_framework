using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class ScreenCustomFactory : IFactory<Type, Transform, Screen>
    {
        private readonly DiContainer _diContainer;
        private readonly IConfigs _configs;

        public ScreenCustomFactory(DiContainer diContainer, IConfigs configs)
        {
            _diContainer = diContainer;
            _configs = configs;
        }

        public Screen Create(Type type, Transform root)
        {
            var configs = _configs.Config<ScreensConfig>().Configs;
            var prefab = configs.First(x => x.Prefab.GetType() == type).Prefab;
            var screen = _diContainer.InstantiatePrefabForComponent<Screen>(prefab);
            screen.CacheTransform.SetParent(root, false);
            screen.name = prefab.name;
            return screen;
        }
    }
}
