using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class Pool : IPool
    {
        private readonly Transform _root;
        private readonly int _capacity;

        private readonly Dictionary<Type, IPoolObjectFactory> _objectFactories = new();

        private IPoolObject[] _objects;

        public Pool(Transform root, int capacity)
        {
            _root = root;
            _capacity = capacity;
        }
        
        public void Initialize()
        {
            InitializeEmptyObjects();
        }

        public void BindFactory<T>(IPoolObjectFactory factory)
        {
            _objectFactories.Add(typeof(T), factory);
        }

        public IEnumerable<T> Objects<T>(string id)
            where T : class, IPoolObject
        {
            var objects = _objects.OfType<T>();
            if (!string.IsNullOrEmpty(id))
            {
                objects = objects.Where(x => x.Id == id);
            }
            return objects;
        }

        public T Request<T>(string id = "")
            where T : class, IPoolObject
        {
            IPoolObject obj;

            if (string.IsNullOrEmpty(id))
            {
                obj = _objects.FirstOrDefault(x => x is { Active: false } and T);
            }
            else
            {
                obj = _objects.FirstOrDefault(x => x is { Active: false } && x.Id == id && x is T);
            }

            if (obj == null)
            {
                for (var i = 0; i < _objects.Length; ++i)
                {
                    if (_objects[i] != null)
                    {
                        continue;
                    }
                    if (!_objectFactories.TryGetValue(typeof(T), out var objectFactory))
                    {
                        throw new Exception($"Pool factory \"{typeof(T).Name}\" didn't find!");
                    }
                    obj = objectFactory.Create(id, _root);
                    _objects[i] = obj;
                    break;
                }
            }

            obj.Request(_root);

            return obj as T;
        }

        public void Return<T>(T obj)
            where T : class, IPoolObject
        {
            if (!_objects.Contains(obj))
            {
                return;
            }
            obj.Return();
        }

        public void ReturnAll<T>()
            where T : class, IPoolObject
        {
            _objects.Where(obj => obj is T).ForEach(obj => obj.Return());
        }

        public void ReturnAll()
        {
            _objects.ForEach(obj => obj.Return());
        }

        public void RemoveAll<T>()
            where T : class, IPoolObject
        {
            for (var i = 0; i < _objects.Length; ++i)
            {
                var obj = _objects[i];
                if (obj is not T)
                {
                    return;
                }
                obj.Remove();
                _objects[i] = null;
            }
        }

        private void InitializeEmptyObjects()
        {
            _objects = new IPoolObject[_capacity];
        }
    }
}
