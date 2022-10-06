using UnityEngine;

namespace MassiveCore.Framework
{
    public interface IPoolObjectFactory
    {
        IPoolObject Create(string id, Transform root);
    }
}
