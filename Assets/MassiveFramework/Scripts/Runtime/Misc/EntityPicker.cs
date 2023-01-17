using System;
using System.Collections.Generic;
using Lean.Touch;
using UniRx;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class EntityPicker<T> : IDisposable
        where T : BaseMonoBehaviour
    {
        public enum PickType
        {
            FingerDown,
            FingerMove,
            FingerUp,
            FingerTap
        }

        private readonly Camera _camera;
        private readonly PickType _pickType;
        private readonly float _maxDistance;
        private readonly int _layerMask;

        private readonly HashSet<T> _disabledEntities = new();

        private bool _active;

        public event Action<T> Picked;
        public event Action Missed;

        public EntityPicker(Camera camera, PickType pickType, float maxDistance, int layerMask)
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

        private void Handle(LeanFinger finger)
        {
            if (finger.IsOverGui)
            {
                return;
            }

            var ray = finger.GetRay(_camera);
            if (!Physics.Raycast(ray, out var hitInfo, _maxDistance, _layerMask))
            {
                Missed?.Invoke();
                return;
            }

            var entity = hitInfo.transform.GetComponent<T>();
            if (entity && !_disabledEntities.Contains(entity))
            {
                Picked?.Invoke(entity);
            }
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
