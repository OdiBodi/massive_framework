using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace MassiveCore.Framework
{
    public class EditorVibrations : IVibrations
    {
        [Inject]
        private readonly ILogger _logger;

        [Inject]
        private readonly IGameConfig _gameConfig;

        private readonly WaitingList<string> _waitingList = new(8);

        private IEnumerable<VibrationConfig> Configs => _gameConfig.Config<VibrationsConfig>().Configs;

        public void Vibrate(string id)
        {
            var config = Configs.FirstOrDefault(config => config.Id == id);
            if (!config)
            {
                return;
            }
            if (config.CooldownTime > 0f && !_waitingList.Add(id, config.CooldownTime))
            {
                return;
            }
            _logger.Print($"EditorVibrations:Vibrate(\"{id}\")");
        }
    }
}
