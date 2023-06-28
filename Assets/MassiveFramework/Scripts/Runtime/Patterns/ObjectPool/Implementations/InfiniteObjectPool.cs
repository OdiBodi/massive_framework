using System;
using System.Collections.Generic;
using System.Linq;

namespace MassiveCore.Framework.Runtime.Patterns
{
    public class InfiniteObjectPool<T> : IObjectPool<T>
        where T : class, IPoolObject
    {
        private readonly IAbstractFactory<T> _objectFactory;
        private readonly IAbstractFactoryArguments _objectFactoryArguments;
        private readonly LinkedList<T> _objects = new ();

        public int Capacity => Count;
        public int Count => _objects.Count;

        public InfiniteObjectPool(IAbstractFactory<T> objectFactory, IAbstractFactoryArguments objectFactoryArguments)
        {
            _objectFactory = objectFactory;
            _objectFactoryArguments = objectFactoryArguments;
        }

        public T Request(string id = "", IPoolObjectArguments arguments = null)
        {
            var obj = string.IsNullOrEmpty(id) ? Request(_ => true) : Request(x => x.Id == id);
            var objectFactoryArguments = new PoolAbstractFactoryArguments(id, _objectFactoryArguments);
            obj ??= _objectFactory.Product(objectFactoryArguments);
            obj.Request(arguments);
            return obj;
        }

        public void Return(T obj)
        {
            if (_objects.Contains(obj))
            {
                return;
            }
            obj.Return();
            _objects.AddLast(obj);
        }

        private T Request(Func<T, bool> predicate)
        {
            var obj = _objects.FirstOrDefault(predicate);
            if (obj == null)
            {
                return null;
            }
            _objects.Remove(obj);
            return obj;
        }
    }
}
