using UniRx;

namespace MassiveCore.Framework
{
    public class LevelIndex
    {
        private readonly IProfile profile;
        private readonly LevelsConfig levelsConfig;

        public LevelIndex(IProfile profile, LevelsConfig levelsConfig)
        {
            this.profile = profile;
            this.levelsConfig = levelsConfig;
        }

        private ReactiveProperty<int> CurrentLevelIndex => profile.Property<int>(ProfileIds.LevelIndex);

        public int Current()
        {
            return CurrentLevelIndex.Value;
        }

        public void UpdateToNext()
        {
            CurrentLevelIndex.Value++;
            CurrentLevelIndex.Value %= levelsConfig.Configs.Length;
        }
    }
}
