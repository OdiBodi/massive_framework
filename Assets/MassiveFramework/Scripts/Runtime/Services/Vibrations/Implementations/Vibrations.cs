using System.Collections.Generic;
using System.Linq;
using Lofelt.NiceVibrations;
using Zenject;

namespace MassiveCore.Framework
{
    public class Vibrations : IVibrations
    {
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
            HapticPatterns.PlayPreset(config.Preset);
        }
    }
}
