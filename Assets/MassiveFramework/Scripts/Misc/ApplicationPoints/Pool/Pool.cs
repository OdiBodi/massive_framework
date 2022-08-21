using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class Pool : BaseMonoBehaviour
    {
        [SerializeField]
        private Transform root;

        [SerializeField]
        private int size = 100;

        private Dictionary<Type, Func<string, GameObject>> factories = new Dictionary<Type, Func<string, GameObject>>();

        private GameObject[] pool;

        private void Awake()
        {
            InitPool();
        }

        public void AddFactory<T>(Func<string, GameObject> onCreate)
        {
            factories.Add(typeof(T), onCreate);
        }

        public T Get<T>(string id = "")
        {
            GameObject obj;
            if (string.IsNullOrEmpty(id))
            {
                obj = pool.FirstOrDefault(x => x != null && !x.activeSelf && x.GetComponent<T>() != null);
            }
            else
            {
                obj = pool.FirstOrDefault(x => x != null && !x.activeSelf && x.name == id && x.GetComponent<T>() != null);
            }
            if (obj == null)
            {
                for (var i = 0; i < pool.Length; ++i)
                {
                    if (pool[i] != null)
                    {
                        continue;
                    }
                    if (factories.TryGetValue(typeof(T), out var createObject))
                    {
                        obj = createObject(id);
                        if (!string.IsNullOrEmpty(id))
                        {
                            obj.name = id;
                        }
                        obj.transform.SetParent(root);
                        pool[i] = obj;
                        break;
                    }
                }
            }
            else
            {
                obj.SetActive(true);
            }
            return obj != null ? obj.GetComponent<T>() : default;
        }

        public void Return<T>(T obj) where T : BaseMonoBehaviour
        {
            obj.UpdateActivity(false);
        }

        public void ReturnAll<T>()
        {
            foreach (var o in pool)
            {
                if (o != null && o.GetComponent<T>() != null)
                {
                    o.SetActive(false);
                }
            }
        }

        public void ReturnAll(IEnumerable<Type> types)
        {
            foreach (var o in pool)
            {
                if (o != null && types.Any(type => o.GetComponent(type)))
                {
                    o.SetActive(false);
                }
            }
        }

        public void ReturnAll()
        {
            foreach (var o in pool)
            {
                if (o != null)
                {
                    o.SetActive(false);
                }
            }
        }

        public void RemoveAll<T>()
        {
            for (var i = 0; i < pool.Length; ++i)
            {
                var o = pool[i];
                if (o != null && o.GetComponent<T>() != null)
                {
                    pool[i] = null;
                    Destroy(o);
                }
            }
        }

        public void RemoveAll(IEnumerable<Type> types)
        {
            for (var i = 0; i < pool.Length; ++i)
            {
                var o = pool[i];
                if (o != null && types.Any(type => o.GetComponent(type)))
                {
                    pool[i] = null;
                    Destroy(o);
                }
            }
        }

        private void InitPool()
        {
            pool = new GameObject[size];
        }
    }
}
