using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class StatesControl<T> : BaseMonoBehaviour, IEnumerable<StatesControl<T>.State>
        where T : Component
    {
        [Serializable]
        public struct State
        {
            public string id;
            public T control;
        }

        [SerializeField]
        private State[] _states;

        private State _state;
 
        public event Action<State> StateChanged;

        public State CurrentState => _state;

        protected virtual void Awake()
        {
            ChangeState(_states[0].id);
        }

        public void ChangeState(string id)
        {
            if (_state.id == id)
            {
                return;
            }
            _state = _states.First(x => x.id == id);
            _states.ForEach(x => x.control.gameObject.SetActive(_state.id == x.id));
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
