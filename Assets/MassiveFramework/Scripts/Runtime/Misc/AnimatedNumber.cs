using System;
using UniRx;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class AnimatedNumber : IDisposable
    {
        private readonly float _speed;

        private IDisposable _updateStream; 

        public AnimatedNumber(float speed)
        {
            _speed = speed;
        }

        public float TargetNumber { get; set; }
        public FloatReactiveProperty Number { get; private set; }

        public void Initialize()
        {
            Number = new FloatReactiveProperty();
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
            Number.Value = number;
        }

        private void StartUpdateStream()
        {
            _updateStream = Observable.EveryUpdate().Subscribe(_ =>
            {
                Number.Value = Number.Value.EqualsTo(TargetNumber) ? TargetNumber :
                    Mathf.Lerp(Number.Value, TargetNumber, Time.deltaTime * _speed);
            });
        }
    }
}
