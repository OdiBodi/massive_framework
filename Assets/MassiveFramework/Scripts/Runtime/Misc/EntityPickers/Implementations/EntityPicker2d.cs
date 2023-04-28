using Lean.Touch;
using UnityEngine;

namespace MassiveCore.Framework.Runtime.Misc.EntityPicker
{
    public class EntityPicker2d<T> : AEntityPicker<T>
        where T : BaseMonoBehaviour
    {
        public EntityPicker2d(Camera camera, PickType pickType, float maxDistance, int layerMask)
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
            var hit = Physics2D.GetRayIntersection(ray, _maxDistance, _layerMask);
            if (!hit.transform)
            {
                InvokeMissed();
                return;
            }

            var entity = hit.transform.GetComponent<T>();
            if (entity && !_disabledEntities.Contains(entity))
            {
                InvokePicked(entity);
            }
        }
    }
}
