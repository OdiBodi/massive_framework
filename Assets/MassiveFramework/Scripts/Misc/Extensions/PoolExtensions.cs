using System;
using System.Threading.Tasks;
using UnityEngine;

namespace MassiveCore.Framework
{
    public static class VfxExtensions
    {
        public static T Get<T>(this Pool pool, string id, Vector3 position, Quaternion rotation, Vector3 scale,
            Action<T> onPrepare = null) where T : BaseMonoBehaviour
        {
            var obj = pool.Get<T>(id);
            obj.CacheTransform.position = position;
            obj.CacheTransform.rotation = rotation;
            obj.CacheTransform.localScale = scale;
            onPrepare?.Invoke(obj);
            return obj;
        }

        public static T Get<T>(this Pool pool, string id, Camera camera, Vector3 position, Quaternion rotation,
            Vector3 scale, Action<T> onPrepare = null) where T : BaseMonoBehaviour
        {
            var worldPoint = camera.ViewportToWorldPoint(position);
            return pool.Get(id, worldPoint, rotation, scale, onPrepare);
        }

        public static Task PlayVfx(this Pool pool, string id, Vector3 position, Quaternion rotation, Vector3 scale,
            Action<Vfx> onPrepare = null)
        {
            var vfx = pool.Get(id, position, rotation, scale, onPrepare);
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
