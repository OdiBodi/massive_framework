using System;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.Linq;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class Levels : ILevels
    {
        [Inject]
        private readonly IProfile _profile;

        [Inject]
        private readonly IConfigs _configs;

        [Inject]
        private readonly LevelFactory _levelsFactory;

        private readonly Transform _root;

        public event Action<ILevel> LevelLoaded;

        public Levels(Transform root)
        {
            _root = root;
        }

        private LevelsConfig LevelsConfig => _configs.Config<LevelsConfig>();
        public ILevel CurrentLevel { get; private set; }

        public UniTask LoadCurrentLevel()
        {
            var levelIndex = new LevelIndex(_profile, LevelsConfig);
            var index = levelIndex.Current();
            return LoadLevel(index);
        }

        public UniTask LoadNextLevel()
        {
            var levelIndex = new LevelIndex(_profile, LevelsConfig);
            levelIndex.UpdateToNext();
            var index = levelIndex.Current();
            return LoadLevel(index);
        }

        public void DestroyCurrentLevel()
        {
            if (CurrentLevel == null)
            {
                return;
            }
            (CurrentLevel as BaseMonoBehaviour).gameObject.Destroy();
            CurrentLevel = null;
        }

        private async UniTask LoadLevel(int index)
        {
            DestroyCurrentLevel();
            await Observable.NextFrame();
            CurrentLevel = _levelsFactory.Create(index, _root);
            await CurrentLevel.Loaded;
            LevelLoaded?.Invoke(CurrentLevel);
        }
    }
}
