using System;
using UniRx;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class Inertia : IDisposable
    {
        private readonly float _dumping;
        private readonly float _stopThreshold;
        private readonly float _maxSpeed;

        private readonly ReactiveProperty<Vector2> _velocity = new ();
        private IDisposable _stream;

        public event Action Broke;
        public event Action Stopped;

        public Inertia(float dumping, float stopThreshold, float maxSpeed)
        {
            _dumping = dumping;
            _stopThreshold = stopThreshold;
            _maxSpeed = maxSpeed;
            Velocity = _velocity.ToReadOnlyReactiveProperty();
        }

        public bool Active => _stream != null;
        public ReadOnlyReactiveProperty<Vector2> Velocity { get; private set; }

        public void Dispose()
        {
            Stop();
        }

        public void Start(Vector2 velocity)
        {
            if (Active)
            {
                return;
            }
            _velocity.Value = velocity;
            _stream = Observable.EveryUpdate().TakeWhile(_ => _velocity.Value.magnitude > _stopThreshold).Subscribe(_ =>
            {
                var velocity = _velocity.Value; 
                velocity -= velocity * _dumping * Time.deltaTime;
                velocity = Vector2.ClampMagnitude(velocity, _maxSpeed);
                _velocity.Value = velocity;
            },
            () =>
            {
                Stopped?.Invoke();
                _stream = null;
            });
        }

        public void Reset()
        {
            _velocity.Value = Vector2.zero;
        }

        public void Stop()
        {
            Broke?.Invoke();
            _stream?.Dispose();
            _stream = null;
        }
    }
}
