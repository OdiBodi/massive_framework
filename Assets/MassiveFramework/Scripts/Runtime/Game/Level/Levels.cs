using System;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.Linq;
using Zenject;

namespace MassiveCore.Framework
{
    public class Levels
    {
        [Inject]
        private readonly IProfile profile;

        [Inject]
        private readonly GameConfig gameConfig;

        [Inject]
        private readonly Level.Factory levelsFactory;

        public event Action<Level> OnLevelLoaded;

        public Level CurrentLevel { get; private set; }

        public UniTask LoadCurrentLevel()
        {
            var levelIndex = new LevelIndex(profile, gameConfig.Config<LevelsConfig>());
            var index = levelIndex.Current();
            return LoadLevel(index);
        }

        public UniTask LoadNextLevel()
        {
            var levelIndex = new LevelIndex(profile, gameConfig.Config<LevelsConfig>());
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
            CurrentLevel = levelsFactory.Create(index);
            SubscribeOnCurrentLevel();
        }

        private void SubscribeOnCurrentLevel()
        {
            CurrentLevel.OnLoaded += () => OnLevelLoaded?.Invoke(CurrentLevel);
        }
    }
}
