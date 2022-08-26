namespace MassiveCore.Framework
{
    public class LevelIndex
    {
        private readonly ICustomProfile profile;
        private readonly LevelsConfig levelsConfig;

        public LevelIndex(ICustomProfile profile, LevelsConfig levelsConfig)
        {
            this.profile = profile;
            this.levelsConfig = levelsConfig;
        }

        public int Current()
        {
            return profile.LevelIndex.Value;
        }

        public void UpdateToNext()
        {
            profile.LevelIndex.Value++;
            profile.LevelIndex.Value %= levelsConfig.Configs.Length;
        }
    }
}
