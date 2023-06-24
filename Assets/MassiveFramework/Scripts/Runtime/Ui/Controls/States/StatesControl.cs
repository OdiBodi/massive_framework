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
            public bool Valid => !string.IsNullOrEmpty(Id) && Control;
        }

        [SerializeField]
        private State[] _states;

        [SerializeField]
        private string _originState;

        private State _state;
 
        public event Action<State> StateChanged;

        public State CurrentState => _state;

        protected virtual void Awake()
        {
            ChangeState(_originState);
        }

        public State StateBy(string id)
        {
            return _states.FirstOrDefault(x => x.Id == id);
        }

        public void ChangeState(string id)
        {
            if (_state.Id == id)
            {
                return;
            }
            var state = StateBy(id);
            if (!state.Valid)
            {
                return;
            }
            _state = state;
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
