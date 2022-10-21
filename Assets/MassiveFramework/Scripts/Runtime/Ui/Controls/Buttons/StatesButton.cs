using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class StatesButton : BaseMonoBehaviour, IEnumerable<StatesButton.State>
    {
        [Serializable]
        public struct State
        {
            public string id;
            public BaseButton button;
        }

        [SerializeField]
        private State[] _states;

        private State _state;

        public event Action<State> Clicked;
        public event Action<State> StateChanged;

        public State CurrentState => _state;

        private void Awake()
        {
            Subscribe();
            ChangeState(_states[0].id);
        }

        private void Subscribe()
        {
            _states.ForEach(state => state.button.Clicked += () => Clicked?.Invoke(state));
        }

        public T Button<T>(string id)
            where T : BaseButton 
        {
            return (T)_states.First(state => state.id == id).button;
        }

        public T CurrentButton<T>()
            where T : BaseButton
        {
            return (T)_state.button;
        }

        public void ChangeState(string id)
        {
            if (_state.id == id)
            {
                return;
            }
            _state = _states.First(x => x.id == id);
            _states.ForEach(x => x.button.ChangeActivity(_state.id == x.id));
            StateChanged?.Invoke(_state);
        }

        public IEnumerator<State> GetEnumerator()
        {
            return ((IEnumerable<State>)_states).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
