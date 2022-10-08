using System;
using System.Collections.Generic;
using Zenject;

namespace MassiveCore.Framework
{
    public class StateFactory : PlaceholderFactory<Type, IState>
    {
        public virtual T Create<T>()
            where T : class, IState
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
