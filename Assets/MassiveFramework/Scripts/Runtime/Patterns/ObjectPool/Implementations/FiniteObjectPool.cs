using System;
using System.Linq;

namespace MassiveCore.Framework.Runtime.Patterns
{
    public class FiniteObjectPool<T> : IObjectPool<T>
        where T : IPoolObject
    {
        private readonly IAbstractFactory<T> _objectFactory;
        private readonly IAbstractFactoryArguments _objectFactoryArguments;
        private readonly T[] _objects;

        public int Capacity => _objects.Length;
        public int Count => _objects.Count(x => x != null);

        public FiniteObjectPool(IAbstractFactory<T> objectFactory, IAbstractFactoryArguments objectFactoryArguments,
            int capacity)
        {
            _objectFactory = objectFactory;
            _objectFactoryArguments = objectFactoryArguments;
            _objects = new T[capacity];
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
            for (var i = 0; i < _objects.Length; ++i)
            {
                if (_objects[i] != null)
                {
                    continue;
                }
                obj.Return();
                _objects[i] = obj;
                return;
            }
            throw new ArgumentOutOfRangeException();
        }

        private T Request(Func<T, bool> predicate)
        {
            for (var i = 0; i < _objects.Length; ++i)
            {
                var obj = _objects[i];
                if (obj == null || !predicate(obj))
                {
                    continue;
                }
                _objects[i] = default;
                return obj;
            }
            return default;
        }
    }
}
