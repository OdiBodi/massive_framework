using Lofelt.NiceVibrations;
using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "vibration_config", menuName = "Massive Framework/Configs/Vibration Config")]
    public class VibrationConfig : ScriptableObject
    {
        [SerializeField]
        private string id;

        [SerializeField]
        private HapticPatterns.PresetType preset;

        [SerializeField]
        private float cooldownTime;

        public string Id => id;
        public HapticPatterns.PresetType Preset => preset;
        public float CooldownTime => cooldownTime;
    }
}
