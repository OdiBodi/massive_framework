using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class VisualEffect : PoolObject, IVisualEffect
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        private void OnDisable()
        {
            Stop();
        }

        public async UniTask Play()
        {
            if (_particleSystem.isPlaying)
            {
                return;
            }
            _particleSystem.Play();
            _logger.Print($"Visual effect \"{Id}\" play!");
            await Observable.EveryUpdate().TakeWhile(_ => _particleSystem.isPlaying && this.Activity());
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

        private void Reset()
        {
            _particleSystem.Stop();
            _logger.Print($"Visual effect \"{Id}\" stop!");
        }
    }
}
