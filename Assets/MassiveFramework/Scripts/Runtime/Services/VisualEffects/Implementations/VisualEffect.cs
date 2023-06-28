using Cysharp.Threading.Tasks;
using MassiveCore.Framework.Runtime.Patterns;
using UniRx;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class VisualEffect : PoolObject, IVisualEffect, IPoolObject
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        private void OnDisable()
        {
            Stop();
        }

        public UniTask Play()
        {
            if (_particleSystem.isPlaying)
            {
                return UniTask.CompletedTask;
            }

            _particleSystem.Play();

            _logger.Print($"Visual effect \"{Id}\" play!");

            var task = Observable.EveryUpdate().TakeWhile(_ => _particleSystem.isPlaying).ToUniTask();
            return task;
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

        private void Reset()
        {
            _particleSystem.Stop();
            _logger.Print($"Visual effect \"{Id}\" stop!");
        }
    }
}
