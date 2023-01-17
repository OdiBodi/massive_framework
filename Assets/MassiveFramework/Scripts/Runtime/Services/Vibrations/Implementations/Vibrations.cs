using System.Collections.Generic;
using System.Linq;
using Lofelt.NiceVibrations;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class Vibrations : IVibrations
    {
        [Inject]
        private readonly ILogger _logger;

        [Inject]
        private readonly IProfile _profile;

        [Inject]
        private readonly IConfigs _configs;

        private readonly WaitingList<string> _waitingList = new(8);

        private IEnumerable<VibrationConfig> Configs => _configs.Config<VibrationsConfig>().Configs;
        private bool Enabled => _profile.Property<bool>(ProfileIds.VibrationsEnabled).Value;

        public void Vibrate(string id)
        {
            var config = Configs.FirstOrDefault(config => config.Id == id);
            if (!config)
            {
                _logger.Print($"Vibration \"{id}\" config is not found!");
                return;
            }
            if (!Enabled)
            {
                _logger.Print($"Vibration \"{id}\" is not available by enable!");
                return;
            }
            if (config.CooldownTime > 0f && !_waitingList.Add(id, config.CooldownTime))
            {
                _logger.Print($"Vibration \"{id}\" is not available by cooldown time!");
                return;
            }
            HapticPatterns.PlayPreset(config.Preset);
            _logger.Print($"Vibration \"{id}\" play!");
        }
    }
}
