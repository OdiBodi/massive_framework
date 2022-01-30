using System;
using System.Threading.Tasks;
using UnityEngine;

namespace MassiveCore.Framework
{
    public static class VfxExtensions
    {
        public static Task PlayVfx(this Pool pool, string id, Vector3 position, Quaternion rotation, Vector3 scale,
            Action<Vfx> onPrepare = null)
        {
            var vfx = pool.Get<Vfx>(id);
            vfx.CacheTransform.position = position;
            vfx.CacheTransform.rotation = rotation;
            vfx.CacheTransform.localScale = scale;
            onPrepare?.Invoke(vfx);
            return vfx.Play();
        }

        public static Task PlayVfx(this Pool pool, string id, Camera camera, Vector3 position, Quaternion rotation,
            Vector3 scale, Action<Vfx> onPrepare = null)
        {
            var worldPoint = camera.ViewportToWorldPoint(position);
            return pool.PlayVfx(id, worldPoint, rotation, scale, onPrepare);
        }
    }
}
