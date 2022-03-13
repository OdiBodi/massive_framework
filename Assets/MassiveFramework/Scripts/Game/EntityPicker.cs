using System;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

namespace MassiveCore.Framework
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

        private readonly Camera camera;
        private readonly PickType pickType;
        private readonly float maxDistance;
        private readonly int layerMask;

        private readonly HashSet<T> disabled = new HashSet<T>();

        private bool active;

        public event Action<T> OnPicked;
        public event Action OnMissed;

        public EntityPicker(Camera camera, PickType pickType, float maxDistance, int layerMask)
        {
            this.camera = camera;
            this.pickType = pickType;
            this.maxDistance = maxDistance;
            this.layerMask = layerMask;
        }

        public bool Active
        {
            get => active;
            set
            {
                if (active != value)
                {
                    active = value;
                    if (value)
                    {
                        SubscribeOnLeanTouch();
                    }
                    else
                    {
                        UnsubscribeFromLeanTouch();
                    }
                }
            }
        }

        public void Dispose()
        {
            Active = false;
        }

        private void SubscribeOnLeanTouch()
        {
            switch (pickType)
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
            switch (pickType)
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
            disabled.Remove(entity);
        }

        public void DisableEntity(T entity)
        {
            disabled.Add(entity);
        }

        private void Handle(LeanFinger finger)
        {
            if (finger.IsOverGui)
            {
                return;
            }

            var ray = finger.GetRay(camera);
            if (!Physics.Raycast(ray, out var hitInfo, maxDistance, layerMask))
            {
                OnMissed?.Invoke();
                return;
            }

            var entity = hitInfo.transform.GetComponent<T>();
            if (entity && !disabled.Contains(entity))
            {
                OnPicked?.Invoke(entity);
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
