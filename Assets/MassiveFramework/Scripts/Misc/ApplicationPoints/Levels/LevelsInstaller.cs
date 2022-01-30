using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class LevelsInstaller : MonoInstaller
    {
        [Inject]
        private readonly GameConfig gameConfig;

        [SerializeField]
        private Transform root;

        public override void InstallBindings()
        {
            Container.Bind<Levels>().ToSelf().AsSingle();
            Container.BindFactory<int, Level, Level.Factory>().FromMethod
            (
                (c, i) =>
                {
                    var configs = gameConfig.LevelsConfig.Configs;
                    var index = i % configs.Length;
                    var prefab = configs[index].Prefab;
                    var level = c.InstantiatePrefabForComponent<Level>(prefab, root);
                    level.name = prefab.name;
                    return level;
                }
            );
        }
    }
}
