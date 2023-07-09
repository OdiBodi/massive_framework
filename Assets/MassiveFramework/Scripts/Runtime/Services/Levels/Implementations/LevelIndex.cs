using UniRx;

namespace MassiveCore.Framework.Runtime
{
    public class LevelIndex : ILevelIndex
    {
        private readonly IProfile _profile;
        private readonly LevelsConfig _levelsConfig;

        public LevelIndex(IProfile profile, LevelsConfig levelsConfig)
        {
            _profile = profile;
            _levelsConfig = levelsConfig;
        }

        private ReactiveProperty<int> CurrentLevelIndex => _profile.Property<int>(ProfileIds.LevelIndex);

        public int Current()
        {
            return CurrentLevelIndex.Value;
        }

        public void UpdateToNext()
        {
            CurrentLevelIndex.Value++;
            CurrentLevelIndex.Value %= _levelsConfig.Configs.Length;
        }
    }
}
