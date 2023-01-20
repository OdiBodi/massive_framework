using System.Linq;
using UnityEngine.LowLevel;

namespace MassiveCore.Framework.Runtime
{
    public class CustomPlayerLoop
    {
        private readonly PlayerLoopConfig _playerLoopConfig;

        public CustomPlayerLoop(PlayerLoopConfig playerLoopConfig)
        {
            _playerLoopConfig = playerLoopConfig;
        }

        public void Apply()
        {
            _playerLoopConfig.Phases.ForEach(ProcessPhase);
        }

        private void ProcessPhase(PlayerLoopPhase phase)
        {
            var phaseType = phase.Type;

            var currentLoop = PlayerLoop.GetCurrentPlayerLoop();
            var currentLoopSystems = currentLoop.subSystemList; 

            if (phase.enabled)
            {
                var currentLoopPhase = currentLoopSystems.First(phase => phase.type == phaseType);
                var currentLoopPhaseSystems = currentLoopPhase.subSystemList;
                var resultLoopPhaseSystems = currentLoopPhaseSystems.Where(currentSystem =>
                {
                    var phaseSystem = phase.systems.FirstOrDefault(phaseSystem => phaseSystem.Type == currentSystem.type);
                    if (!phaseSystem.Valid)
                    {
                        return true;
                    }
                    var result = phaseSystem.enabled;
                    return result;
                });
                currentLoopPhase.subSystemList = resultLoopPhaseSystems.ToArray();
            }
            else
            {
                currentLoop.subSystemList = currentLoopSystems.Where(x => x.type != phaseType).ToArray();
            }

            PlayerLoop.SetPlayerLoop(currentLoop);
        }
    }
}
