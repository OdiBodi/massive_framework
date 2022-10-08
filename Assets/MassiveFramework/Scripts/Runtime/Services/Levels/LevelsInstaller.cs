using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class LevelsInstaller : ServiceInstaller
    {
        [Inject]
        private readonly IConfigs _configs;

        [SerializeField]
        private Transform _root;

        public override void InstallBindings()
        {
            Container.Bind<ILevels>().To<Levels>().AsSingle();
            Container.BindFactory<int, Level, Level.Factory>().FromMethod
            (
                (c, i) =>
                {
                    var configs = _configs.Config<LevelsConfig>().Configs;
                    var index = i % configs.Length;
                    var prefab = configs[index].Prefab;
                    var level = c.InstantiatePrefabForComponent<Level>(prefab, _root);
                    level.name = prefab.name;
                    return level;
                }
            );
        }
    }
}
