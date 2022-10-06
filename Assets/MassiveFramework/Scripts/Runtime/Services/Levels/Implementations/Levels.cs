using System;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.Linq;
using Zenject;

namespace MassiveCore.Framework
{
    public class Levels : ILevels
    {
        [Inject]
        private readonly IProfile _profile;

        [Inject]
        private readonly IGameConfig _gameConfig;

        [Inject]
        private readonly Level.Factory _levelsFactory;

        public event Action<Level> OnLevelLoaded;

        private LevelsConfig LevelsConfig => _gameConfig.Config<LevelsConfig>();
        public Level CurrentLevel { get; private set; }

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
            CurrentLevel.gameObject.Destroy();
            CurrentLevel = null;
        }

        private async UniTask LoadLevel(int index)
        {
            DestroyCurrentLevel();
            await Observable.NextFrame();
            CurrentLevel = _levelsFactory.Create(index);
            SubscribeOnCurrentLevel();
        }

        private void SubscribeOnCurrentLevel()
        {
            CurrentLevel.OnLoaded += () => OnLevelLoaded?.Invoke(CurrentLevel);
        }
    }
}
