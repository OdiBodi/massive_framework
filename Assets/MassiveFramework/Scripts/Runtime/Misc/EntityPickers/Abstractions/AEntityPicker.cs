using System;
using System.Collections.Generic;
using Lean.Touch;
using UniRx;
using UnityEngine;

namespace MassiveCore.Framework.Runtime.Misc.EntityPicker
{
    public abstract class AEntityPicker<T> : IDisposable
        where T : BaseMonoBehaviour
    {
        private bool _active;

        private readonly PickType _pickType;

        protected readonly Camera _camera;
        protected readonly float _maxDistance;
        protected readonly int _layerMask;

        protected readonly HashSet<T> _disabledEntities = new();

        public event Action<T> Picked;
        public event Action Missed;

        protected AEntityPicker(Camera camera, PickType pickType, float maxDistance, int layerMask)
        {
            _camera = camera;
            _pickType = pickType;
            _maxDistance = maxDistance;
            _layerMask = layerMask;
            Active.Subscribe(OnActiveChanged);
        }

        public ReactiveProperty<bool> Active { get; } = new();

        public void Dispose()
        {
            UnsubscribeFromLeanTouch();
        }

        private void SubscribeOnLeanTouch()
        {
            switch (_pickType)
            {
                case PickType.FingerDown:
                    LeanTouch.OnFingerDown += OnFingerDown;
                    break;
                case PickType.FingerMove:
                    LeanTouch.OnFingerUpdate += OnFingerUpdate;
                    break;
                case PickType.FingerUp:
                    LeanTouch.OnFingerUp += OnFingerUp;
                    break;
                case PickType.FingerTap:
                    LeanTouch.OnFingerTap += OnFingerTap;
                    break;
            }
        }

        private void UnsubscribeFromLeanTouch()
        {
            switch (_pickType)
            {
                case PickType.FingerDown:
                    LeanTouch.OnFingerDown -= OnFingerDown;
                    break;
                case PickType.FingerMove:
                    LeanTouch.OnFingerUpdate -= OnFingerUpdate;
                    break;
                case PickType.FingerUp:
                    LeanTouch.OnFingerUp -= OnFingerUp;
                    break;
                case PickType.FingerTap:
                    LeanTouch.OnFingerTap -= OnFingerTap;
                    break;
            }
        }

        public void EnableEntity(T entity)
        {
            _disabledEntities.Remove(entity);
        }

        public void DisableEntity(T entity)
        {
            _disabledEntities.Add(entity);
        }

        protected abstract void Handle(LeanFinger finger);

        protected void InvokePicked(T entity)
        {
            Picked?.Invoke(entity);
        }

        protected void InvokeMissed()
        {
            Missed?.Invoke();
        }

        private void OnActiveChanged(bool value)
        {
            if (value)
            {
                SubscribeOnLeanTouch();
            }
            else
            {
                UnsubscribeFromLeanTouch();
            }
        }

        private void OnFingerDown(LeanFinger finger)
        {
            Handle(finger);
        }

        private void OnFingerUpdate(LeanFinger finger)
        {
#if UNITY_EDITOR
            if (finger.Index == LeanTouch.MOUSE_FINGER_INDEX)
#endif
            {
                Handle(finger);
            }
        }

        private void OnFingerUp(LeanFinger finger)
        {
            Handle(finger);
        }
        
        private void OnFingerTap(LeanFinger finger)
        {
            Handle(finger);
        }
    }
}
