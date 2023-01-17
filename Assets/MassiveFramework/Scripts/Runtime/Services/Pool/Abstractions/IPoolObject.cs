using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public interface IPoolObject
    {
        string Id { get; }
        bool Active { get; }
        void Request(Transform root);
        void Return();
        void Remove();
    }
}
