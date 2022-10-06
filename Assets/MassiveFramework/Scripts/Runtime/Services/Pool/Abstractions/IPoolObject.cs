using UnityEngine;

namespace MassiveCore.Framework
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
