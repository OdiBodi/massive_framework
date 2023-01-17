using System;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework.Runtime
{
    public interface ILevels
    {
        event Action<ILevel> LevelLoaded;

        ILevel CurrentLevel { get; }

        UniTask LoadCurrentLevel();
        UniTask LoadNextLevel();
        void DestroyCurrentLevel();
    }
}
