using MassiveCore.Framework.Runtime.Patterns;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class PoolObjectArguments : IPoolObjectArguments
    {
        public PoolObjectArguments(Transform root)
        {
            Root = root;
        }

        public Transform Root { get; private set; }
    }
}
