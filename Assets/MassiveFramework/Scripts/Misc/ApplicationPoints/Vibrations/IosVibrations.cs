using Lofelt.NiceVibrations;

namespace MassiveCore.Framework
{
    public class IosVibrations : IVibrations
    {
        private readonly WaitingList<string> waitingList = new WaitingList<string>(8);

        public void ButtonClick()
        {
            Vibrate("button_click", HapticPatterns.PresetType.Selection);
        }

        private void Vibrate(string name, HapticPatterns.PresetType preset, float delay = 0.1f)
        {
            if (waitingList.Add(name, delay))
            {
                HapticPatterns.PlayPreset(preset);
            }
        }
    }
}
