using System;
using MassiveCore.Framework.Runtime.Patterns;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class StateCustomFactory<T> : IFactory<Type, IState<T>>
    {
        private readonly DiContainer _diContainer;

        public StateCustomFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public IState<T> Create(Type type)
        {
            var state = _diContainer.Instantiate(type) as IState<T>; 
            return state;
        }
    }
}
