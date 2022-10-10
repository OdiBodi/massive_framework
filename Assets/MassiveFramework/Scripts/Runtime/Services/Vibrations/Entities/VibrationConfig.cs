using Lofelt.NiceVibrations;
using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "vibration_config", menuName = "Massive Framework/Configs/Vibration Config")]
    public class VibrationConfig : ScriptableObject
    {
        [SerializeField]
        private string _id;

        [SerializeField]
        private HapticPatterns.PresetType _preset;

        [SerializeField]
        private float _cooldownTime;

        public string Id => _id;
        public HapticPatterns.PresetType Preset => _preset;
        public float CooldownTime => _cooldownTime;
    }
}
