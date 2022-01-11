using Lofelt.NiceVibrations;
using Zenject;

namespace MassiveCore.Framework
{
    public class EditorVibrations : IVibrations
    {
        [Inject]
        private readonly ILogger logger;

        private readonly WaitingList<string> waitingList = new WaitingList<string>(4);

        public void ButtonClick()
        {
            Vibrate("button_click", HapticPatterns.PresetType.Selection);
        }

        private void Vibrate(string name, HapticPatterns.PresetType preset, float delay = 0.1f)
        {
            if (waitingList.Add(name, delay))
            {
                logger.Print($"EditorVibrations:Vibrate({name},{preset})");
            }
        }
    }
}
