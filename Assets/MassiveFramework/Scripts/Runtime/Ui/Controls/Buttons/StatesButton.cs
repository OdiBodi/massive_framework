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

        public event Action<State> OnClicked;
        public event Action<State> OnStateChanged;

        public State CurrentState => _state;

        private void Awake()
        {
            Subscribe();
            UpdateState(_states[0].id);
        }

        private void Subscribe()
        {
            _states.ForEach(state => state.button.OnClicked += () => OnClicked?.Invoke(state));
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

        public void UpdateState(string id)
        {
            if (_state.id == id)
            {
                return;
            }
            _state = _states.First(x => x.id == id);
            _states.ForEach(x => x.button.UpdateActivity(_state.id == x.id));
            OnStateChanged?.Invoke(_state);
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
