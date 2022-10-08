using System;
using Zenject;

namespace MassiveCore.Framework
{
    public class StateCustomFactory : IFactory<Type, IState>
    {
        private readonly DiContainer _diContainer;

        public StateCustomFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public IState Create(Type type)
        {
            var state = _diContainer.Instantiate(type) as IState; 
            return state;
        }
    }
}
