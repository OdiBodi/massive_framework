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
        private readonly LevelFactory _levelsFactory;

        private readonly ILevelIndex _levelIndex;
        private readonly Transform _root;

        public event Action<ILevel> LevelLoaded;

        public Levels(ILevelIndex levelIndex, Transform root)
        {
            _levelIndex = levelIndex;
            _root = root;
        }

        public ILevel CurrentLevel { get; private set; }

        public UniTask LoadCurrentLevel()
        {
            var index = _levelIndex.Current();
            return LoadLevel(index);
        }

        public UniTask LoadNextLevel()
        { 
            _levelIndex.UpdateToNext();
            var index = _levelIndex.Current();
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
