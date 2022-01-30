using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class Vfx : BaseMonoBehaviour
    {
        public class Factory : PlaceholderFactory<string, Vfx>
        {
        }

        [SerializeField]
        private ParticleSystem vfx;

        private IDisposable stream;

        private void OnDisable()
        {
            Stop();
        }

        public async Task Play()
        {
            if (stream != null)
            {
                return;
            }
            if (!vfx.main.loop)
            {
                var observable = Observable.Timer(TimeSpan.FromSeconds(vfx.main.duration));
                stream = observable.Subscribe(_ => stream = null).AddTo(this);
                await observable;
                Reset();
            }
            vfx.Play();
        }

        public void Stop()
        {
            Reset();
        }

        private void Reset()
        {
            vfx.Stop();
            CacheGameObject.SetActive(false);
            stream?.Dispose();
            stream = null;
        }
    }
}
