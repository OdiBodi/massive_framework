using UniRx;

namespace MassiveCore.Framework.Runtime
{
    public class LevelIndex
    {
        private readonly IProfile _profile;
        private readonly LevelsConfig _levelsConfig;

        public LevelIndex(IProfile profile, LevelsConfig levelsConfig)
        {
            _profile = profile;
            _levelsConfig = levelsConfig;
        }

        private ReactiveProperty<int> CurrentLevelIndexProperty => _profile.Property<int>(ProfileIds.LevelIndex);

        public int Current()
        {
            return CurrentLevelIndexProperty.Value;
        }

        public void UpdateToNext()
        {
            CurrentLevelIndexProperty.Value++;
            CurrentLevelIndexProperty.Value %= _levelsConfig.Configs.Length;
        }
    }
}
