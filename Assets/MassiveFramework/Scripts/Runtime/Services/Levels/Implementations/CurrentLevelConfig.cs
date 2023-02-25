namespace MassiveCore.Framework.Runtime
{
    public class CurrentLevelConfig
    {
        private readonly IConfigs _configs;
        private readonly ILevelIndex _levelIndex;

        public CurrentLevelConfig(IConfigs configs, ILevelIndex levelIndex)
        {
            _configs = configs;
            _levelIndex = levelIndex;
        }

        public T Config<T>()
            where T : LevelConfig
        {
            var levelsConfig = _configs.Config<LevelsConfig>();
            var index = _levelIndex.Current();
            var config = levelsConfig.Configs[index] as T;
            return config;
        }
    }
}
