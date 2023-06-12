using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class StatesControl<T> : BaseMonoBehaviour, IEnumerable<StatesControl<T>.State>
        where T : Component
    {
        [Serializable]
        public struct State
        {
            public string Id;
            public T Control;
        }

        [SerializeField]
        private State[] _states;

        private State _state;
 
        public event Action<State> StateChanged;

        public State CurrentState => _state;

        protected virtual void Awake()
        {
            ChangeState(_states[0].Id);
        }

        public void ChangeState(string id)
        {
            if (_state.Id == id)
            {
                return;
            }
            _state = _states.First(x => x.Id == id);
            _states.ForEach(x => x.Control.gameObject.SetActive(_state.Id == x.Id));
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
