using System;
using System.Collections.Generic;
using MassiveCore.Framework.Runtime.Patterns;

namespace MassiveCore.Framework.Runtime
{
    public class Pool : IPool
    {
        private readonly Dictionary<Type, ObjectPoolItem> _pools = new();

        public void BindObjectPool<T>(IObjectPool<IPoolObject> objectPool, IPoolObjectArguments poolObjectArguments)
            where T : IPoolObject
        {
            _pools[typeof(T)] = new ObjectPoolItem
            {
                ObjectPool = objectPool,
                PoolObjectArguments = poolObjectArguments
            };
        }

        public T Request<T>(string id = "")
            where T : class, IPoolObject
        {
            var objectPool = ObjectPool<T>(); 
            var obj = objectPool.ObjectPool.Request(id, objectPool.PoolObjectArguments);
            return obj as T;
        }

        public void Return<T>(T obj)
            where T : class, IPoolObject
        {
            ObjectPool<T>().ObjectPool.Return(obj);
        }

        private ObjectPoolItem ObjectPool<T>()
        {
            if (!_pools.TryGetValue(typeof(T), out var item))
            {
                throw new ArgumentException($"Object pool \"{typeof(T).Name}\" didn't find!");
            }
            return item;
        }
    }
}
