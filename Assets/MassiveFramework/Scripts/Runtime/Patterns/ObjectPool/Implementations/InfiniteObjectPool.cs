using System;
using System.Collections.Generic;
using System.Linq;

namespace MassiveCore.Framework.Runtime.Patterns
{
    public class InfiniteObjectPool<T> : IObjectPool<T>
        where T : IPoolObject
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
            var obj = string.IsNullOrEmpty(id) ? Request(x => true) : Request(x => x.Id == id);
            obj ??= _objectFactory.Product(_objectFactoryArguments);
            obj.Request(arguments);
            return obj;
        }

        public void Return(T obj)
        {
            if (_objects.Contains(obj))
            {
                throw new ArgumentException();
            }
            obj.Return();
            _objects.AddLast(obj);
        }

        private T Request(Func<T, bool> predicate)
        {
            var obj = _objects.FirstOrDefault(predicate);
            if (obj == null)
            {
                return default;
            }
            _objects.Remove(obj);
            return obj;
        }
    }
}
