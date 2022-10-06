using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class VisualEffect : PoolObject, IVisualEffect
    {
        public class Factory : PlaceholderFactory<string, VisualEffect>
        {
        }

        [SerializeField]
        private ParticleSystem _particleSystem;

        public override string Id => name;

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
        }
    }
}
