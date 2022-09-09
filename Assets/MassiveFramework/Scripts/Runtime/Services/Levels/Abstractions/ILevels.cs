using System;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface ILevels
    {
        public event Action<Level> OnLevelLoaded;

        public Level CurrentLevel { get; }

        public UniTask LoadCurrentLevel();
        public UniTask LoadNextLevel();
        public void DestroyCurrentLevel();
    }
}
