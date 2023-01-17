using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class PoolObject : BaseMonoBehaviour, IPoolObject
    {
        private Transform _root;

        public virtual string Id => name;
        public virtual bool Active => this.Activity();

        public virtual void Request(Transform root)
        {
            _root = root;
            CacheTransform.SetParent(root);
            this.ChangeActivity(true);
        }

        public virtual void Return()
        {
            CacheTransform.SetParent(_root);
            this.ChangeActivity(false);
        }

        public virtual void Remove()
        {
            Destroy(CacheGameObject);
        }
    }
}
