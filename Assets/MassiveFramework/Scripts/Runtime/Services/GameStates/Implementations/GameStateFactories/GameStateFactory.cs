using System;
using System.Collections.Generic;
using Zenject;

namespace MassiveCore.Framework
{
    public class GameStateFactory : PlaceholderFactory<Type, IGameState>
    {
        public virtual T Create<T>()
            where T : class, IGameState
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
