using Lean.Touch;
using UnityEngine;

namespace MassiveCore.Framework.Runtime.Misc.EntityPicker
{
    public class EntityPicker3d<T> : AEntityPicker<T>
        where T : BaseMonoBehaviour
    {
        public EntityPicker3d(Camera camera, PickType pickType, float maxDistance, int layerMask)
            : base(camera, pickType, maxDistance, layerMask)
        {
        }

        protected override void Handle(LeanFinger finger)
        { 
            if (finger.IsOverGui)
            {
                return;
            }

            var ray = finger.GetRay(_camera);
            if (!Physics.Raycast(ray, out var hitInfo, _maxDistance, _layerMask))
            {
                InvokeMissed();
                return;
            }
     
            var entity = hitInfo.transform.GetComponent<T>();
            if (entity && !_disabledEntities.Contains(entity))
            {
                InvokePicked(entity);
            }
        }
    }
}
