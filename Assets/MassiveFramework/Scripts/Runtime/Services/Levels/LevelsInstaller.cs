using UnityEngine;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class LevelsInstaller : ServiceInstaller
    {
        [Inject]
        private IProfile _profile;

        [Inject]
        private IConfigs _configs;

        [SerializeField]
        private Transform _root;

        public override void InstallBindings()
        {
            var levelIndex = LevelIndex();
            Container.Bind<ILevels>().To<Levels>().AsSingle().WithArguments(levelIndex, _root);
            Container.BindFactory<int, Transform, Level, LevelFactory>().FromFactory<LevelCustomFactory>();
        }

        private LevelIndex LevelIndex()
        {
            var levelConfigs = _configs.Config<LevelsConfig>();
            var index = new LevelIndex(_profile, levelConfigs);
            return index;
        }
    }
}
