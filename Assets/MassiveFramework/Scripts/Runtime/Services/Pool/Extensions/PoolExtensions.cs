using System;
using MassiveCore.Framework.Runtime.Patterns;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public static class PoolExtensions
    {
        public static T Request<T>(this IPool pool, string id, Vector3 position, Quaternion rotation, Vector3 scale,
            Action<T> prepare = null)
            where T : BaseMonoBehaviour, IPoolObject
        {
            var obj = pool.Request<T>(id);
            obj.CacheTransform.position = position;
            obj.CacheTransform.rotation = rotation;
            obj.CacheTransform.localScale = scale;
            prepare?.Invoke(obj);
            return obj;
        }

        public static T Request<T>(this IPool pool, string id, Camera camera, Vector3 position, Quaternion rotation,
            Vector3 scale, Action<T> prepare = null)
            where T : BaseMonoBehaviour, IPoolObject
        {
            var worldPoint = camera.ViewportToWorldPoint(position);
            return pool.Request(id, worldPoint, rotation, scale, prepare);
        }

        public static void BindObjectPool<T>(this Pool pool, IAbstractFactory<IPoolObject> objectFactory,
            IAbstractFactoryArguments objectFactoryArguments, Transform root, int capacity)
            where T : class, IPoolObject
        {
            var objectPool = new FiniteObjectPool<IPoolObject>(objectFactory, objectFactoryArguments, capacity);
            var poolObjectArguments = new PoolObjectArguments(root);
            pool.BindObjectPool<T>(objectPool, poolObjectArguments);
        }
    }
}
