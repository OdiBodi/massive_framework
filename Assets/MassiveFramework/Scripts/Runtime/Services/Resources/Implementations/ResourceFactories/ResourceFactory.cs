using System;
using System.Collections.Generic;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class ResourceFactory : PlaceholderFactory<Type, IResource>
    {
        public virtual T Create<T>()
            where T : class, IResource
        {
            var arguments = new List<TypeValuePair>
            {
                InjectUtil.CreateTypePair(typeof(T))
            };
            var state = CreateInternal(arguments) as T; 
            return state;
        }
    };
}
