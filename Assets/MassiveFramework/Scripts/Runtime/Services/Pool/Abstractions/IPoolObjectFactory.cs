using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public interface IPoolObjectFactory
    {
        IPoolObject Create(string id, Transform root);
    }
}
