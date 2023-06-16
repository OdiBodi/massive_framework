using System;
using UniRx;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class AnimatedNumber : IDisposable
    {
        private readonly float _speed;
        private readonly ReactiveProperty<float> _number = new();

        private IDisposable _updateStream; 

        public AnimatedNumber(float speed)
        {
            _speed = speed;
        }

        public float TargetNumber { get; set; }
        public ReadOnlyReactiveProperty<float> Number { get; private set; }

        public void Initialize()
        {
            Number = _number.ToReadOnlyReactiveProperty();
            StartUpdateStream();
        }

        public void Dispose()
        {
            _updateStream?.Dispose();
            Number?.Dispose();
        }

        public void Reset(float number)
        {
            TargetNumber = number;
            _number.Value = number;
        }

        private void StartUpdateStream()
        {
            _updateStream = Observable.EveryUpdate().Subscribe(_ =>
            {
                _number.Value = _number.Value.EqualsTo(TargetNumber) ? TargetNumber :
                    Mathf.Lerp(_number.Value, TargetNumber, Time.deltaTime * _speed);
            });
        }
    }
}
