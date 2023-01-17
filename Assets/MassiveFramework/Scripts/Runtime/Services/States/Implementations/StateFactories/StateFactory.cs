using System;
using System.Collections.Generic;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class StateFactory<T> : PlaceholderFactory<Type, IState<T>>
    {
        public virtual S Create<S>()
            where S : class, IState<T>
        {
            var arguments = new List<TypeValuePair>
            {
                InjectUtil.CreateTypePair(typeof(S))
            };
            var state = CreateInternal(arguments) as S; 
            return state;
        }
    };
}
