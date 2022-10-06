using System;
using Zenject;

namespace MassiveCore.Framework
{
    public class GameStateCustomFactory : IFactory<Type, IGameState>
    {
        private readonly DiContainer _diContainer;

        public GameStateCustomFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public IGameState Create(Type type)
        {
            var state = _diContainer.Instantiate(type) as IGameState; 
            return state;
        }
    }
}
