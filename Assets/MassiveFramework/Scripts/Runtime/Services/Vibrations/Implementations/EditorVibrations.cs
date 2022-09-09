using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace MassiveCore.Framework
{
    public class EditorVibrations : IVibrations
    {
        [Inject]
        private readonly ILogger logger;

        [Inject]
        private readonly IGameConfig gameConfig;

        private readonly WaitingList<string> waitingList = new WaitingList<string>(8);

        private IEnumerable<VibrationConfig> Configs => gameConfig.Config<VibrationsConfig>().Configs;

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
            logger.Print($"EditorVibrations:Vibrate(\"{id}\")");
        }
    }
}
