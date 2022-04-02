using System.Collections.Generic;
using System.Linq;
using Lofelt.NiceVibrations;
using Zenject;

namespace MassiveCore.Framework
{
    public class Vibrations : IVibrations
    {
        [Inject]
        private readonly GameConfig gameConfig;

        private readonly WaitingList<string> waitingList = new WaitingList<string>(8);

        private IEnumerable<VibrationConfig> Configs => gameConfig.VibrationsConfig.Configs;

        public void Vibrate(string id)
        {
            var config = Configs.FirstOrDefault(config => config.Id == id);
            if (!config)
            {
                return;
            }
            if (config.CooldownTime > 0f && !waitingList.Add(id, config.CooldownTime))
            {
                return;
            }
            HapticPatterns.PlayPreset(config.Preset);
        }
    }
}
