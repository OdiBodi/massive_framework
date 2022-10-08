using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class States : IStates
    {
        [Inject]
        private readonly ILogger _logger;

        private readonly Dictionary<Type, IState> _states = new();

        public IState CurrentState { get; private set; }

        public void BindState<T>(T state)
            where T : class, IState
        {
            if (State<T>() != null)
            {
                throw new Exception($"State \"{typeof(T)}\" was added!");
            }
            _states[typeof(T)] = state;
            _logger.Print($"State \"{typeof(T)}\" added!");
        }

        public T State<T>()
            where T : class, IState
        {
            var result = _states.TryGetValue(typeof(T), out var state);
            if (!result)
            {
                return default;
            }
            return state as T;
        }

        public async UniTask GoTo<T>()
            where T : class, IState
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
