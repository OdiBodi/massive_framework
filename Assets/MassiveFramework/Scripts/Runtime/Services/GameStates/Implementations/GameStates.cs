using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public class GameStates : IGameStates
    {
        private readonly List<IGameState> _states = new();

        public IGameState CurrentState { get; private set; }

        public void BindState<T>(T gameState)
            where T : class, IGameState
        {
            if (State<T>() != null)
            {
                throw new Exception($"Game state \"{typeof(T)}\" was added!");
            }
            _states.Add(gameState);
        }

        public T State<T>()
            where T : class, IGameState
        {
            var state = _states.FirstOrDefault(x => x.GetType() == typeof(T)); 
            return state as T;
        }

        public async UniTask GoTo<T>()
            where T : class, IGameState
        {
            var previousState = CurrentState; 
            if (CurrentState != null)
            {
                await CurrentState.Exit();
            }
            CurrentState = State<T>();
            if (CurrentState != null)
            {
                await CurrentState.Enter(previousState);
            }
        }
    }
}
