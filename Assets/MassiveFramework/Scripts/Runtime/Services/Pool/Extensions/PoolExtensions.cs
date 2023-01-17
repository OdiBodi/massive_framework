using System;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public static class PoolExtensions
    {
        public static T Request<T>(this IPool pool, string id, Vector3 position, Quaternion rotation, Vector3 scale,
            Action<T> onPrepare = null)
            where T : BaseMonoBehaviour, IPoolObject
        {
            var obj = pool.Request<T>(id);
            obj.CacheTransform.position = position;
            obj.CacheTransform.rotation = rotation;
            obj.CacheTransform.localScale = scale;
            onPrepare?.Invoke(obj);
            return obj;
        }

        public static T Request<T>(this IPool pool, string id, Camera camera, Vector3 position, Quaternion rotation,
            Vector3 scale, Action<T> onPrepare = null)
            where T : BaseMonoBehaviour, IPoolObject
        {
            var worldPoint = camera.ViewportToWorldPoint(position);
            return pool.Request(id, worldPoint, rotation, scale, onPrepare);
        }
    }
}
