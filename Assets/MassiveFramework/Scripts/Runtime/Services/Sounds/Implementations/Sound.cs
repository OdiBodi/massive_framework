using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class Sound : PoolObject, ISound
    {
        [SerializeField]
        private AudioSource _audioSource;

        private float _initialVolume;
        private float _initialPitch;

        public string Id => name;

        private void Awake()
        {
            InitializeInitialAudioSourceValues();
        }

        private void OnDisable()
        {
            Stop();
        }

        public async UniTask Play(float volumeScale = 1f, float pitchScale = 1f)
        {
            if (_audioSource.isPlaying)
            {
                return;
            }
            _audioSource.volume = _initialVolume * volumeScale;
            _audioSource.pitch = _initialPitch * pitchScale;
            _audioSource.Play();
            _logger.Print($"Sound \"{Id}\" play!");
            await Observable.EveryUpdate().TakeWhile(_ => _audioSource.isPlaying && this.Activity());
            Return();
        }

        public void Stop()
        {
            Return();
        }

        public override void Return()
        {
            Reset();
            base.Return();
        }

        public override void Remove()
        {
            Reset();
            base.Remove();
        }

        private void InitializeInitialAudioSourceValues()
        {
            _initialVolume = _audioSource.volume;
            _initialPitch = _audioSource.pitch;
        }
        
        private void Reset()
        {
            if (!_audioSource.isPlaying)
            {
                return;
            }
            _audioSource.Stop();
            _logger.Print($"Sound \"{Id}\" stop!");
        }
    }
}
